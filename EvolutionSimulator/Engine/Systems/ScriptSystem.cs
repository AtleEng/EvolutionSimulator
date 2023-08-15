using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using Core;
using EvolutionSim;

namespace Core
{
    public class ScriptSystem : GameSystem
    {
        public override void InnitSystem()
        {
            
        }
        public override void Update()
        {
            foreach (GameEntity gameEntity in ECSSystem.gameEntities)
            {
                foreach (Component component in gameEntity.components.Values)
                {
                    component.Update();
                }
            }
        }
    }
}