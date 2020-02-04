using UnityEngine;

public class CheckpointEnable : MonoBehaviour {
    public Checkpoint Checkpoint;
    public MonoBehaviour MonoBehaviour;

    private void Awake() {
        Checkpoint.Checkpointed += Checkpoint_Checkpointed;
    }

    private void Checkpoint_Checkpointed(Checkpoint source, CheckpointSaver checkpointSaver) {
        source.Checkpointed -= Checkpoint_Checkpointed;
        MonoBehaviour.enabled = true;
    }
}
