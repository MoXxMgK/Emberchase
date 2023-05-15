using Emberchase.Extentions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emberchase.Graphics
{
    public class Sprite
    {
        public Texture2D Texture;

        public readonly Rectangle SourceRect;

        public readonly Vector2 Center;

        public Vector2 Origin;

        public Sprite(Texture2D texture, Rectangle sourceRect, Vector2 origin)
        {
            Texture = texture;
            SourceRect = sourceRect;
            Origin = origin;

            Center = new Vector2(sourceRect.Width * 0.5f, sourceRect.Height * 0.5f);
        }

        public Sprite(Texture2D texture, Rectangle sourceRect) : this(texture, sourceRect, sourceRect.GetHalfSize()) { }

        public Sprite(Texture2D texture) : this(texture, new Rectangle(0, 0, texture.Width, texture.Height)) { }

        public Sprite Clone()
        {
            return new Sprite(Texture, SourceRect, new Vector2(Origin.X, Origin.Y));
        }

        public static implicit operator Texture2D(Sprite sprite)
        {
            return sprite.Texture;
        }
    }
}
