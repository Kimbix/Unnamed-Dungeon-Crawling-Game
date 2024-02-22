using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Unnamed_Dungeon_Crawling_Game;

public class Player : Entity {
  private Vector2 velocity;
  private const float _maxSpeed = 5.0f;
  private const float _acceleration = _maxSpeed / 12;
  private const float _decceleration = _maxSpeed / 10;

  public override void Update() {
    Move();
    BoundBox = new RectangleHitbox(Position, Position + (BoundBox.Size));
    Popout();
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
    System.Console.WriteLine("INFO: Adding " + this.GetType().ToString() + " in " + Position.ToString());
  }

  public void Move() {
    var kb = Keyboard.GetState();
    if (kb.IsKeyDown(Keys.D) || kb.IsKeyDown(Keys.A)) {
      if (kb.IsKeyDown(Keys.D)) {
        velocity.X += _acceleration;
      }
      if (kb.IsKeyDown(Keys.A)) {
        velocity.X -= _acceleration;
      }
    } else {
    velocity.X = MathF.Sign(velocity.X) *
                   MathF.Max(MathF.Abs(velocity.X) - _decceleration, 0);
    }

    if (kb.IsKeyDown(Keys.W) || kb.IsKeyDown(Keys.S)) {
      if (kb.IsKeyDown(Keys.W)) {
        velocity.Y -= _acceleration;
      }
      if (kb.IsKeyDown(Keys.S)) {
        velocity.Y += _acceleration;
      }
    } else {
      velocity.Y = MathF.Sign(velocity.Y) *
                   MathF.Max(MathF.Abs(velocity.Y) - _decceleration, 0);
    }

    velocity = Vector2.Clamp(velocity, Vector2.One * -_maxSpeed,
                             Vector2.One * _maxSpeed);
    Position += velocity;
  }

  public void Popout() {
    // TODO: Collisions work mostly fine, but could use some refinement
    List<WallHit> hitlist = EntityManager.SolidWallCheck(BoundBox);
    foreach (WallHit hit in hitlist) {
      if (hit.Pushout != Vector2.Zero &&
          Math.Abs(hit.Pushout.X) > Math.Abs(hit.Pushout.Y)) {
        Position.X +=
            Math.Sign(hit.Pushout.X) *
            Math.Abs((Math.Abs(hit.Pushout.X) - Math.Abs(BoundBox.Size.X)));
        velocity.X = 0;
      }
      if (hit.Pushout != Vector2.Zero &&
          Math.Abs(hit.Pushout.X) < Math.Abs(hit.Pushout.Y)) {
        Position.Y +=
            Math.Sign(hit.Pushout.Y) *
            Math.Abs((Math.Abs(hit.Pushout.Y) - Math.Abs(BoundBox.Size.Y)));
        velocity.Y = 0;
      }

      if (hit.Entity.Collidable && hit.Entity.GetType().IsSubclassOf(typeof(Enemy))) {
        EntityManager.Remove(this);
      }
    }
  }
}
