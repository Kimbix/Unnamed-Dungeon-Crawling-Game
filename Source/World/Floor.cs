using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Unnamed_Dungeon_Crawling_Game;

public class Floor
{
    public readonly List<RoomLayout> Layouts = [];

    public void CreateRoom(RoomLayout room)
    {
        for (int x = 0; x < 15; x++)
        {
            for (int y = 0; y < 9; y++)
            {
                switch (room.SolidGrid[x, y])
                {
                    case RoomLayout.Space.Rock:
                        var instance = EntityManager.Add(new Solid());
                        instance.Texture =
                            Globals.Content.Load<Texture2D>("GameObstacles/TestRock");
                        instance.Position = new Vector2(x * 64, y * 64);
                        break;
                }
            }
        }
    }
}
