using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EndGameRestartControls : PlayerControls {
    public EndGameRestart EndGameRestart;

    protected override void AddEvents() {
        input.Movement.Horizontal.performed += EndGame_Event;
        input.Movement.Vertical.performed += EndGame_Event;
    }

    protected override void RemoveEvents() {
        input.Movement.Horizontal.performed -= EndGame_Event;
        input.Movement.Vertical.performed -= EndGame_Event;
    }

    private void EndGame_Event(InputAction.CallbackContext obj) {
        if (EndGameRestart.Timer <= 0f)
            SceneManager.LoadSceneAsync(0);
    }

}
