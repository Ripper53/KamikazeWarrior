using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenuControls : PlayerControls {
    public MainMenu MainMenu;

    private void Start() {
        input.Movement.Vertical.performed += Vertical_performed;
        input.Movement.Horizontal.performed += Horizontal_performed;
    }

    private void OnDestroy() {
        input.Movement.Vertical.performed -= Vertical_performed;
        input.Movement.Horizontal.performed -= Horizontal_performed;
    }

    private bool playGame = true;
    private void Vertical_performed(InputAction.CallbackContext obj) {
        Vector2 pos = MainMenu.SelectedTransform.position;
        if (obj.ReadValue<float>() > 0f) {
            playGame = true;
            pos.y = MainMenu.PlayTransform.position.y;
        } else {
            playGame = false;
            pos.y = MainMenu.QuitTransform.position.y;
        }
        MainMenu.SelectedTransform.position = pos;
    }

    private void Horizontal_performed(InputAction.CallbackContext obj) {
        if (playGame) {
            SceneManager.LoadSceneAsync(1);
            enabled = false;
        } else {
            Application.Quit();
        }
    }
}
