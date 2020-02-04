using UnityEngine;

public class UnitJump : MonoBehaviour {
    public Rigidbody2D RB;
    public UnitMovement UnitMovement;
    [System.NonSerialized]
    public bool Request = false;
    public Vector2 JumpForce;

    public void Execute() {
        Vector2 force = new Vector2(0f, JumpForce.y);
        switch (UnitMovement.Direction) {
            case Direction.Right:
                force.x = JumpForce.x;
                break;
            case Direction.Left:
                force.x = -JumpForce.x;
                break;
        }

        RB.velocity = new Vector2(0f, 0f);
        RB.AddForce(force, ForceMode2D.Impulse);
    }
}
