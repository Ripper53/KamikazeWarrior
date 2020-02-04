using UnityEngine.InputSystem;

public class UnitMovementPlayerControls : PlayerControls {
    public UnitMovement UnitMovement;

    private void Start() {
        input.Movement.Horizontal.performed += Horizontal_performed;

        input.Movement.Horizontal.canceled += Horizontal_canceled;
    }

    private void OnDestroy() {
        input.Movement.Horizontal.performed -= Horizontal_performed;

        input.Movement.Horizontal.canceled -= Horizontal_canceled;
    }

    #region Events
    private void Horizontal_performed(InputAction.CallbackContext obj) {
        if (obj.ReadValue<float>() > 0f)
            UnitMovement.Direction = Direction.Right;
        else
            UnitMovement.Direction = Direction.Left;
    }

    private void Horizontal_canceled(InputAction.CallbackContext obj) {
        UnitMovement.Direction = Direction.None;
    }
    #endregion

}
