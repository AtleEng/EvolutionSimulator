using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using Core;
using EvolutionSim;

namespace Core
{
    public static class CoreLogic
    {
        public static bool shouldClose;

        //When the program starts
        public static void OnProgramStart()
        {
            ECSSystem.Innit();

            GameEntity player = new();
            
            player.AddComponent<PlayerController>(new PlayerController());
            player.AddComponent<ColliderComponent>(new ColliderComponent());
            EntityManager.SpawnEntity(player, Vector2.Zero, Vector2.One, 0);

            GameEntity camera = new();
            camera.AddComponent<CameraController>(new CameraController());
            EntityManager.SpawnEntity(camera, Vector2.Zero, Vector2.One, 0);

            while (shouldClose == false)
            {
                Time.UpdateDeltaTime();

                ECSSystem.UpdateSystems();
            }
        }
    }
}