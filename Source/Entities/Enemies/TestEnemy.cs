using System;
using Microsoft.Xna.Framework;

namespace Unnamed_Dungeon_Crawling_Game;

public class TestEnemy : Enemy {
  private Vector2 velocity;
  private const float _maxSpeed = 3.0f;
  private const float _acceleration = _maxSpeed / 12;
  private const float _decceleration = _maxSpeed / 10;

  public override void Added() {
    BoundBox.Min = Position;
    BoundBox.Max = new Vector2(Position.X + 64, Position.Y + 64);
    Console.WriteLine("INFO: Adding " + this.GetType().ToString() + " in " + Position.ToString());
  }

  public override void Update() {
    var playerList = EntityManager.Get<Player>();
    Player closestPlayer = null;
    foreach (Player player in playerList) {
      if (closestPlayer == null) { closestPlayer = player; }
    }

    // TODO: Handle the fact that there's no player a better way holy shit
    if (closestPlayer == null) { Console.WriteLine("WARNING: Closest player not found, not moving"); return; }

    var direction = closestPlayer.Position - Position;
    direction.Normalize();
    Position += direction;
    BoundBox = new RectangleHitbox(Position, Position + (BoundBox.Size));
  }
}
