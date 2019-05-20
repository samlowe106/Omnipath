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
    interface IDisplayable
    {
        /// <summary>
        /// Draws this object to the screen
        /// </summary>
        /// <param name="sp">SpriteBatch which will draw this object</param>
        void Draw(SpriteBatch sp);

        Texture2D Texture { get; }

        Rectangle Rectangle { get; }

        /// <summary>
        /// Whether this object is drawable or not
        /// </summary>
        bool Active { get; }
    }
}
