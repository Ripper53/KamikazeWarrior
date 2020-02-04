using UnityEngine;

public abstract class SmoothDampTo : MonoBehaviour {
    public float SmoothTime;

    public abstract Vector2 Position { get; set; }
    public abstract Vector2 Target { get; }

    private Vector2 currentVelocity = new Vector2(0f, 0f);

    private void FixedUpdate() {
        Position = Vector2.SmoothDamp(Position, Target, ref currentVelocity, SmoothTime);
        if (Vector2.Distance(Position, Target) < 0.1f) {
            Position = Target;
            enabled = false;
        }
    }
}
