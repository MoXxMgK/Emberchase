using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using Emberchase.ECS.Base;
using Emberchase.ECS.Components;
using Microsoft.Xna.Framework.Input;

namespace Emberchase.Components
{
    public class PlayerMovementComponent : Component, IUpdateComponent
    {
        public float Speed = 150f;

        public Vector2 Velocity = Vector2.Zero;
        public Vector2 OldVelocity = Vector2.Zero;
        public Vector2 Direction = Vector2.Zero;
        public Vector2 OldDirection = Vector2.Zero;

        private SpriteRenderer _renderer;

        public override void OnAddToEntity()
        {
            _renderer = Owner.GetComponent<SpriteRenderer>();
        }

        public void Update()
        {
            var k = Keyboard.GetState();
            
            OldDirection = new Vector2(Direction.X, Direction.Y);

            Direction = Vector2.Zero;

            if (k.IsKeyDown(Keys.W))
            {
                Direction.Y -= 1;
            }
            if (k.IsKeyDown(Keys.S))
            {
                Direction.Y += 1;
            }
            if (k.IsKeyDown(Keys.A))
            {
                Direction.X -= 1;
                _renderer.FlipX = true;
            }
            if (k.IsKeyDown(Keys.D))
            {
                Direction.X += 1;
                _renderer.FlipX = false;
            }

            OldVelocity = new Vector2(Velocity.X, Velocity.Y);
            Velocity = Speed * Direction * Time.DeltaTime;

            Owner.Center += Velocity;
        }
    }
}
