using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour {
    [SerializeField] Projectile AsteroidPrefab;
    [SerializeField] float AsteroidDirection = 180f;
    [SerializeField] float AsteroidAngle = 25f;
    [SerializeField] Vector2 SpawnArea = new Vector2(1f, 1f);
    [SerializeField] float IntervalSecs = 1.0f;
    [SerializeField] float JitterSecs = 0.5f;

    void Start() {
        this.StartCoroutine(this.spawn());
    }

    void OnDrawGizmosSelected() {
        this.drawGizmosSpawnArea();
        this.drawGizmosSpawnDirection();
    }

    void drawGizmosSpawnArea() {
        Gizmos.color = Color.cyan;
        var spawnArea = this.spawnArea();
        Gizmos.DrawWireCube(this.transform.TransformPoint(spawnArea.center), spawnArea.size);
    }

    void drawGizmosSpawnDirection() {
        Gizmos.color = Color.yellow;
        GizmosExtensions.DrawArrow(this.transform.position, this.direction() * this.transform.localScale, Color.yellow, 0.05f);

        GizmosExtensions.DrawArrow(
            this.transform.position,
            Quaternion.AngleAxis(-this.AsteroidAngle / 2 + this.AsteroidDirection - 45f, Vector3.forward) * this.transform.localScale,
            Color.blue,
            0.05f);
        GizmosExtensions.DrawArrow(
            this.transform.position,
            Quaternion.AngleAxis(this.AsteroidAngle / 2 + this.AsteroidDirection - 45f, Vector3.forward) * this.transform.localScale, Color.blue,
            0.05f);
    }

    Rect spawnArea() {
        return new Rect(
            -(this.SpawnArea.x / 2f),
            -(this.SpawnArea.y / 2f),
            this.SpawnArea.x,
            this.SpawnArea.y
        );
    }

    Vector2 direction() {
        Vector3 direction = Quaternion.AngleAxis(this.AsteroidDirection, Vector3.forward) * Vector3.right;
        return direction.normalized;
    }

    IEnumerator spawn() {
        while (true) {
            var spawnPos = this.transform.TransformPoint(this.spawnArea().RandomPoint());

            var angle = Random.Range(-this.AsteroidAngle / 2 + 45f, this.AsteroidAngle / 2 + 45f);
            var rotation = Quaternion.AngleAxis(angle + this.AsteroidDirection - 45f, Vector3.forward);

            Projectile asteroid = Instantiate(this.AsteroidPrefab, spawnPos, Quaternion.identity);
            asteroid.Direction = angle + this.AsteroidDirection - 45f;

            var jitterSecs = Random.Range(-this.JitterSecs, this.JitterSecs);
            yield return new WaitForSeconds(this.IntervalSecs + jitterSecs);
        }
    }
}
