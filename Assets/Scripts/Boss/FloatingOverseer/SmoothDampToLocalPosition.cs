using UnityEngine;

public class SmoothDampToLocalPosition : SmoothDampTo {
    public Transform Transform;
    public Vector2 TargetLocalPosition;

    public override Vector2 Position {
        get => Transform.localPosition;
        set => Transform.localPosition = value;
    }
    public override Vector2 Target => TargetLocalPosition;
}
