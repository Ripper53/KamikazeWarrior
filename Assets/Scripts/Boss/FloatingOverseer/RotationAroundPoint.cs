using UnityEngine;

public class RotationAroundPoint : MonoBehaviour {
    public Transform Point;
    public float Speed, Distance;
    public Transform[] Tranforms;

    private void FixedUpdate() {
        Vector2 pos = Point.position;
        foreach (Transform transform in Tranforms) {
            Vector2 newPos = (Vector2)transform.position - pos;
            float angle = Mathf.Atan2(newPos.y, newPos.x) + Speed;

            newPos.x = Mathf.Cos(angle);
            newPos.y = Mathf.Sin(angle);

            transform.position = (newPos * Distance) + pos;
        }
    }


}
