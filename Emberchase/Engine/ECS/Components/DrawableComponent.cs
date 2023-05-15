using Emberchase.ECS.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emberchase.ECS.Components
{
    public abstract class DrawableComponent : Component, IDrawComponent
    {
        private bool _isVisible = true;
        public bool IsVisible
        {
            get => Owner != null ? Owner.IsActive && _isVisible : _isVisible;
            set
            {
                _isVisible = value;
            }
        }

        private int _drawLayer = World.Renderlayer.Main;
        public int DrawLayer
        {
            get => _drawLayer;
            set
            {
                int oldLayer = _drawLayer;
                _drawLayer = value;
            }
        }

        public Color Color = Color.White;

        public Vector2 LocalOffset = Vector2.Zero;

        public abstract float Width { get; }
        public abstract float Height { get; }

        public virtual Rectangle Bounds => new Rectangle(
            (int)Owner.Position.X + (int)LocalOffset.X,
            (int)Owner.Position.Y + (int)LocalOffset.Y,
            (int)Width, 
            (int)Height);

        // TODO Add fluent setters

        public abstract void Draw(SpriteBatch batch);
    }
}
