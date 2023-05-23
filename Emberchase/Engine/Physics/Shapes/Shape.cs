using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emberchase.Engine.Physics
{
    public abstract class Shape
    {
        public Vector2 Position;

        public abstract Rectangle Bounds { get; }

        public abstract bool Collide(Shape other);

        public abstract bool ContainsPoint(Vector2 point);
    }
}
