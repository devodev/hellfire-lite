using System;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] float Speed = 2.75f;
    [SerializeField] GameObject EngineEffect;
    [Range(0, 1)]
    [SerializeField] float InterpolateRatio;
    [SerializeField] Collider2D Collider;

    Vector2 colCenter;
    Vector3[] screenRectPoints;
    bool isMoving;

    void Update() {
        Vector3 moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        this.movePlayer(moveDirection);

        // show engine effect if moving
        this.EngineEffect.SetActive(this.isMoving);
    }

    void movePlayer(Vector3 direction) {
        var oldPos = this.transform.position;
        var newPos = oldPos + direction * (this.Speed * Time.deltaTime);
        this.isMoving = oldPos != newPos;

        if (this.isMoving) {
            var worldBounds = MainCameraHelper.WorldBounds();
            newPos.x = Mathf.Clamp(newPos.x, worldBounds.min.x + this.Collider.bounds.extents.x, worldBounds.max.x - this.Collider.bounds.extents.x);
            newPos.y = Mathf.Clamp(newPos.y, worldBounds.min.y + this.Collider.bounds.extents.y, worldBounds.max.y - this.Collider.bounds.extents.y);
            this.transform.position = newPos;
        }
    }
}
