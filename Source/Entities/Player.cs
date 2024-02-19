using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Unnamed_Dungeon_Crawling_Game;

public class Player : Entity {
  private Vector2 velocity;

  public override void Update() {
    Move();
    BoundBox = new RectangleHitbox(Position, Position + (BoundBox.Size));
    Popout();
  }
  public override void Draw() {
    Globals.SpriteBatch.Draw(Texture, Position, Color.White);
  }

  public override void DebugDraw() {
    Globals.SpriteBatch.Draw(
        Globals.Pixel,
        new Rectangle((int)BoundBox.Min.X, (int)BoundBox.Min.Y,
                      (int)(BoundBox.Max.X - BoundBox.Min.X),
                      (int)(BoundBox.Max.Y - BoundBox.Min.Y)),
        Color.Cyan * 0.25f);
    Globals.SpriteBatch.Draw(
        texture: Globals.Pixel,
        destinationRectangle: new Rectangle(
            (int)BoundBox.Center.X, (int)BoundBox.Center.Y,
            Math.Abs((int)(velocity.Length() * 5)), 1),
        sourceRectangle: null, color: Color.Red * 0.25f,
        rotation: (float)Math.Atan2(velocity.Y, velocity.X),
        origin: Vector2.Zero,
        effects: Microsoft.Xna.Framework.Graphics.SpriteEffects.None,
        layerDepth: 1);
  }

  public override void Added() {
    BoundBox.Min = Position;
    BoundBox.Max = new Vector2(Position.X + 64, Position.Y + 64);
    Console.WriteLine("ADDING SOLID");
  }

  public void Move() {
    var kb = Keyboard.GetState();
    if (kb.IsKeyDown(Keys.D)) {
      velocity.X++;
    }
    if (kb.IsKeyDown(Keys.A)) {
      velocity.X--;
    }
    if (kb.IsKeyDown(Keys.W)) {
      velocity.Y--;
    }
    if (kb.IsKeyDown(Keys.S)) {
      velocity.Y++;
    }

    velocity = Vector2.Clamp(velocity, Vector2.One * -3, Vector2.One * 3);
    Position += velocity;
  }

  public void Popout() {
    List<WallHit> hitlist = EntityManager.SolidWallCheck(BoundBox);
    foreach (WallHit hit in hitlist) {
      if (hit.Pushout != Vector2.Zero && Math.Abs(hit.Pushout.X) >= Math.Abs(hit.Pushout.Y)) {
        Position.X += Math.Sign(hit.Pushout.X) * Math.Abs((Math.Abs(hit.Pushout.X) - Math.Abs(BoundBox.Size.X)));
        velocity.X = 0;
      }
      if (hit.Pushout != Vector2.Zero && Math.Abs(hit.Pushout.X) <= Math.Abs(hit.Pushout.Y)) {
        Position.Y += Math.Sign(hit.Pushout.Y) * Math.Abs((Math.Abs(hit.Pushout.Y) - Math.Abs(BoundBox.Size.Y)));
        velocity.Y = 0;
      }
    }
  }
}
