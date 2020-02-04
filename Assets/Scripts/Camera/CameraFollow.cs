using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform Transform, Target;
    public Vector2 Offset;

    private void LateUpdate() {
        Vector3 pos = Transform.position, target = Target.position;
        pos.x = target.x + Offset.x;
        pos.y = target.y + Offset.y;
        Transform.position = pos;
    }
}
