using UnityEngine;

public class Rotate : MonoBehaviour {
    [SerializeField] float Speed = 0.0f;
    [SerializeField] Vector3 Direction = Vector3.forward;

    Collider2D col;

    void Start() {
        this.col = this.GetComponent<Collider2D>();
    }

    void Update() {
        this.transform.RotateAround(this.col.bounds.center, this.Direction, this.Speed * Time.deltaTime);
    }
}
