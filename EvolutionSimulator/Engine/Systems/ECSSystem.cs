using System.Collections.Generic;
using System.Numerics;
using Core;
using EvolutionSim;

namespace Core
{
    public static class ECSSystem
    {
        static public List<GameEntity> gameEntities = new();
        static public Dictionary<Type, GameSystem> systems = new();

        static public List<GameEntity> entitiesToAdd = new();
        static public List<GameEntity> entitiesToRemove = new();

        public static void Innit()
        {
            systems.Add(typeof(ScriptSystem), new ScriptSystem());
            systems.Add(typeof(PhysicsSystem), new PhysicsSystem());
            systems.Add(typeof(RenderSystem), new RenderSystem());

            systems[typeof(ScriptSystem)].InnitSystem();
            systems[typeof(PhysicsSystem)].InnitSystem();
            systems[typeof(RenderSystem)].InnitSystem();
        }
        public static void UpdateSystems()
        {
            // Uppdate all the systems in the right order
            systems[typeof(ScriptSystem)].Update();
            systems[typeof(PhysicsSystem)].Update();
            systems[typeof(RenderSystem)].Update();

            // Add and remove games entities
            foreach (var entity in entitiesToAdd)
            {
                gameEntities.Add(entity);
                entity.Start();
            }
            entitiesToAdd.Clear();
            foreach (var entity in entitiesToRemove)
            {
                gameEntities.Remove(entity);
            }
            entitiesToRemove.Clear();
        }
    }
}
