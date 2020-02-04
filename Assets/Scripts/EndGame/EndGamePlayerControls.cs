using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EndGamePlayerControls : PlayerControls {
    public EndGamePlayer EndGamePlayer;

    private void Start() {
        input.Movement.Vertical.performed += Vertical_performed;
        input.Movement.Vertical.canceled += Vertical_canceled;
    }

    private void OnDestroy() {
        input.Movement.Vertical.performed -= Vertical_performed;
        input.Movement.Vertical.canceled -= Vertical_canceled;
    }

    #region Events
    private void Vertical_performed(InputAction.CallbackContext obj) {
        if (obj.ReadValue<float>() > 0f)
            EndGamePlayer.Move = true;
    }

    private void Vertical_canceled(InputAction.CallbackContext obj) {
        EndGamePlayer.Move = false;
    }
    #endregion
}
