using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emberchase.Engine.Physics
{
    public static partial class Collision
    {
        public static bool ABBBvsAABB(AABB a, AABB b)
        {
            AABB minkowskiDiff = MinkowskiDifference(a, b);

            if (minkowskiDiff.ContainsPoint(new Vector2(0, 0)))
            {
                return true;
            }

            return false;
        }

        public static bool AABBvsAABB(AABB a, AABB b, out CollisionResult result)
        {
            result = new CollisionResult();

            AABB minkowskiDiff = MinkowskiDifference(a, b);

            if (minkowskiDiff.ContainsPoint(new Vector2 (0, 0)))
            {
                result.TranslationVector = minkowskiDiff.GetClosestPointOnBoundsToTopLeft();
                if (result.TranslationVector == Vector2.Zero)
                    return false;

                result.Normal = -result.TranslationVector;
                result.Normal.Normalize();

                return true;
            }

            return false;
        }

        public static bool AABBvsAABB(AABB a, AABB b, Vector2 movement, out CollisionResult result)
        {
            result = new CollisionResult();

            AABB broad = a.GetSweptBroadphase(movement);
            AABB minkowskiDiff = MinkowskiDifference(broad, b);

            if (minkowskiDiff.ContainsPoint(new Vector2(0, 0)))
            {
                result.TranslationVector = minkowskiDiff.GetClosestPointOnBoundsToTopLeft();
                if (result.TranslationVector == Vector2.Zero)
                    return false;

                result.Normal = -result.TranslationVector;
                result.Normal.Normalize();

                return true;
            }

            return false;
        }

        public static AABB MinkowskiDifference(AABB a, AABB b)
        {
            Vector2 topleft = a.Min - b.Max;
            Vector2 size = a.Size + b.Size;
            Vector2 center = topleft + size / 2;

            return new AABB(center, size);
        }

        #region AABB Extentions for physics calculations
        public static Vector2 GetClosestPointOnBounds(this AABB box, Vector2 point)
        {
            Vector2 min = box.Min;
            Vector2 max = box.Max;
            float minDist = MathF.Abs(point.X - min.X);
            Vector2 boundsPoint = new Vector2(max.X, point.Y);

            if (MathF.Abs(max.X - point.X) < minDist)
            {
                minDist = MathF.Abs(max.X - point.X);
                boundsPoint.X = max.X;
                boundsPoint.Y = point.Y;
            }

            if (MathF.Abs(max.Y - point.Y) < minDist)
            {
                minDist = MathF.Abs(max.Y - point.Y);
                boundsPoint.X = point.X;
                boundsPoint.Y = max.Y;
            }

            if (MathF.Abs(min.Y - point.Y) < minDist)
            {
                minDist = MathF.Abs(min.Y - point.Y);
                boundsPoint.X = point.X;
                boundsPoint.Y = min.Y;
            }

            return boundsPoint;
        }

        public static Vector2 GetClosestPointOnBoundsToTopLeft(this AABB box)
        {
            return GetClosestPointOnBounds(box, new Vector2());
        }
        #endregion
    }
}
