using UnityEngine;

public class EndGameRestart : MonoBehaviour {
    public float Timer;

    private void Update() {
        if (Timer > 0f)
            Timer -= Time.deltaTime;
    }
}
