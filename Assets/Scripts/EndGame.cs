using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour {
    public Check Check;

    private void FixedUpdate() {
        if (Check.EvaluateCheck()) {
            SceneManager.LoadSceneAsync(2);
            enabled = false;
        }
    }
}
