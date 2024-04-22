using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LaserGun : MonoBehaviour {
    [SerializeField] Projectile ProjectilePrefab;
    [SerializeField] KeyCode ShootKey = KeyCode.Space;
    [Range(10f, 1000f)]
    [SerializeField] float ShootDelayMs = 80f;
    [SerializeField] float ProjectileSpeed = 6f;
    [SerializeField] Color ProjectileTint = Color.white;

    List<Transform> projectileSources = new List<Transform>();
    float lastShotPeriod;
    TimeDebouncer shootDebounce = new TimeDebouncer();
    Vector3 velocity;

    void Start() {
        this.projectileSources = this.transform.Cast<Transform>().Select(child => child.transform).ToList();
    }

    void Update() {
        if (this.shootDebounce.Ready(this.ShootDelayMs, this.ShootKey)) {
            this.shoot();
        }
    }

    void shoot() {
        foreach (Transform transform in this.projectileSources) {
            Projectile laser = Instantiate(this.ProjectilePrefab, transform.position, transform.rotation);
            laser.Speed = this.ProjectileSpeed;
            transform.rotation.ToAngleAxis(out laser.Direction, out _);
            laser.Tint = this.ProjectileTint;
        }
    }
}
