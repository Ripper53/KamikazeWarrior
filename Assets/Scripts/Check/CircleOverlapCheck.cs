using UnityEngine;

public class CircleOverlapCheck : Check {
    public float Radius;
    public LayerMask LayerMask;

    public override bool EvaluateCheck() {
        return Physics2D.OverlapCircle(Origin.position, Radius, LayerMask);
    }
}
