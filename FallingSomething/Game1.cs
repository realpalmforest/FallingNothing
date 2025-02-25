using System;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FallingSomething;

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
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        SpriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        GameTime = gameTime;

        InputManager.Update();
        GameManager.Update();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(BackgroundColor);

        GameManager.Draw();

        base.Draw(gameTime);
    }

    public static bool NextBool(int chance) => Instance.Random.Next(chance) == 0;
}
