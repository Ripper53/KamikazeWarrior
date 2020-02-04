using UnityEngine;

public class Checkpoint : MonoBehaviour {
    public Transform Point;
    public LayerMask LayerMask;


    public float rotation = 0f;
    private void Update() {
        if (rotation > 0f) {
            float vel = 100f * Time.deltaTime, result = rotation - vel;
            if (result <= 0f) {
                rotation = 0f;
                vel = rotation;
                transform.rotation = Quaternion.identity;
            } else {
                rotation = result;
                transform.rotation *= Quaternion.Euler(0f, 0f, vel);
            }
        }
    }

    public delegate void CheckpointAction(Checkpoint source, CheckpointSaver checkpointSaver);
    public event CheckpointAction Checkpointed;
    private void FixedUpdate() {
        foreach (Collider2D col in Physics2D.OverlapBoxAll(Point.position, new Vector2(1f, 1f), 0f, LayerMask)) {
            CheckpointSaver checkpointSaver = col.GetComponent<CheckpointSaver>();
            if (checkpointSaver != null) {
                checkpointSaver.Checkpoint = this;
                rotation = 90f;
                Checkpointed?.Invoke(this, checkpointSaver);
            }
        }
    }

}
