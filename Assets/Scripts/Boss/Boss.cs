using UnityEngine;

public abstract class Boss : MonoBehaviour {
    public Check ActivationCheck;

    protected bool activated = false;
    protected virtual void FixedUpdate() {
        if (activated) {
            Execute();
        } else if (ActivationCheck.EvaluateCheck()) {
            activated = true;
            Activate();
        }
    }

    public abstract void Activate();
    public abstract void Execute();
}
