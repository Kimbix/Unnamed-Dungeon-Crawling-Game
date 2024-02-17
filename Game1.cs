using System;

// XNA Framework Imports
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Unnamed_Dungeon_Crawling_Game;

public static class Globals {
  public static SpriteBatch SpriteBatch;
  public static GraphicsDevice Graphics;
  public static ContentManager Content;

  public static Texture2D HitboxDebugTexture;
  public static Texture2D HitboxCollideDebugTexture;
}

public static class Input {
  public static KeyboardState state;
  public static void Update() { state = Keyboard.GetState(); }
}

public class Rock : IHasHitbox {
  public Sprite rockSprite;
  public Vector2 position;

  private SquareHitbox _hitbox;
  public SquareHitbox Hitbox {
    get => _hitbox;
    set => _hitbox = value;
  }
  public void CreateHitbox() {
    Hitbox = new SquareHitbox(position, 64, 64);
    HitboxManager.HitboxList.Add(Hitbox);
  }

  public void SetSprite(Sprite sprite) {
    rockSprite = sprite;
    rockSprite.Position = position;
  }

  public void Draw() { rockSprite.Draw(); }
}

public class Player : IHasHitbox {
  public Sprite playerSprite;
  public Vector2 Position;
  private Vector2 Velocity;

  private SquareHitbox _hitbox;
  public SquareHitbox Hitbox {
    get => _hitbox;
    set => _hitbox = value;
  }

  public void CreateHitbox() {
    Hitbox = new SquareHitbox(Position, 64, 64);
    Hitbox._parent = this;
    HitboxManager.HitboxList.Add(Hitbox);
  }

  private float _acceleration = 0.25f;
  private float _decceleration = 1.25f;
  private float _maxSpeed = 5f;

  public void SetSprite(Sprite sprite) {
    playerSprite = sprite;
    sprite._parent = this;
  }

  public void Movement() {
    if (Input.state.IsKeyDown(Keys.Right)) {
      Velocity.X += _acceleration;
    } else if (Input.state.IsKeyDown(Keys.Left)) {
      Velocity.X -= _acceleration;
    } else {
      Velocity.X = Math.Sign(Velocity.X) *
                   Math.Max(0, Math.Abs(Velocity.X) - _decceleration);
    }

    if (Input.state.IsKeyDown(Keys.Up)) {
      Velocity.Y -= _acceleration;
    } else if (Input.state.IsKeyDown(Keys.Down)) {
      Velocity.Y += _acceleration;
    } else {
      Velocity.Y = Math.Sign(Velocity.Y) *
                   Math.Max(0, Math.Abs(Velocity.Y) - _decceleration);
    }

    Velocity = Vector2.Clamp(Velocity, Vector2.One * -_maxSpeed,
                             Vector2.One * _maxSpeed);
    Position += Velocity;
    _hitbox.Position =
        Vector2.Zero; // TODO: Find a better way to update hitbox position
  }

  public void Update() {
    Movement();
    playerSprite?.Update();
  }

  public void Draw() { playerSprite?.Draw(); }
}

public class Game1 : Game {
  private GraphicsDeviceManager _graphics;
  private SpriteBatch _spriteBatch;

  public Player test = new Player();
  public Rock rockInstance = new Rock();
  public Rock newrock = new Rock();

  public Game1() {
    _graphics = new GraphicsDeviceManager(this);
    Content.RootDirectory = "Content";
    IsMouseVisible = true;
  }

  protected override void Initialize() {
    Globals.Graphics = GraphicsDevice;
    Globals.Content = Content;

    Globals.HitboxDebugTexture = new Texture2D(Globals.Graphics, 1, 1);
    Globals.HitboxDebugTexture.SetData<Color>(new Color[] { Color.Cyan });

    Globals.HitboxCollideDebugTexture = new Texture2D(Globals.Graphics, 1, 1);
    Globals.HitboxCollideDebugTexture.SetData<Color>(new Color[] { Color.Red });
    base.Initialize();
  }

  protected override void LoadContent() {
    _spriteBatch = new SpriteBatch(GraphicsDevice);
    Globals.SpriteBatch = _spriteBatch;

    SpriteManager.LoadSprite("Explosion");
    SpriteManager.LoadSprite("GameObstacles/TestRock");

    test.SetSprite(new Sprite(SpriteManager.LoadedSprites["Explosion"], 17, 1,
                              5, true, true));
    rockInstance.SetSprite(
        new Sprite(SpriteManager.LoadedSprites["GameObstacles/TestRock"]));
    newrock.SetSprite(
        new Sprite(SpriteManager.LoadedSprites["GameObstacles/TestRock"]));
    newrock.position = new Vector2(72, 72);

    test.CreateHitbox();
    rockInstance.CreateHitbox();
    newrock.CreateHitbox();
  }

  protected override void Update(GameTime gameTime) {
    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
        Keyboard.GetState().IsKeyDown(Keys.Escape))
      Exit();

    Input.Update();
    test.Update();
    HitboxManager.Update();

    base.Update(gameTime);
  }

  protected override void Draw(GameTime gameTime) {
    GraphicsDevice.Clear(Color.CornflowerBlue);
    _spriteBatch.Begin();

    test.Draw();
    test.Hitbox.DebugDraw();

    rockInstance.Draw();
    rockInstance.Hitbox.DebugDraw();

    newrock.Draw();
    newrock.Hitbox.DebugDraw();

    _spriteBatch.End();
    base.Draw(gameTime);
  }
}
