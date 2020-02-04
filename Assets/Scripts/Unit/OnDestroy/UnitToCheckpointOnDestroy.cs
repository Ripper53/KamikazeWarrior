using UnityEngine;

/// <summary>
/// Script should be disabled in the editor to work properly.
/// </summary>
public class UnitToCheckpointOnDestroy : MonoBehaviour {
    public UnitData UnitData;
    public CheckpointSaver CheckpointSaver;
    public Transform Transform;
    public Rigidbody2D RB;
    public Collider2D Col;

    private void Awake() {
        UnitData.Destroyed += UnitData_Destroyed;
    }
    private void OnDestroy() {
        UnitData.Destroyed -= UnitData_Destroyed;
    }

    private void UnitData_Destroyed(UnitData source) {
        Trigger();
    }

    public void Trigger() {
        if (CheckpointSaver.Checkpoint == null) return;
        RB.simulated = false;
        Col.enabled = false;
        currentVelocity = new Vector2(0f, 0f);
        UnitData.Health = UnitData.MaxHealth;
        enabled = true;
    }

    private Vector2 currentVelocity;
    private void FixedUpdate() {
        Vector2 pos = Transform.position, targetPos = CheckpointSaver.Checkpoint.Point.position;
        pos = Vector2.SmoothDamp(pos, targetPos, ref currentVelocity, 0.1f);

        if (Vector2.Distance(pos, targetPos) < 0.1f) {
            pos = targetPos;
            RB.velocity = new Vector2(0f, 0f);
            RB.simulated = true;
            Col.enabled = true;
            enabled = false;
        }

        Transform.position = pos;
    }

}
