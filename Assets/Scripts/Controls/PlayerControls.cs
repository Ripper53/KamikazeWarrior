using UnityEngine;

public abstract class PlayerControls : MonoBehaviour {
    protected PlayerInput input;

    protected void Awake() {
        input = new PlayerInput();
    }
    protected void Start() {
        AddEvents();
    }
    protected void OnEnable() {
        input.Enable();
    }
    protected void OnDisable() {
        input.Disable();
    }

    protected void OnDestroy() {
        RemoveEvents();
        input.Dispose();
    }

    protected abstract void AddEvents();
    protected abstract void RemoveEvents();

}
