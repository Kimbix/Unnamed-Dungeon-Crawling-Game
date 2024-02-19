using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace Unnamed_Dungeon_Crawling_Game;

public struct WallHit {
  public Vector2 Pushout;
}

public static class EntityManager {

  // TODO: Add list for each type
  public static readonly List<Entity> EntityList = [];
  private static readonly List<Entity> _toAdd = [];
  private static readonly List<Entity> _toRemove = [];

  public static Entity Add(Entity instance) {
    _toAdd.Add(instance);
    return instance;
  }

  public static void Remove(Entity instance) { _toRemove.Add(instance); }

  private static void AddAndRemove() {
    if (_toAdd.Count > 0) {
      // Add all in list to entity list
      int addCount = _toAdd.Count;
      for (int i = 0; i < addCount; i++) {
        EntityList.Add(_toAdd[i]);
        _toAdd[i].Added(); // Code to execute when added
      }
      _toAdd.RemoveRange(0, addCount);
    }

    if (_toRemove.Count > 0) {
      // Remove all from remove list
      int removeCount = _toRemove.Count;
      for (int i = 0; i < removeCount; i++) {
        EntityList.Remove(_toAdd[i]);
        _toRemove[i].Removed(); // Code to execute when removed
      }
      _toRemove.RemoveRange(0, removeCount);
    }
  }

  public static void Update() {
    AddAndRemove();
    foreach (var entity in EntityList) {
      entity.Update();
    }
  }

  public static void Draw() {
    foreach (var entity in EntityList) {
      entity.Draw();
    }
  }

  public static void DebugDraw() {
    foreach (var entity in EntityList) {
      entity.DebugDraw();
    }
  }

  public static List<WallHit> SolidWallCheck(in RectangleHitbox box) {
    List<WallHit> hitlist = [];
    foreach (Entity solid in EntityList) {
      if (solid.GetType() == typeof(Player)) { continue; }
      if (solid.BoundBox.Intersects(box)) {
        WallHit hit;
        hit.Pushout = box.Center - solid.BoundBox.Center;
        hitlist.Add(hit);
      }
    }
    return hitlist;
  }
}
