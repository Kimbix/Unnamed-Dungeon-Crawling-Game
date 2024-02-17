namespace Unnamed_Dungeon_Crawling_Game;

public interface IHasHitbox {
  SquareHitbox Hitbox { get; set; }
  public void CreateHitbox();
  public void Collide();
}
