using Emberchase.ECS.Components;
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
        public Vector2 LocalOffset = Vector2.Zero;

        public Vector2 AbsolutePosition => Transform.Position + LocalOffset;

        public bool IsTrigger;

        public abstract Rectangle Bounds { get; }

        protected bool _autoSize = false;

        public abstract bool Collide(Collider other);

        public abstract bool ContainsPoint(float x, float y);
        public virtual bool ContainsPoint(int x, int y) => ContainsPoint((float)x, (float)y);
        public virtual bool ContainsPoint(Vector2 point) => ContainsPoint(point.X, point.Y);
        public virtual bool ContainsPoint(Point point) => ContainsPoint(point.X, point.Y);
    }
}
