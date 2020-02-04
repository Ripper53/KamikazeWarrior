using UnityEngine;

public class Elevator : MonoBehaviour {
    public Transform Transform;
    public float TopY, BottomY;
    public float TopWaitTime, BottomWaitTime;
    public float Speed;

    public bool GoingUp;

    private float waitTimer = 0f;
    private void FixedUpdate() {
        if (waitTimer > 0f) {
            waitTimer -= Time.fixedDeltaTime;
        } else {
            Vector2 pos = Transform.localPosition;
            if (GoingUp) {
                pos.y += Speed;
                if (pos.y > TopY) {
                    pos.y = TopY;
                    GoingUp = false;
                    waitTimer = TopWaitTime;
                }
            } else {
                pos.y -= Speed;
                if (pos.y < BottomY) {
                    pos.y = BottomY;
                    GoingUp = true;
                    waitTimer = BottomWaitTime;
                }
            }
            Transform.localPosition = pos;
        }
    }
}
