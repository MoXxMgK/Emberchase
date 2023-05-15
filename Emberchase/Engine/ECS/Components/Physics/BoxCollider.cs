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
        public float Width;
        public float Height;

        public override Rectangle Bounds => new Rectangle((int)AbsolutePosition.X, (int)AbsolutePosition.Y, (int)Width, (int)Height);

        #region Ctors
        public BoxCollider()
        {
            Width = 1f;
            Height = 1f;
            _autoSize = true;
        }

        public BoxCollider(float x, float y, float width, float height)
        {
            LocalOffset = new Vector2(x, y);
            Width = width;
            Height = height;
        }

        public BoxCollider(float width, float height) : this (0f, 0f, width, height) { }

        public BoxCollider(Rectangle rect) : this(rect.X, rect.Y, rect.Width, rect.Height) { }
        #endregion

        public override void OnAddToEntity()
        {
            if (_autoSize)
            {
                Width = Owner.Width;
                Height = Owner.Height;
            }
        }

        #region Collider
        public override bool Collide(Collider other)
        {
            if (other == null)
                return false;

            if (other is BoxCollider)
                return Bounds.Intersects(other.Bounds);

            return false;
        }

        public override bool ContainsPoint(float x, float y)
        {
            return Bounds.Contains(x, y);
        }
        #endregion
    }
}
