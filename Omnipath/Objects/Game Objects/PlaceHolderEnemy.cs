using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Omnipath
{
    class PlaceHolderEnemy : GameObject
    {
        public PlaceHolderEnemy(Point spawn, Texture2D texture)
            : base(new Rectangle(spawn, new Point(10, 10)), texture)
        {

        }
    }
}
