using UnityEngine;

public static class RectExtensions {
    public static Rect Expand(this Rect rect, float amount) {
        float halfAmount = amount * 0.5f;
        rect.xMin -= halfAmount;
        rect.xMax += halfAmount;
        rect.yMin -= halfAmount;
        rect.yMax += halfAmount;
        return rect;
    }

    public static Vector2 RandomPoint(this Rect rect) {
        return new Vector2(
            Random.Range(rect.min.x, rect.max.x),
            Random.Range(rect.min.y, rect.max.y)
        );
    }
}
