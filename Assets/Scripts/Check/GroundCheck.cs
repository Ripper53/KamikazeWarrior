using UnityEngine;

public class GroundCheck : BoxOverlapCheck {
    public float CoyoteTime;
    private float timer = 0f;

    public override bool EvaluateCheck() {
        if (base.EvaluateCheck()) {
            timer = CoyoteTime;
            enabled = true;
            return true;
        }
        return timer > 0f;
    }

    protected void FixedUpdate() {
        timer -= Time.fixedDeltaTime;
        if (timer <= 0f)
            enabled = false;
    }

}
