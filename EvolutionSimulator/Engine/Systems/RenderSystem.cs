using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using Core;
using EvolutionSim;

namespace Core
{
    public class RenderSystem : GameSystem
    {
        public float scale;
        int offsetX;
        int offsetY;

        RenderTexture2D target = new();

        float gameScreenWidth;
        float gameScreenHeight;

        public override void InnitSystem()
        {
            Raylib.SetConfigFlags(ConfigFlags.FLAG_WINDOW_RESIZABLE);
            Raylib.SetConfigFlags(ConfigFlags.FLAG_VSYNC_HINT);

            Raylib.InitWindow(WindowSettings.startWindowWidth, WindowSettings.startWindowHeight, "Game Window");

            Raylib.SetWindowMinSize(400, 300);

            Raylib.SetTargetFPS(100);

            Raylib.SetExitKey(KeyboardKey.KEY_NULL);

            target = Raylib.LoadRenderTexture(WindowSettings.gameScreenWidth, WindowSettings.gameScreenHeight);
            SetValuesOfWindow();

        }
        public override void Update()
        {
            if (Raylib.IsWindowResized())
            {
                SetValuesOfWindow();
            }

            Raylib.BeginTextureMode(target);
            Raylib.ClearBackground(new Color(41, 189, 193, 255));
            //Render all sprites
            RenderAll();
            Raylib.EndTextureMode();

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.BLACK);

            Raylib.DrawTexturePro(target.texture,
    new Rectangle(0.0f, 0.0f, (float)target.texture.width, (float)-target.texture.height),
    new Rectangle(offsetX, offsetY, gameScreenWidth * scale, gameScreenHeight * scale),
    new Vector2(0, 0), 0.0f, Color.WHITE);

            Raylib.EndDrawing();

            if (CoreLogic.shouldClose)
            {
                Raylib.UnloadRenderTexture(target);
                Raylib.CloseWindow();
            }

            if (Raylib.WindowShouldClose())
            {
                CoreLogic.shouldClose = true;
            }
        }

        void RenderAll()
        {
            //render all entities's spriteRenderers
            foreach (GameEntity gameEntity in ECSSystem.gameEntities)
            {
                foreach (object component in gameEntity.components.Values)
                {
                    if (component is SpriteRenderer renderComponent)
                    {
                        renderComponent.drawingAction(renderComponent.colorTint, gameEntity.position, gameEntity.size);
                    }
                }
            }
            Raylib.DrawText($"GameEntitys:{ECSSystem.gameEntities.Count}\nFPS:{Raylib.GetFPS()}", 20, 20, 20, Color.RAYWHITE);
        }
        void SetValuesOfWindow()
        {
            gameScreenWidth = WindowSettings.gameScreenWidth;
            gameScreenHeight = WindowSettings.gameScreenHeight;

            float screenAspectRatio = (float)Raylib.GetScreenWidth() / Raylib.GetScreenHeight();
            float gameAspectRatio = (float)gameScreenWidth / gameScreenHeight;

            if (screenAspectRatio > gameAspectRatio)
            {
                // Window is wider than the game screen
                scale = (float)Raylib.GetScreenHeight() / gameScreenHeight;
                offsetX = (int)((Raylib.GetScreenWidth() - (gameScreenWidth * scale)) * 0.5f);
                offsetY = 0;
            }
            else
            {
                // Window is taller than the game screen
                scale = (float)Raylib.GetScreenWidth() / gameScreenWidth;
                offsetX = 0;
                offsetY = (int)((Raylib.GetScreenHeight() - (gameScreenHeight * scale)) * 0.5f);
            }
        }
    }
}