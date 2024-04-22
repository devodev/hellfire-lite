using UnityEngine;

public static class CameraExtensions {
    public static Bounds WorldBounds(this Camera camera) {
        Vector3 bottomLeft = camera.ViewportToWorldPoint(new Vector3(0, 0, camera.nearClipPlane));
        Vector3 topRight = camera.ViewportToWorldPoint(new Vector3(1, 1, camera.nearClipPlane));

        Bounds bounds = new Bounds();
        bounds.SetMinMax(bottomLeft, topRight);

        return bounds;
    }

    public static Rect WorldRect(this Camera camera) {
        var bounds = WorldBounds(camera);
        return Rect.MinMaxRect(bounds.min.x, bounds.min.y, bounds.max.x, bounds.max.y);
    }
}
