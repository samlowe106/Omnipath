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
    /// <summary>
    /// Class from which all enemies
    /// </summary>
    abstract class NPC : GameObject
    {
        public NPC(Rectangle rectangle, Texture2D texture) 
            : base(rectangle, texture)
        {

        }


    }
}
