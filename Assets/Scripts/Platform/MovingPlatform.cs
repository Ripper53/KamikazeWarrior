using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {
    public Transform Transform;

    private Vector2 previousPosition;

    private void Awake() {
        previousPosition = Transform.position;
    }

    private readonly HashSet<IMoveable> on = new HashSet<IMoveable>();
    private void OnTriggerEnter2D(Collider2D collision) {
        IMoveable moveable = collision.GetComponent<IMoveable>();
        if (moveable != null)
            on.Add(moveable);
    }
    private void OnTriggerExit2D(Collider2D collision) {
        IMoveable moveable = collision.GetComponent<IMoveable>();
        if (moveable != null)
            on.Remove(moveable);
    }

    private void FixedUpdate() {
        Vector2 pos = Transform.position, vel = pos - previousPosition;
        foreach (IMoveable moveable in on) {
            moveable.Move(vel);
        }
        previousPosition = pos;
    }
}
