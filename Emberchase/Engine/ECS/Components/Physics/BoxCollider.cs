using Emberchase.Engine.Physics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emberchase.ECS.Components
{
    public class BoxCollider : Collider
    {
        public float Width
        {
            get => (Shape as AABB).Width;
            set => (Shape as AABB).Width = value;
        }
        public float Height
        {
            get => (Shape as AABB).Height;
            set => (Shape as AABB).Height = value;
        }

        // This wil also recalculate shape position
        public override Rectangle Bounds => _box.Bounds;

        private AABB _box => Shape as AABB;

        // TODO: Add sides with position changing

        #region Ctors
        public BoxCollider()
        {
            Shape = new AABB(1f, 1f);
            _autoSize = true;
        }

        public BoxCollider(float x, float y, float width, float height)
        {
            LocalOffset = new Vector2(x, y);
            Shape = new AABB(width, height);
        }

        public BoxCollider(float width, float height) : this (0f, 0f, width, height) { }

        public BoxCollider(Rectangle rect) : this(rect.X, rect.Y, rect.Width, rect.Height) { }
        #endregion
    }
}
