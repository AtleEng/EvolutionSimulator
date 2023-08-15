using System.Numerics;
using System.Collections.Generic;
using Core;
using EvolutionSim;
namespace EvolutionSim
{
    public abstract class Component
    {
        public GameEntity parentEntity;

        public virtual void Start() { }
        public virtual void Update() { }
        public virtual void OnCollision(ColliderComponent other) { }
        public virtual void OnDestroy() { }
    }

    public class GameEntity
    {
        public string name = "GameEntity";
        public Dictionary<Type, Component> components = new();

        public Vector2 position;
        public Vector2 size;
        public float rotation;

        public GameEntity? parent;

        public bool HasComponent<T>()
        {
            return components.ContainsKey(typeof(T));
        }
        public T? GetComponent<T>() where T : Component
        {
            return (T)components[typeof(T)];
        }
        public void AddComponent<T>(Component component) where T : Component
        {
            component.parentEntity = this;
            components.Add(typeof(T), component);
        }
        public void RemoveComponent<T>()
        {
            components.Remove(typeof(T));
        }

        public virtual void Start()
        {
            foreach (Component component in components.Values)
            {
                component.Start();
            }
        }
        public virtual void OnCollision(ColliderComponent other)
        {
            foreach (Component component in components.Values)
            {
                component.OnCollision(other);
            }
        }
    }

    public abstract class GameSystem
    {
        public virtual void InnitSystem() { }
        public virtual void Update() { }
    }
}