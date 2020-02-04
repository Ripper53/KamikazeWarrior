using UnityEngine;

public enum Direction {
    None, Right, Left
};
public class UnitMovement : MonoBehaviour {
    public Rigidbody2D RB;
    [System.NonSerialized]
    public Direction Direction = Direction.None;
    public float Speed;

    public void Execute(Vector2 toMove) {
        switch (Direction) {
            case Direction.Right:
                MovePosition(new Vector2(Speed, 0f) + toMove);
                break;
            case Direction.Left:
                MovePosition(new Vector2(-Speed, 0f) + toMove);
                break;
            default:
                if (toMove.x != 0f || toMove.y != 0f)
                    MovePosition(toMove);
                break;
        }
    }

    private void MovePosition(Vector2 position) {
        RB.MovePosition(RB.position + position);
    }
}
