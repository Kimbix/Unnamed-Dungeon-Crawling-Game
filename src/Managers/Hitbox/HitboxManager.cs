using System;
using System.Collections.Generic;

namespace Unnamed_Dungeon_Crawling_Game;

public static class HitboxManager {
  // TODO: Change this to a QuadTree
  // Ref: https://en.wikipedia.org/wiki/Quadtree
  public static readonly List<SquareHitbox> HitboxList =
      new List<SquareHitbox>();

  public static void Update() {
    foreach (SquareHitbox i in HitboxList) {
      foreach (SquareHitbox j in HitboxList) {
        if (i.Equals(j)) {
          continue;
        }
        if (i.Intersects(j)) {
          Console.WriteLine("Collision");
        }
      }
    }
  }
}
