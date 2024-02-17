using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Unnamed_Dungeon_Crawling_Game;

/// <summary>
/// Class <c>Sprite</c> Sprite used for GameObjects
/// </summary>
public class Sprite {
  public Player _parent = null;

  private Vector2 _drawPosition = Vector2.Zero;
  public Vector2 Position {
    get => (_parent == null ? Vector2.Zero : _parent.Position) + _drawPosition;
    set => _drawPosition =
        (_parent == null ? Vector2.Zero : _parent.Position) + value;
  }

  // Texture2D is a class so when we assign it, its done by reference
  private Texture2D _texture;

  // When current frame is modified also change the properties of the texture
  // bounds
  private ushort _frame;
  public ushort Frame {
    get => _frame;
    set {
      if (value < 0) {
        Console.Error.WriteLine(
            "WARNING: Selected frame outside of sprite bounds");
        return;
      }
      if (value > this._horizontalFrames * this._verticalFrames) {
        if (_looping) {
          Frame = 0;
          return;
        }
        Console.Error.WriteLine(
            "WARNING: Selected frame outside of sprite bounds");
        return;
      }
      this._currentFrame.X =
          ((this._texture.Bounds.Width / this._horizontalFrames) * value) %
          this._texture.Bounds.Width;
      this._currentFrame.Y =
          ((this._texture.Bounds.Height / this._verticalFrames) * value) /
          this._texture.Bounds.Height;
      this._frame = value;
    }
  }

  // If framerate is changed reset the frame changer timer
  private byte _timer;
  private byte _framerate;
  public byte Framerate {
    get => _framerate;
    set {
      _framerate = value;
      _timer = (byte)(Framerate / 60);
    }
  }

  // Animated variable can only be set to true if _horizontalFrames or
  // _verticalFrames is more than 0
  private bool _animated;
  private ushort _horizontalFrames;
  private ushort _verticalFrames;

  // Can only be set to true if animated == true
  private bool _playing;
  public bool Playing {
    get => _playing;
    set {
      if (!_animated) {
        Console.Error.WriteLine("WARNING: Sprite is not animated, cannot play");
        _playing = false;
        return;
      }
      _playing = value;
      _timer = (byte)(Framerate / 60);
    }
  }

  // Can only be set to true if animated == true
  private bool _looping;
  public bool Looping {
    get => _looping;
    set {
      if (!_animated) {
        Console.Error.WriteLine("WARNING: Sprite is not animated, cannot loop");
        _playing = false;
        return;
      }
      _looping = value;
    }
  }

  private Rectangle _currentFrame;

  public Sprite(Texture2D texture, ushort horizontalFrames = 1,
                ushort verticalFrames = 1, byte framerate = 0,
                bool looping = false, bool playing = false) {

    this._texture = texture ?? throw new ArgumentException(
                                   "Parameter cannot be null", nameof(texture));
    if (horizontalFrames < 1) {
      throw new ArgumentException("Parameter cannot be smaller than 1",
                                  nameof(horizontalFrames));
    }
    this._horizontalFrames = horizontalFrames;

    if (verticalFrames < 1) {
      throw new ArgumentException("Parameter cannot be smaller than 1",
                                  nameof(verticalFrames));
    }
    this._verticalFrames = verticalFrames;

    this._currentFrame = new Rectangle(0, 0, texture.Width / horizontalFrames,
                                       texture.Height / verticalFrames);
    Frame = 0;
    Framerate = framerate;
    if (Framerate > 0) {
      _animated = true;
    }

    Looping = looping;
    Playing = playing;
  }

  public void Update() {
    if (!this._playing) {
      return;
    }
    if (this._looping) {
      Frame++;
    } else {
      this._playing = false;
    }
  }

  public void Play() { Playing = true; }
  public void Stop() { Playing = false; }

  public void Draw() {
    Globals.SpriteBatch.Draw(this._texture, Position, _currentFrame,
                             Color.White);
  }
}
