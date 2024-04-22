using UnityEngine;

public class TimeDebouncer {
    float lastMs;

    public bool Ready(float delayMs, KeyCode key) {
        if (this.lastMs > delayMs / 1000 && Input.GetKey(key)) {
            this.lastMs = 0;
            return true;
        }
        this.lastMs += Time.deltaTime;
        return false;
    }
}

public class SwitchDebouncer {
    private bool isKeyPressed = false;

    public bool Ready(KeyCode key) {
        if (Input.GetKeyDown(key) && !this.isKeyPressed) {
            this.isKeyPressed = true;
            return true;
        }
        if (Input.GetKeyUp(key)) {
            this.isKeyPressed = false;
        }
        return false;
    }
}
