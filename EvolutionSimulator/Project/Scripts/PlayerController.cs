using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using EvolutionSim;
public class PlayerController : Component
{
    readonly float moveSpeed = 3;
    public override void Update()
    {
        if (Raylib.IsKeyDown(KeyboardKey.KEY_UP))
        {
            parentEntity.position.Y -= moveSpeed * Time.deltaTime;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_DOWN))
        {
            parentEntity.position.Y += moveSpeed * Time.deltaTime;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
        {
            parentEntity.position.X += moveSpeed * Time.deltaTime;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
        {
            parentEntity.position.X -= moveSpeed * Time.deltaTime;
        }
    }
}