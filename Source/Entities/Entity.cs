using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Unnamed_Dungeon_Crawling_Game;

public class Entity {
  public Vector2 Position;
  public Texture2D Texture;
  public RectangleHitbox BoundBox;
  public bool Collidable = true;

  public virtual void Update() {}
  public virtual void Draw() {}
  public virtual void DebugDraw() {}
  public virtual void Added() {}
  public virtual void Removed() {}
}
