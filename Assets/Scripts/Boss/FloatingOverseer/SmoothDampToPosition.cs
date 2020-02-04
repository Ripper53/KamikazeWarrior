using UnityEngine;

public class SmoothDampToPosition : SmoothDampTo {
    public Transform Transform;
    public Vector2 TargetPosition;

    public override Vector2 Position {
        get => Transform.position;
        set => Transform.position = value;
    }
    public override Vector2 Target => TargetPosition;
}
