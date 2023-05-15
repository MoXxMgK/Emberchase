using Emberchase.ECS.Base;
using Emberchase.ECS.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emberchase.Components
{
    public class KeepInBounds : Component, IUpdateComponent
    {
        public readonly Rectangle Bounds;

        public KeepInBounds(Rectangle bounds)
        {
            Bounds = bounds;
        }

        public void Update()
        {
            Transform.Position.X = Math.Clamp(Transform.Position.X, (float)Bounds.Left, (float)Bounds.Right - Owner.Width);
            Transform.Position.Y = Math.Clamp(Transform.Position.Y, (float)Bounds.Top, (float)Bounds.Bottom - Owner.Height);
        }
    }
}
