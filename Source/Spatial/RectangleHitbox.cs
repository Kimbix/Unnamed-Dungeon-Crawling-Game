using Microsoft.Xna.Framework;

namespace Unnamed_Dungeon_Crawling_Game;

// Basically ripped straight from Celeste64 BoundingBox
// Reference:
// https://github.com/ExOK/Celeste64/blob/main/Source/Spatial/BoundingBox.cs

public record struct RectangleHitbox
(Vector2 Min, Vector2 Max) {
  public readonly Vector2 Center => (Min + Max) / 2;
  public readonly Vector2 Size => (Max - Min);

  public RectangleHitbox(Vector2 position, float size)
      : this(position - Vector2.One * size, position + Vector2.One * size) {}

  public readonly bool Contains(in Vector2 point) {
    return (point.X >= Min.X && point.X <= Max.X) &&
           (point.Y >= Min.Y && point.Y <= Max.Y);
  }

  public readonly bool Intersects(in RectangleHitbox box) {
    return (Max.X >= box.Min.X && Max.Y >= box.Min.Y) &&
           (Min.X <= box.Max.X && Min.Y <= box.Max.Y);
  }

  public static RectangleHitbox operator +(RectangleHitbox box,
                                           Vector2 offset) =>
      new(box.Min + offset, box.Max + offset);

  public static RectangleHitbox operator -(RectangleHitbox box,
                                           Vector2 offset) =>
      new(box.Min - offset, box.Max - offset);

  public readonly Vector2[] GetCorners() {
    return [
      new Vector2(Min.X, Min.Y), new Vector2(Min.X, Max.Y),
      new Vector2(Max.X, Min.Y), new Vector2(Max.X, Max.Y)
    ];
  }
}
