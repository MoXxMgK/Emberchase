using Emberchase.ECS.Components;
using Emberchase.Engine.Physics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emberchase.ECS.Components
{
    public abstract class Collider : Component
    {
        private Shape _shape;
        public Shape Shape
        {
            get
            {
                _shape.Position = AbsolutePosition;
                return _shape;
            }
            set => _shape = value;
        }

        public Vector2 LocalOffset = Vector2.Zero;

        public Vector2 AbsolutePosition => Transform.Position + LocalOffset;

        public bool IsTrigger;

        public virtual Rectangle Bounds
        {
            get
            {
                return Shape.Bounds;
            }
        }

        protected bool _autoSize = false;

        public override void OnAddToEntity()
        {
            if (_autoSize)
            {
                var drawable = Owner.GetComponent<DrawableComponent>();

                if (drawable != null)
                {
                    var drawableBounds = drawable.Bounds;

                    // TODO: Deal with scaling

                    if (this is BoxCollider box)
                    {
                        box.Width = Bounds.Width;
                        box.Height = Bounds.Height;

                        LocalOffset = drawableBounds.Center.ToVector2() - Owner.Transform.Position;
                    }
                }
            }
        }

        public bool Collide(Collider other)
        {
            return Shape.Collide(other.Shape);
        }

        public bool ContainsPoint(Vector2 point)
        {
            return Shape.ContainsPoint(point);
        }
        public virtual bool ContainsPoint(int x, int y) => ContainsPoint((float)x, (float)y);
        public virtual bool ContainsPoint(float x, float y) => ContainsPoint(new Vector2(x, y));
        public virtual bool ContainsPoint(Point point) => ContainsPoint(point.X, point.Y);
    }
}
