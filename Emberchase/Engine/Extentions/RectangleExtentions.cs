using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emberchase.Extentions
{
    public static class RectangleExtentions
    {
        public static Vector2 GetHalfSize(this Rectangle rectangle)
        {
            return new Vector2(rectangle.Width * 0.5f, rectangle.Height * 0.5f);
        }

        public static Rectangle GetCopy(this Rectangle rect)
        {
            return new Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
        }
    }
}
