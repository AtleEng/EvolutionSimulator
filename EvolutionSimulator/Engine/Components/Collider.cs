using EvolutionSim;
using System.Numerics;
using System.Collections.Generic;

namespace EvolutionSim
{
    public class ColliderComponent : Component
    {
        Vector2[] worldPoints;
        Vector2 offsetPos;
        float angle;
        Vector2[] originPoints;
        bool isOverlaping;
    }

}