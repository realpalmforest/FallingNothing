using Microsoft.Xna.Framework;

namespace FallingNothing.Utility;

public static class Utils
{
    public static int Fps { get; private set; } = 0;

    private static int frameCount = 0;
    private static double elapsedTime = 0;

    //public static void LoadContent(ContentManager content)
    //{

    //}

    public static void UpdateFps(GameTime gameTime)
    {
        elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;
        frameCount++;

        if (elapsedTime >= 1.0) // Update every second
        {
            Fps = frameCount;
            frameCount = 0;
            elapsedTime = 0;
        }
    }

    public static bool NextBool(int possibilites)
    {
        return Game1.Instance.Random.Next(possibilites) == 0;
    }
}
