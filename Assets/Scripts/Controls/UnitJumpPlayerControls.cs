using UnityEngine.InputSystem;

public class UnitJumpPlayerControls : PlayerControls {
    public UnitJump UnitJump;

    private void Start() {
        input.Movement.Vertical.performed += Vertical_performed;

        input.Movement.Vertical.canceled += Vertical_canceled;
    }

    private void OnDestroy() {
        input.Movement.Vertical.performed -= Vertical_performed;

        input.Movement.Vertical.canceled += Vertical_canceled;
    }

    #region Events
    private void Vertical_performed(InputAction.CallbackContext obj) {
        if (obj.ReadValue<float>() > 0f)
            UnitJump.Request = true;
    }

    private void Vertical_canceled(InputAction.CallbackContext obj) {
        UnitJump.Request = false;
    }
    #endregion

}
