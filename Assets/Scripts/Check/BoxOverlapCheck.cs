using UnityEngine;

public class BoxOverlapCheck : Check {
    public Vector2 Size;
    public LayerMask LayerMask;

    public override bool EvaluateCheck() {
        return Physics2D.OverlapBox(Origin.position, Size, Origin.eulerAngles.z, LayerMask);
    }

#if UNITY_EDITOR
    protected void OnDrawGizmos() {
        if (!Origin) return;
        Gizmos.color = Color.green;
        Gizmos.matrix = Origin.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.zero, Size);
    }
#endif

}
