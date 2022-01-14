using UnityEngine;

public static class VectorExtensions
{
    public static string ToExtraString(this Vector2 vector)
        => $"Vec2({vector.x}, {vector.y})";

    public static string ToExtraString(this Vector3 vector)
        => $"Vec3({vector.x}, {vector.y}, {vector.z})";

    public static string ToExtraString(this Vector4 vector)
        => $"Vec4({vector.w}, {vector.x}, {vector.y}, {vector.z})";
}
