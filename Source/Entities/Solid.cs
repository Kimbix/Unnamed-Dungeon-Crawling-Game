using System;
using Microsoft.Xna.Framework;

namespace Unnamed_Dungeon_Crawling_Game;

public class Solid : Entity {
  public override void DebugDraw() {
    Globals.SpriteBatch.Draw(
        Globals.Pixel,
        new Rectangle((int)BoundBox.Min.X, (int)BoundBox.Min.Y,
                      (int)(BoundBox.Max.X - BoundBox.Min.X),
                      (int)(BoundBox.Max.Y - BoundBox.Min.Y)),
        Color.Cyan * 0.25f);
  }

  public override void Added() {
    BoundBox.Min = Position;
    BoundBox.Max = new Vector2(Position.X + 64, Position.Y + 64);
    System.Console.WriteLine("INFO: Adding " + this.GetType().ToString() + " in " + Position.ToString());
  }
}
