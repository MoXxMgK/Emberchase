using Emberchase.ECS.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emberchase.Engine.Physics
{
    public struct CollisionResult
    {
        public Collider Collider;

        public Vector2 Normal;

        public Vector2 TranslationVector;
    }
}
