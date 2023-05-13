using Emberchase.ECS.Base;
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
                OnDrawLayerChanged?.Invoke(this, oldLayer);
            }
        }

        public event DrawLayerChangedDelegate OnDrawLayerChanged;

        public virtual void Draw(SpriteBatch batch) { }
    }
}
