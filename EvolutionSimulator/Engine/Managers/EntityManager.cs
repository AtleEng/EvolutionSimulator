using System.Collections.Generic;
using System.Numerics;
using Core;
using EvolutionSim;

namespace EvolutionSim
{
    //handles all entities for the components
    public static class EntityManager
    {
        public static void SpawnEntity(GameEntity entity, Vector2 spawnPoint, Vector2 size, float rotation)
        {
            SpawnEntity(entity, spawnPoint, size, rotation, null);
        }
        public static void SpawnEntity(GameEntity entity, Vector2 spawnPoint, Vector2 size, float rotation, GameEntity? parent)
        {
            string name = "GameEntity";
            SpawnEntity(entity, spawnPoint, size, rotation, parent, name);
        }
        public static void SpawnEntity(GameEntity gameEntity, Vector2 spawnPoint, Vector2 size, float rotation, GameEntity? parent, string name)
        {
            System.Console.WriteLine("Spawning entity");

            gameEntity.name = name;
            gameEntity.position = spawnPoint;
            gameEntity.size = size;
            gameEntity.rotation = rotation;
            gameEntity.parent = parent;

            foreach (object component in gameEntity.components.Values)
            {
                if (component is PhysicsBody physicsBody)
                {
                    physicsBody.position = gameEntity.position;
                }
            }

            ECSSystem.entitiesToAdd.Add(gameEntity);
        }

    }
}