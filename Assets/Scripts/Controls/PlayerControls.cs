using UnityEngine;

public abstract class PlayerControls : MonoBehaviour {
    protected PlayerInput input;

    private void Awake() {
        input = new PlayerInput();
    }
    private void OnEnable() {
        input.Enable();
    }
    private void OnDisable() {
        input.Disable();
    }
}
