using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Emberchase.ECS.Components
{
    public class Transform : Component
    {
        public Vector2 Position = Vector2.Zero;
        public Vector2 OldPositions = Vector2.Zero;
        public Vector2 Velocity = Vector2.Zero;
        public Vector2 OldVelocity = Vector2.Zero;
        public Vector2 Direction = Vector2.Zero;
        public Vector2 OldDirection = Vector2.Zero;

        public float Rotation = 0f;

        public float ScaleX = 1f;
        public float ScaleY = 1f;

        public Vector2 Scale => new Vector2(ScaleX, ScaleY);
    }
}
