using UnityEngine;

public class BoxOverlapCheck : Check {
    public Vector2 Size;
    public LayerMask LayerMask;

    public override bool EvaluateCheck() {
        return Physics2D.OverlapBox(Origin.position, Size, Origin.eulerAngles.z, LayerMask);
    }
}
