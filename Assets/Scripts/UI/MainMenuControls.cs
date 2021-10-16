using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenuControls : PlayerControls {
    public MainMenu MainMenu;

    private void Start() {
#if !UNITY_WEBGL
        input.Movement.Vertical.performed += Vertical_performed;
#endif
        input.Movement.Horizontal.performed += Horizontal_performed;
    }

    private void OnDestroy() {
#if !UNITY_WEBGL
        input.Movement.Vertical.performed -= Vertical_performed;
#endif
        input.Movement.Horizontal.performed -= Horizontal_performed;
    }

#if !UNITY_WEBGL
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
#endif

    private void Horizontal_performed(InputAction.CallbackContext obj) {
#if !UNITY_WEBGL
        if (playGame) {
            SceneManager.LoadSceneAsync(1);
            enabled = false;
        } else {
            Application.Quit();
        }
#else
        SceneManager.LoadSceneAsync(1);
        enabled = false;
#endif
    }

}
