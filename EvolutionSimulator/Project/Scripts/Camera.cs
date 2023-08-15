using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using EvolutionSim;
public class CameraController : Component
{
    readonly float cameraMoveSpeed = 3;
    Random rnd = new Random();
    public override void Update()
    {
        CameraControll();

        if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_BUTTON_LEFT))
        {
            Vector2 mousePosition = WorldSpace.GetVirtualMousePos();

            float ballSize = ((float)rnd.Next(5, 50)) / 10;

            GameEntity gameEntity1 = new();

            PhysicsBody physicsBody = new()
            {
                velocity = new Vector2(rnd.Next(-5, 5), rnd.Next(-5, 5)),
                mass = ballSize,
                position = mousePosition
            };
            gameEntity1.AddComponent<PhysicsBody>(physicsBody);

            SpriteRenderer spriteRenderer = new SpriteRenderer
            {
                drawingAction = SpriteRenderer.Circle
            };
            gameEntity1.AddComponent<SpriteRenderer>(spriteRenderer);

            EntityManager.SpawnEntity(gameEntity1, mousePosition, new Vector2(ballSize, ballSize), 0);
        }
    }
    void CameraControll()
    {
        if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
        {
            Camera.position.Y -= cameraMoveSpeed * Time.deltaTime / Camera.zoom;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
        {
            Camera.position.Y += cameraMoveSpeed * Time.deltaTime / Camera.zoom;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
        {
            Camera.position.X += cameraMoveSpeed * Time.deltaTime / Camera.zoom;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
        {
            Camera.position.X -= cameraMoveSpeed * Time.deltaTime / Camera.zoom;
        }
        Camera.zoom += Raylib.GetMouseWheelMove() / 100;

        Camera.zoom = Math.Clamp(Camera.zoom, 0.02f, 5);
    }
}