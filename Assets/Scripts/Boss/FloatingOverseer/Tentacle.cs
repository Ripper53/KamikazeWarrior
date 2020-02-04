using UnityEngine;

public class Tentacle : MonoBehaviour {
    public Node[] Nodes;

    [System.Serializable]
    public class Node {
        public Transform Transform;
        //[System.NonSerialized]
        public Vector2 Target, CurrentVelocity;
    }

    private void Awake() {
        foreach (Node node in Nodes)
            node.Target = node.Transform.localPosition;
    }

    private void FixedUpdate() {
        foreach (Node node in Nodes) {
            node.Transform.localPosition = Vector2.SmoothDamp(node.Transform.localPosition, node.Target, ref node.CurrentVelocity, 1f);
        }
    }
}
