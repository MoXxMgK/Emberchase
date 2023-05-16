using Emberchase.ECS.Base;
using Emberchase.Extentions;
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

        protected int _drawLayer = 0;
        public virtual int DrawLayer
        {
            get => _drawLayer;
            set => _drawLayer = value;
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

        protected bool _useAsSize = false;

        public DrawableComponent AsSize()
        {
            _useAsSize = true;

            return this;
        }

        public override void OnAddToEntity()
        {
            if (_useAsSize)
            {
                Owner.Width = Width; 
                Owner.Height = Height;
            }
        }

        public abstract void Draw(SpriteBatch batch);

        #region IComparable
        public int CompareTo(IDrawComponent other)
        {
            return this.DrawLayer.CompareTo(other.DrawLayer); ;
        }
        #endregion
    }
}
