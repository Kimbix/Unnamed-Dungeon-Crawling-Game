namespace Unnamed_Dungeon_Crawling_Game;

public class RoomLayout {
  public enum Space {
    Null,
    Rock,
  }
  public readonly Space[,] SolidGrid = new Space[15, 9];
}
