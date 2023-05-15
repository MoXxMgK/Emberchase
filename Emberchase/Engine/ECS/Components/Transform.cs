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

        public float Rotation = 0f;

        public float ScaleX = 1f;
        public float ScaleY = 1f;

        public Vector2 Scale
        {
            get => new Vector2(ScaleX, ScaleY);
            set
            {
                ScaleX = value.X;
                ScaleY = value.Y;
            }
        }
    }
}
