using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emberchase
{
    public static class Time
    {
        public static float DeltaTime { get; private set; } = 0f;
        public static float OldDeltaTime { get; private set; } = 0f;
        public static ulong Ticks { get; private set; } = 0;

        public static void Update(GameTime gameTime)
        {
            Ticks += 1;

            OldDeltaTime = DeltaTime;
            DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
