using System;
using UnityEngine;

public class Projectile : MonoBehaviour {
    [SerializeField] GameObject HitPrefab;
    [Range(1, 100f)]
    [SerializeField] float DamageAmount = 10f;
    [Range(0f, 360f)]
    public float Direction = 0f;
    [Range(1f, 100f)]
    public float Speed = 30f;
    public Color Tint = Color.white;
    public float TimeToLive = 5f;

    Collider2D col;
    bool wasInsideViewport = false;

    void Start() {
        // destroy when TTL expires
        Destroy(this.gameObject, this.TimeToLive);

        // grab collider
        this.col = this.GetComponent<Collider2D>();

        // set color
        var spr = this.GetComponent<SpriteRenderer>();
        spr.color = this.Tint;

        // set velocity
        Vector3 velocity = this.Speed * this.direction();
        var rigidBody = this.GetComponent<Rigidbody2D>();
        rigidBody.velocity = velocity;
    }

    void Update() {
        bool isInsideViewport = this.isInsideViewport(this.transform.position);
        if (this.wasInsideViewport && !isInsideViewport) {
            Destroy(this.gameObject);
            return;
        }
        this.wasInsideViewport = isInsideViewport;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.TryGetComponent(out Damageable damageable)) {
            ContactPoint2D[] contacts = new ContactPoint2D[1];
            collision.GetContacts(contacts);

            damageable.TakeDamage(this.DamageAmount, contacts[0].point);
            Destroy(this.gameObject);

            if (this.HitPrefab != null) {
                var gameObject = Instantiate(this.HitPrefab, this.transform.position, this.transform.rotation);
                gameObject.transform.localScale = this.transform.localScale;
            }
        }
    }

    void OnDrawGizmosSelected() {
        GizmosExtensions.DrawArrow(this.transform.position, this.direction() * this.transform.localScale, Color.yellow, 0.05f);
    }

    Vector2 direction() {
        Vector3 direction = Quaternion.AngleAxis(this.Direction, Vector3.forward) * Vector3.right;
        return direction.normalized;
    }

    bool isInsideViewport(Vector2 point) {
        var worldRect = MainCameraHelper.WorldRect();
        worldRect.Expand(Mathf.Max(this.col.bounds.extents.x, this.col.bounds.extents.y));
        return worldRect.Contains(point);
    }
}
