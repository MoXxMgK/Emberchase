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
        public float Width;
        public float Height;

        public float HalfWidth => Width / 2;
        public float HalfHeight => Height / 2;

        public readonly Transform Transform;
        public Vector2 Position
        {
            get => Transform.Position;
            set => Transform.Position = value;
        }

        public readonly ComponentsList Components;

        #region PositionProps

        public Vector2 Center
        {
            get => new Vector2(Position.X + HalfWidth, Position.Y + HalfHeight);
            set => Position = new Vector2(value.X - HalfWidth, value.Y - HalfHeight);
        }

        public Vector2 Left
        {
            get => new Vector2(Position.X, Position.Y + HalfHeight);
            set => Position = new Vector2(value.X, value.Y - HalfHeight);
        }

        public Vector2 Right
        {
            get => new Vector2(Position.X + Width, Position.Y + HalfHeight);
            set => Position = new Vector2(value.X - Width, value.Y - HalfHeight);
        }

        public Vector2 Top
        {
            get => new Vector2(Position.X + HalfWidth, Position.Y);
            set => Position = new Vector2(value.X - HalfWidth, value.Y);
        }

        public Vector2 Bottom
        {
            get => new Vector2(Position.X + HalfWidth, Position.Y + Height);
            set => Position = new Vector2(value.X - HalfWidth, value.Y - Height);
        }

        public Vector2 TopLeft
        {
            get => Position;
            set => Position = value;
        }

        public Vector2 TopRight
        {
            get => new Vector2(Position.X + Width, Position.Y);
            set => Position = new Vector2(value.X - Width, value.Y);
        }

        public Vector2 BottomLeft
        {
            get => new Vector2(Position.X, Position.Y + Height);
            set => Position = new Vector2(value.X, value.Y - Height);
        }

        public Vector2 BottomRight
        {
            get => new Vector2(Position.X + Width, Position.Y + Height);
            set => Position = new Vector2(value.X - Width, value.Y - Height);
        }
        #endregion

        #region Math

        public float AngleTo(Vector2 dest)
        {
            return MathF.Atan2(dest.Y - Center.Y, dest.X - Center.X);
        }

        public float AngleTo(Entity other)
        {
            return AngleTo(other.Center);
        }

        public float AngleFrom(Vector2 source)
        {
            return MathF.Atan2(Center.Y - source.Y, Center.X - source.X);
        }

        public float AngleFrom(Entity other)
        {
            return AngleFrom(other.Center);
        }

        public float Distance(Vector2 dest)
        {
            return Vector2.Distance(Center, dest);
        }

        public float Distance(Entity other)
        {
            return Distance(other.Center);
        }

        public float DistanceSquared(Vector2 dest)
        {
            return Vector2.DistanceSquared(Center, dest);
        }

        public float DistanceSquared(Entity other)
        {
            return DistanceSquared(other.Center);
        }

        public Vector2 DirectionTo(Vector2 dest)
        {
            return Vector2.Normalize(dest - Center);
        }

        public Vector2 DirectionTo(Entity other)
        {
            return DirectionTo(other.Center);
        }

        public Vector2 DirectionFrom(Vector2 dest)
        {
            return Vector2.Normalize(Center - dest);
        }

        public Vector2 DirectionFrom(Entity other)
        {
            return DirectionFrom(other.Center);
        }

        public bool WithinRange(Vector2 target, float range)
        {
            return DistanceSquared(target) <= range * range;
        }

        public bool WithinRange(Entity target, float range)
        {
            return WithinRange(target.Center, range);
        }

        #endregion

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
