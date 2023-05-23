using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emberchase.ECS.Components;
using Microsoft.Xna.Framework;

namespace Emberchase.ECS
{
    public class Entity
    {
        private static int NextEntityId = 0;

        public readonly int Id;
        public string Name;

        public World World { get; set; }

        private bool _isActive = true;
        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
                IsActiveChanged?.Invoke(value);
            }
        }
        public event Action<bool> IsActiveChanged;

        // Dimentions

        public readonly Transform Transform;
        public Vector2 Position
        {
            get => Transform.Position;
            set => Transform.Position = value;
        }

        public readonly ComponentsList Components;

        #region Ctor
        public Entity(string name)
        {
            Id = NextEntityId;
            NextEntityId++;
            Name = name;

            Transform = new Transform();
            Components = new ComponentsList(this);
        }

        public Entity() : this($"Entity_{NextEntityId}") { }
        #endregion

        public virtual void Initialize()
        {

        }

        public virtual void OnAddToWorld()
        {

        }

        public virtual void OnRemoveFromWorld()
        {

        }

        public Entity AddComponent(Component component)
        {
            Components.Add(component);

            return this;
        }

        public Entity RemoveComponent(Component component)
        {
            Components.Remove(component);

            return this;
        }

        public T GetComponent<T>() where T : Component
        {
            return Components.GetComponent<T>();
        }

        public List<T> GetComponents<T>() where T : Component
        {
            return Components.GetComponents<T>();
        }

        public virtual void Update()
        {
            Components.Update();
        }
    }
}
