using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using Core;
using EvolutionSim;

namespace Core
{
    public class PhysicsSystem : GameSystem
    {
        List<PhysicsBody> physicsBodies = new();
        int numberOfSubstep = 3;
        public override void InnitSystem()
        {

        }
        public override void Update()
        {
            //cashing
            float dT = Time.deltaTime;

            physicsBodies.Clear();
            foreach (GameEntity gameEntity in ECSSystem.gameEntities)
            {
                foreach (object component in gameEntity.components.Values)
                {
                    if (component is PhysicsBody physicsComponent)
                    {
                        physicsBodies.Add(physicsComponent);
                    }
                }
            }
            //---Forces---

            //Apply Forces

            //Uppdate Velocity and Position
            foreach (PhysicsBody physicsBody in physicsBodies)
            {
                // Calculate viscosity force
                Vector2 viscosityForce = -physicsBody.velocity * physicsBody.viscosity;

                // Calculate acceleration including viscosity
                Vector2 viscosityAcceleration = viscosityForce / physicsBody.mass;
                Vector2 totalAcceleration = physicsBody.acceleration + viscosityAcceleration;

                // Update velocity and position
                physicsBody.velocity += totalAcceleration + new Vector2(0, physicsBody.gravity) * dT;
                physicsBody.position += physicsBody.velocity * dT;
            }
            dT /= numberOfSubstep;
            for (var i = 0; i < numberOfSubstep; i++)
            {
                //---Collisions---

                //Broad Phase

                //Narrow Phase
                SolveCollisions();
                ApplyConstraint();
            }
            //Uppdate parent
            foreach (PhysicsBody physicsBody in physicsBodies)
            {
                physicsBody.parentEntity.position = physicsBody.position;
            }
        }

        void SolveCollisions()
        {
            for (int i = 0; i < physicsBodies.Count; i++)
            {
                for (int j = i + 1; j < physicsBodies.Count; j++)
                {
                    float COR = 1 - (physicsBodies[i].elastisity + physicsBodies[j].elastisity) / 2.0f;
                    // Inside the collision resolution loop
                    float dist = Vector2.Distance(physicsBodies[i].position, physicsBodies[j].position);
                    float minDist = physicsBodies[i].radius * physicsBodies[i].parentEntity.size.X + physicsBodies[j].radius * physicsBodies[j].parentEntity.size.X;

                    if (dist <= minDist)
                    {
                        // Calculate relative velocity and contact normal
                        Vector2 relativeVelocity = physicsBodies[i].velocity - physicsBodies[j].velocity;
                        Vector2 contactNormal = Vector2.Normalize(physicsBodies[i].position - physicsBodies[j].position);

                        // Calculate impulse magnitude
                        float impulseMagnitude = -(1 + COR) * Vector2.Dot(relativeVelocity, contactNormal);
                        impulseMagnitude /= 1.0f / physicsBodies[i].mass + 1.0f / physicsBodies[j].mass;

                        // Apply impulses
                        Vector2 impulse = impulseMagnitude * contactNormal;
                        physicsBodies[i].velocity += impulse / physicsBodies[i].mass;
                        physicsBodies[j].velocity -= impulse / physicsBodies[j].mass;

                        // Position correction
                        float penetrationDepth = minDist - dist;
                        Vector2 correction = penetrationDepth * contactNormal;

                        // Apply position correction using radius
                        float radiusSum = physicsBodies[i].radius * physicsBodies[i].parentEntity.size.X + physicsBodies[j].radius * physicsBodies[j].parentEntity.size.X;
                        float radiusRatioA = physicsBodies[j].radius * physicsBodies[j].parentEntity.size.X / radiusSum;
                        float radiusRatioB = physicsBodies[i].radius * physicsBodies[i].parentEntity.size.X / radiusSum;

                        physicsBodies[i].position += correction * radiusRatioA;
                        physicsBodies[j].position -= correction * radiusRatioB;
                    }
                }
            }
        }
        void ApplyConstraint()
        {
            float maxX = 40;
            float maxY = 40;
            foreach (PhysicsBody physicsBody in physicsBodies)
            {
                if (physicsBody.position.X + physicsBody.radius * physicsBody.parentEntity.size.X > maxX || physicsBody.position.X - physicsBody.radius * physicsBody.parentEntity.size.X < -maxX)
                {
                    physicsBody.velocity.X *= -1;
                }
                if (physicsBody.position.Y + physicsBody.radius * physicsBody.parentEntity.size.X > maxY)
                {
                    physicsBody.velocity.Y *= -1;
                    physicsBody.position.Y = maxY - physicsBody.radius * physicsBody.parentEntity.size.X;
                }
            }
        }

    }
}
