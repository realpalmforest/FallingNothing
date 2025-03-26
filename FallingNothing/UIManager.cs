using FallingNothing.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FallingNothing;

public static class UIManager
{
    public static SpriteFont Font1 { get; private set; }

    static UIManager()
    {
        ContentManager content = Game1.Instance.Content;

        Font1 = content.Load<SpriteFont>("Fonts/font1");
    }

    public static void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        spriteBatch.DrawString(Font1, $"FPS: {Utils.Fps}", new Vector2(10), Color.White);
        spriteBatch.End();
    }
}
