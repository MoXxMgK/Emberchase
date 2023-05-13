using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emberchase.ECS.Base
{
    public interface IUpdateComponent
    {
        bool IsEnabled { get; set; }

        void Update();
    }
}
