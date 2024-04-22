using UnityEngine;

public static class MainCameraHelper {
    public class CameraExtensionsContainer : MonoBehaviour {
        public Bounds WorldBounds { get => this.worldBounds; }
        public Rect WorldRect { get => this.worldRect; }

        private Bounds worldBounds;
        private Rect worldRect;

        void Update() {
            this.worldBounds = Camera.main.WorldBounds();
            this.worldRect = Camera.main.WorldRect();
        }
    }

    private static CameraExtensionsContainer cameraExtensionsContainer;

    [RuntimeInitializeOnLoadMethod]
    public static void Initialize() {
        if (cameraExtensionsContainer == null) {
            GameObject gameObject = new GameObject("CameraExtensionsContainer");
            cameraExtensionsContainer = gameObject.AddComponent<CameraExtensionsContainer>();
            UnityEngine.Object.DontDestroyOnLoad(gameObject);
        }
    }

    public static Bounds WorldBounds() => cameraExtensionsContainer.WorldBounds;
    public static Rect WorldRect() => cameraExtensionsContainer.WorldRect;
}
