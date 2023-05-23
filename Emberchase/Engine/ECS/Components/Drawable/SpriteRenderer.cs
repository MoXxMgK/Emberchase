using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Emberchase.Graphics;

namespace Emberchase.ECS.Components
{
    public class SpriteRenderer : DrawableComponent
    {
        #region Props
        protected Vector2 _origin;
        public Vector2 Origin
        {
            get => _origin;
            set => SetOrigin(value);
        }

        public SpriteRenderer SetOrigin(Vector2 origin)
        {
            if (origin != _origin)
                _origin = origin;

            return this;
        }

        protected Sprite _sprite;
        public Sprite Sprite
        {
            get => _sprite;
            set => SetSprite(value);
        }

        public SpriteRenderer SetSprite(Sprite sprite)
        {
            _sprite = sprite;

            if (sprite != null)
                SetOrigin(sprite.Origin);

            return this;
        }

        public SpriteRenderer SetTexture(Texture2D texture) => SetSprite(new Sprite(texture));

        public SpriteEffects Effects = SpriteEffects.None;

        public bool FlipX
        {
            get => (Effects & SpriteEffects.FlipHorizontally) == SpriteEffects.FlipHorizontally;
            set => Effects = value ? (Effects | SpriteEffects.FlipHorizontally) : (Effects & ~SpriteEffects.FlipHorizontally);
        }

        public bool FlipY
        {
            get => (Effects & SpriteEffects.FlipVertically) == SpriteEffects.FlipVertically;
            set => Effects = value ? (Effects | SpriteEffects.FlipVertically) : (Effects & ~SpriteEffects.FlipVertically);
        }

        public override float Width => Sprite.SourceRect.Width;
        public override float Height => Sprite.SourceRect.Height;

        #endregion

        #region Ctors
        public SpriteRenderer(Texture2D texture) : this(new Sprite(texture)) { }

        public SpriteRenderer(Sprite sprite)
        {
            SetSprite(sprite);
        }

        #endregion

        public override void Draw(SpriteBatch batch)
        {
            batch.Draw(Sprite, Owner.Position + LocalOffset, Sprite.SourceRect, Color, Transform.Rotation, Origin, Transform.Scale, Effects, 1f);
        }
    }
}
