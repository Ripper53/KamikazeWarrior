using UnityEngine;

public class CapsuleCastCheck : Check {
    public Vector2 Size;
    public CapsuleDirection2D CapsuleDirection;
    public Vector2 Direction;
    public float Distance;
    public LayerMask LayerMask;

    public override bool EvaluateCheck() {
        return Physics2D.CapsuleCast(Origin.position, Size, CapsuleDirection, 0f, Direction, Distance, LayerMask);
    }
}
