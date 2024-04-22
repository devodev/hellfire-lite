using System.Collections;
using UnityEngine;

public class Damageable : MonoBehaviour {
    [SerializeField] GameObject HitPrefab;
    [SerializeField] GameObject DeathPrefab;
    [SerializeField] float DeathDelaySec;

    public float MaxHealth = 100f;
    public float Health { get; private set; }

    public void TakeDamage(float amount, Vector3 position) {
        this.showHit(position);

        this.Health -= amount;

        if (this.Health <= 0f) {
            this.Health = 0f;
            this.die();
        }
    }

    void Start() {
        this.Health = this.MaxHealth;
    }

    void showHit(Vector3 position) {
        if (this.HitPrefab != null) {
            Instantiate(this.HitPrefab, position, Quaternion.identity);
        }
    }

    void die() {
        this.GetComponentInChildren<Renderer>().enabled = false;
        this.StartCoroutine(this.delayDestroy(this.DeathDelaySec));
        if (this.DeathPrefab != null) {
            Instantiate(this.DeathPrefab, this.transform.position, Quaternion.identity);
        }
    }

    IEnumerator delayDestroy(float seconds) {
        yield return new WaitForSeconds(seconds);
        Destroy(this.gameObject);
    }
}
