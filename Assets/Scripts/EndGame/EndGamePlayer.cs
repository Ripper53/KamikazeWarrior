using UnityEngine;

public class EndGamePlayer : MonoBehaviour {
    public Transform Transform;
    public float Speed, MaxY;
    public SpriteRenderer TheEndSR;
    public EndGameRestart EndGameRestart;
    [System.NonSerialized]
    public bool Move = false;

    private void FixedUpdate() {
        if (!Move) return;
        Vector3 pos = Transform.position;
        pos.y += Speed;
        if (pos.y >= MaxY) {
            pos.y = MaxY;
            TheEndSR.enabled = true;
            EndGameRestart.enabled = true;
            enabled = false;
        }
        Transform.position = pos;
    }
}
