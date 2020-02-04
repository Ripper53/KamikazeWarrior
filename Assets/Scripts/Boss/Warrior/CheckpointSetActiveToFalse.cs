using UnityEngine;

public class CheckpointSetActiveToFalse : MonoBehaviour {
    public Checkpoint Checkpoint;
    public GameObject GameObject;

    private void Awake() {
        Checkpoint.Checkpointed += Checkpoint_Checkpointed;
    }

    private void Checkpoint_Checkpointed(Checkpoint source, CheckpointSaver checkpointSaver) {
        source.Checkpointed -= Checkpoint_Checkpointed;
        GameObject.SetActive(false);
    }
}
