﻿using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emberchase.ECS.Base
{

    public interface IDrawComponent : IComparable<IDrawComponent>
    {
        bool IsVisible { get; set; }

        int DrawLayer { get; set; }

        void Draw(SpriteBatch batch);
    }
}
