using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using Core;
using EvolutionSim;

namespace EvolutionSim
{
    public static class Time
    {
        //deltaTime variabler
        static float oldTime = 0;
        static float newTime = 0;
        //detta är deltaTime variabeln man kan ändas få (get) värdet eftersom den har private set
        public static float deltaTime { get; private set; }

        public static void UpdateDeltaTime()
        {
            //räkna ut delta time
            //------------------------------------------------
            //sätter oldTime till förra framens tid
            oldTime = newTime;
            //sätter newTime till denna frames tid
            newTime = (float)Raylib.GetTime();
            //subrahera newTime med oldTime för att få skillnaden i tid mellan framen
            deltaTime = newTime - oldTime;
            //------------------------------------------------
        }
    }

    public static class WorldSpace
    {
        static RenderSystem RenderSystem => (RenderSystem)ECSSystem.systems[typeof(RenderSystem)];
        public static Vector2 GetVirtualMousePos() // Uppdatera virtuella musen (låst till spelfönstret)
        {

            // Virtual mouse position
            Vector2 virtualMousePosition;

            // Get the mouse position
            Vector2 mousePosition = Raylib.GetMousePosition();

            // Calculate the virtual mouse position adjusted to the game window
            virtualMousePosition.X = (mousePosition.X - (Raylib.GetScreenWidth() - (WindowSettings.gameScreenWidth * RenderSystem.scale)) * 0.5f) / RenderSystem.scale;
            virtualMousePosition.Y = (mousePosition.Y - (Raylib.GetScreenHeight() - (WindowSettings.gameScreenHeight * RenderSystem.scale)) * 0.5f) / RenderSystem.scale;

            // Clamp the virtual mouse position to the game window boundaries
            virtualMousePosition.X = Math.Clamp(virtualMousePosition.X, 0f, WindowSettings.gameScreenWidth);
            virtualMousePosition.Y = Math.Clamp(virtualMousePosition.Y, 0f, WindowSettings.gameScreenHeight);

            virtualMousePosition.X = PixelsToBaseUnits((virtualMousePosition.X - WindowSettings.gameScreenWidth / 2) / Camera.zoom);
            virtualMousePosition.Y = PixelsToBaseUnits((virtualMousePosition.Y - WindowSettings.gameScreenHeight / 2) / Camera.zoom);

            virtualMousePosition += Camera.position;

            return virtualMousePosition;
        }

        public static int BaseUnitsToPixels(float units)
        {
            int pixels = (int)(units * 100);
            return pixels;
        }
        public static float PixelsToBaseUnits(float pixels)
        {
            float units = (float)pixels / 100;
            return units;
        }
        public static Vector2 ConvertToWorldPositon(Vector2 v)
        {
            v.X = (v.X - Camera.position.X) * 100 * Camera.zoom + WindowSettings.gameScreenWidth / 2;
            v.Y = (v.Y - Camera.position.Y) * 100 * Camera.zoom + WindowSettings.gameScreenHeight / 2;

            return v;
        }
        public static Vector2 ConvertToWorldSize(Vector2 v)
        {
            return v * Camera.zoom * 100;
        }
    }

    public static class WindowSettings
    {
        readonly public static int startWindowWidth = 800;
        readonly public static int startWindowHeight = 450;

        public static int gameScreenWidth = 1200;
        public static int gameScreenHeight = 900;
    }
}