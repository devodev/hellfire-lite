using Unity.VisualScripting;
using UnityEngine;

public class ShieldManager : MonoBehaviour {
    [SerializeField] KeyCode toggleKey = KeyCode.LeftControl;
    [SerializeField] GameObject ShieldPrefab;

    SwitchDebouncer toggle = new SwitchDebouncer();
    GameObject shield;

    void Update() {
        if (this.toggle.Ready(this.toggleKey)) {
            if (this.shield == null) {
                this.shield = Instantiate(this.ShieldPrefab, this.transform);
                this.shield.SetActive(true);
            } else {
                this.shield.GameObject().SetActive(!this.shield.GameObject().activeSelf);
            }
        }
    }
}
