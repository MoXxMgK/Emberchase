using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emberchase.Engine.Extentions
{
    public static class VectorExtentions
    {
        public static float AngleTo(this Vector2 a, Vector2 b) => MathF.Atan2(b.Y - a.Y, b.X - a.X);

        public static float AngleFrom(this Vector2 a, Vector2 b) => MathF.Atan2(a.Y - b.Y, a.X - b.X);

        public static float Distance(this Vector2 a, Vector2 b) => Vector2.Distance(a, b);

        public static float DistanceSquared(this Vector2 a, Vector2 b) => Vector2.DistanceSquared(a, b);

        public static Vector2 DirectionTo(this Vector2 a, Vector2 b) => Vector2.Normalize(b - a);

        public static Vector2 DirectionFrom(this Vector2 a, Vector2 b) => Vector2.Normalize(a - b);

        public static bool WithinRange(this Vector2 a, Vector2 b, float range) => DistanceSquared(a, b) <= range * range;
    }
}
