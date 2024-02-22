// XNA Framework Imports
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Unnamed_Dungeon_Crawling_Game;

// TODO: Add ability to draw lines

public static class Globals {
  public static SpriteBatch SpriteBatch;
  public static ContentManager Content;

  public static Texture2D Pixel;
}

public class Game1 : Game {
  private GraphicsDeviceManager _graphics;
  private SpriteBatch _spriteBatch;

  // TODO: REMOVE LATER
  public RoomLayout testLayout;
  public Floor testFloor;

  public Game1() {
    _graphics = new GraphicsDeviceManager(this);
    Content.RootDirectory = "Content";
    IsMouseVisible = true;
  }

  protected override void Initialize() {
    _graphics.PreferredBackBufferWidth = 1280;
    _graphics.PreferredBackBufferHeight = 720;
    _graphics.ApplyChanges();

    base.Initialize();
  }

  protected override void LoadContent() {
    _spriteBatch = new SpriteBatch(GraphicsDevice);
    Globals.SpriteBatch = _spriteBatch;
    Globals.Content = Content;

    Globals.Pixel = new Texture2D(GraphicsDevice, 1, 1);
    Globals.Pixel.SetData<Color>(new Color[] { Color.Cyan });

    // TODO: REMOVE LATER
    testLayout = new RoomLayout();
    testFloor = new Floor();
    for (int x = 0; x < 15; x++) {
      testLayout.SolidGrid[x, 0] = RoomLayout.Space.Rock;
      testLayout.SolidGrid[x, 8] = RoomLayout.Space.Rock;
    }
    for (int x = 2; x < 7; x++) {
      testLayout.SolidGrid[x, 2] = RoomLayout.Space.Rock;
    }
    for (int x = 8; x < 13; x++) {
      testLayout.SolidGrid[x, 2] = RoomLayout.Space.Rock;
    }
    for (int y = 0; y < 9; y++) {
      testLayout.SolidGrid[0, y] = RoomLayout.Space.Rock;
      testLayout.SolidGrid[14, y] = RoomLayout.Space.Rock;
    }
    testFloor.CreateRoom(testLayout);

    var player = EntityManager.Add(new Player());
    player.Position = new Vector2(300, 300);
    player.Texture = Globals.Content.Load<Texture2D>("GameObstacles/TestRock");
  }

  protected override void Update(GameTime gameTime) {
    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
        Keyboard.GetState().IsKeyDown(Keys.Escape))
      Exit();

    EntityManager.Update();

    base.Update(gameTime);
  }

  protected override void Draw(GameTime gameTime) {
    GraphicsDevice.Clear(Color.CornflowerBlue);

    Globals.SpriteBatch.Begin();

    EntityManager.Draw();
    EntityManager.DebugDraw();

    Globals.SpriteBatch.End();

    base.Draw(gameTime);
  }
}
