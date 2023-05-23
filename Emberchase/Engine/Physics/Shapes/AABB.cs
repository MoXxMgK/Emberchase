using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emberchase.Engine.Physics
{
    public class AABB : Shape
    {
        public float Width;
        public float Height;

        public float HalfWidth => Width / 2;
        public float HalfHeight => Height / 2;

        public Vector2 Size
        {
            get => new Vector2(Width, Height);
            set
            {
                Width = value.X;
                Height = value.Y;
            }
        }

        public override Rectangle Bounds => new Rectangle(TopLeft.ToPoint(), new Point((int)Width, (int)Height));


        #region PositionProps

        /// <summary>
        /// Alias for the position
        /// </summary>
        public Vector2 Center
        {
            get => Position;
            set => Position = value;
        }

        public Vector2 Left
        {
            get => new Vector2(Position.X - HalfWidth, Position.Y);
            set => Position = new Vector2(value.X + HalfWidth, value.Y);
        }

        public Vector2 Right
        {
            get => new Vector2(Position.X + HalfWidth, Position.Y);
            set => Position = new Vector2(value.X - HalfWidth, value.Y);
        }

        public Vector2 Top
        {
            get => new Vector2(Position.X, Position.Y - HalfHeight);
            set => Position = new Vector2(value.X, value.Y + HalfHeight);
        }

        public Vector2 Bottom
        {
            get => new Vector2(Position.X, Position.Y + HalfHeight);
            set => Position = new Vector2(value.X, value.Y - HalfHeight);
        }

        public Vector2 TopLeft
        {
            get => new Vector2(Position.X - HalfWidth, Position.Y - HalfHeight);
            set => Position = new Vector2(value.X + HalfWidth, value.Y + HalfHeight);
        }

        public Vector2 TopRight
        {
            get => new Vector2(Position.X + HalfWidth, Position.Y - HalfHeight);
            set => Position = new Vector2(value.X - HalfWidth, value.Y + HalfHeight);
        }

        public Vector2 BottomLeft
        {
            get => new Vector2(Position.X - HalfWidth, Position.Y + HalfHeight);
            set => Position = new Vector2(value.X + HalfWidth, value.Y - HalfHeight);
        }

        public Vector2 BottomRight
        {
            get => new Vector2(Position.X + HalfWidth, Position.Y + HalfHeight);
            set => Position = new Vector2(value.X - HalfWidth, value.Y - HalfHeight);
        }

        /// <summary>
        /// Alias to <see cref="TopLeft"/>
        /// </summary>
        public Vector2 Min
        {
            get => TopLeft;
            set => TopLeft = value;
        }

        /// <summary>
        /// Alias to <see cref="BottomRight"/>
        /// </summary>
        public Vector2 Max
        {
            get => BottomRight;
            set => BottomRight = value;
        }
        #endregion


        #region Ctors
        public AABB(Vector2 position, Vector2 size)
        {
            Position = position;
            Width = size.X;
            Height = size.Y;
        }

        public AABB(Vector2 size) : this(Vector2.Zero, size) { }

        public AABB(float width, float height) : this (new Vector2(width, height)) { }

        public AABB(Vector2 position, float width, float height) : this(position, new Vector2(width, height)) { }

        public AABB(Vector2 position, int width, int height) : this(position, (float)width, (float)height) { }

        public AABB(Rectangle rect) : this(rect.Center.ToVector2(), rect.Size.ToVector2()) { }
        #endregion

        public AABB GetSweptBroadphase(Vector2 movement)
        {
            AABB broad = new AABB(Position, Size);
            broad.Position += movement;

            return broad;
        }

        public override bool Collide(Shape other)
        {
            if (other is AABB box)
            {
                return Collision.ABBBvsAABB(this, box);
            }

            return false;
        }

        public override bool ContainsPoint(Vector2 point)
        {
            return Bounds.Contains(point);
        }
    }
}
