using UnityEngine;

/// <summary>
/// Script should be disabled in the editor to work properly.
/// </summary>
public class UnitJumpFadeOnDestroy : MonoBehaviour {
    public UnitData UnitData;
    public Rigidbody2D RB;
    public SpriteRenderer SR;
    public Collider2D Col;
    public Vector2 JumpForce;
    public float DestroyDelay = 1f;

    private void Awake() {
        UnitData.Destroyed += UnitData_Destroyed;
    }
    private void OnDestroy() {
        UnitData.Destroyed -= UnitData_Destroyed;
    }

    private void UnitData_Destroyed(UnitData source) {
        Col.enabled = false;
        Color color = SR.color;
        color.a = 0.5f;
        SR.color = color;
        RB.velocity = new Vector2(0f, 0f);
        RB.AddForce(JumpForce, ForceMode2D.Impulse);
        enabled = true;
    }

    private float timer = 0f;
    private void Update() {
        timer += Time.deltaTime;
        if (timer > DestroyDelay)
            Destroy(gameObject);
        else {
            Color color = SR.color;
            color.a = (1f - timer) / DestroyDelay;
            if (color.a > 0.5f)
                color.a = 0.5f;
            SR.color = color;
        }
    }
}
