using UnityEngine;

public abstract class Check : MonoBehaviour {
    public Transform Origin;

    public abstract bool EvaluateCheck();
}
