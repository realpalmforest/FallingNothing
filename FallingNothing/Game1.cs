using FallingNothing.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace FallingNothing;

public class Game1 : Game
{
    public static Game1 Instance;

    public GraphicsDeviceManager Graphics;
    public SpriteBatch SpriteBatch;

    public GameTime GameTime;

    public int WindowWidth { get => Graphics.PreferredBackBufferWidth; }
    public int WindowHeight { get => Graphics.PreferredBackBufferHeight; }

    public Color BackgroundColor = Color.CornflowerBlue;

    public Random Random = new Random();


    public Game1()
    {
        Graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        Instance = this;

        IsFixedTimeStep = false;
        Graphics.SynchronizeWithVerticalRetrace = false;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        SpriteBatch = new SpriteBatch(GraphicsDevice);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        GameTime = gameTime;

        InputManager.Update();
        GameManager.Update();

        Utils.UpdateFps(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(BackgroundColor);

        GameManager.Draw();

        UIManager.Draw(SpriteBatch);
    }
}
