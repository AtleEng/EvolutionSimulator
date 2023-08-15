using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using Core;
using EvolutionSim;

namespace EvolutionSim
{
    public class SpriteRenderer : Component
    {
        public Action<Color, Vector2, Vector2> drawingAction = Circle;

        public Color colorTint = Color.WHITE;

        public static void Circle(Color color, Vector2 pos, Vector2 size)
        {
            Vector2 p = WorldSpace.ConvertToWorldPositon(pos);
            Vector2 s = WorldSpace.ConvertToWorldSize(size);

            Raylib.DrawEllipse(
            (int)p.X,
            (int)p.Y,
            (int)s.X,
            (int)s.Y,
            color
            );
        }
        public static void Square()
        {

        }
    }
    public static class Camera
    {
        public static Vector2 position = new();
        public static float zoom = 1;
    }
}
