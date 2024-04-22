using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ItemSwitcher : MonoBehaviour {
    [SerializeField] KeyCode SwitchKey = KeyCode.F;

    List<GameObject> childObjects = new List<GameObject>();
    SwitchDebouncer switchDebounce = new SwitchDebouncer();
    int currentIndex = 0;

    void Start() {
        this.childObjects = this.transform.Cast<Transform>().Select(child => {
            child.GameObject().SetActive(false);
            return child.GameObject();
        }).ToList();
        this.selectObject(0);
    }

    void Update() {
        if (this.switchDebounce.Ready(this.SwitchKey)) {
            this.nextObject();
        }
    }

    void nextObject() {
        var next = (this.currentIndex + 1) % this.childObjects.Count;
        this.selectObject(next);
    }

    void selectObject(int index) {
        // Deselect previous object if any
        if (this.currentIndex != index && this.currentIndex < this.childObjects.Count) {
            this.childObjects[this.currentIndex].SetActive(false);
        }

        // Select new object
        if (index >= 0 && index < this.childObjects.Count) {
            this.childObjects[index].SetActive(true);
            this.currentIndex = index;
        }
    }
}
