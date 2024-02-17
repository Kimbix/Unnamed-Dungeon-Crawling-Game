using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Unnamed_Dungeon_Crawling_Game;

public static class SpriteManager {
  public readonly static Dictionary<string, Texture2D> LoadedSprites =
      new Dictionary<string, Texture2D> {};

  public static void LoadSprite(string path) {
    Texture2D temp_texture = Globals.Content.Load<Texture2D>(path);
    LoadedSprites.Add(path, temp_texture);
  }
}
