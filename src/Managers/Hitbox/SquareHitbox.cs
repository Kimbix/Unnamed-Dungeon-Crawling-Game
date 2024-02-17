using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Unnamed_Dungeon_Crawling_Game;

public class SquareHitbox {
  public Player _parent = null;
  private Rectangle HitboxRectangle;
  private Texture2D DebugTexture;

  public bool Intersects(SquareHitbox rect) {
    if (HitboxRectangle.Intersects(rect.HitboxRectangle)) {
      DebugTexture = Globals.HitboxCollideDebugTexture;
      return true;
    }
    DebugTexture = Globals.HitboxDebugTexture;
    return false;
  }

  public Vector2 Position {
    get {
      if (_parent == null) {
        return new Vector2(HitboxRectangle.X, HitboxRectangle.Y);
      }
      return new Vector2(_parent.Position.X + HitboxRectangle.X,
                         _parent.Position.Y + HitboxRectangle.Y);
    }
    set {
      if (_parent == null) {
        HitboxRectangle.X = (int)value.X;
        HitboxRectangle.Y = (int)value.Y;
        return;
      }
      HitboxRectangle.X = (int)(value.X + _parent.Position.X);
      HitboxRectangle.Y = (int)(value.Y + _parent.Position.Y);
      return;
    }
  }

  public SquareHitbox(Vector2 Position, int Width, int Height) {
    HitboxRectangle =
        new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
  }

  public void DebugDraw() {
    Globals.SpriteBatch.Draw(texture: DebugTexture,
                             destinationRectangle: HitboxRectangle,
                             color: Color.White * 0.25f);
  }
}
