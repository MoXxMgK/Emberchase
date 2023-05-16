using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Emberchase.ECS;
using Emberchase.ECS.Components;
using Microsoft.Xna.Framework;

namespace Emberchase.Extentions
{
    public static class EntityExtentions
    {

        /// <summary>
        /// Calculates entity bounds using all of ther DrawableComponents
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns></returns>
        public static Rectangle CalculateBounds(this Entity entity)
        {

            // TODO: Can make it better?
            var drawables = entity.GetComponents<DrawableComponent>();

            drawables.Sort((l, r) =>
            {
                if (l.Bounds.Location.X >= r.Bounds.Location.X &&
                l.Bounds.Location.Y >= r.Bounds.Location.Y)
                {
                    return 1;
                }

                return 0;
            });

            if (drawables.Count == 0)
                return new Rectangle(entity.Position.ToPoint(), new Point(1, 1));
            if (drawables.Count == 1)
                return drawables[0].Bounds;

            // Else
            Point topleft = drawables[0].Bounds.Location;
            var lastBound = drawables[^1].Bounds;
            Point bottomRight = lastBound.Location + lastBound.Size;

            return new Rectangle(topleft, bottomRight - topleft);
        }
    }
}
