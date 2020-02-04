using UnityEngine;

public class UnitSystem : MonoBehaviour, IMoveable {
    public Rigidbody2D RB;
    public UnitMovement UnitMovement;
    public UnitJump UnitJump;

    [Header("Checks")]
    public Check GroundCheck;

    private void FixedUpdate() {
        if (RB.velocity.y < 0.01f && GroundCheck.EvaluateCheck()) {
            RB.velocity = new Vector2(0f, RB.velocity.y);
            if (UnitJump.Request)
                UnitJump.Execute();
            else
                UnitMovement.Execute(toMove);
        }
        toMove = new Vector2(0f, 0f);
    }

    private Vector2 toMove = new Vector2(0f, 0f);
    public void Move(Vector2 position) {
        toMove += position;
    }
}
