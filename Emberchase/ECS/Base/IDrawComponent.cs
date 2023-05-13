using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emberchase.ECS.Base
{
    public delegate void DrawLayerChangedDelegate(IDrawComponent drawable, int oldLayer);

    public interface IDrawComponent
    {
        bool IsVisible { get; set; }

        int DrawLayer { get; set; }

        event DrawLayerChangedDelegate OnDrawLayerChanged;

        void Draw(SpriteBatch batch);
    }
}
