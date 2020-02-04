using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EndGameRestartControls : PlayerControls {
    public EndGameRestart EndGameRestart;

    private void Start() {
        input.Movement.Horizontal.performed += EndGame_Event;
        input.Movement.Vertical.performed += EndGame_Event;
    }

    private void OnDestroy() {
        input.Movement.Horizontal.performed -= EndGame_Event;
        input.Movement.Vertical.performed -= EndGame_Event;
    }

    private void EndGame_Event(InputAction.CallbackContext obj) {
        if (EndGameRestart.Timer <= 0f)
            SceneManager.LoadSceneAsync(0);
    }
}
