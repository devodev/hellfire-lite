using UnityEngine;

public class MathHelper {
    public static Vector2 ComputeCentroid(Vector2[] points) {
        float centroidX = 0;
        float centroidY = 0;

        foreach (Vector2 point in points) {
            centroidX += point.x;
            centroidY += point.y;
        }

        centroidX /= points.Length;
        centroidY /= points.Length;

        return new Vector2(centroidX, centroidY);
    }
}
