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
    /// Represents a status modifer
    /// </summary>
    class Modifier
    {
        private GameObject source;

        /// <summary>
        /// Number of ticks until this modifier expires
        /// </summary>
        private int ticksRemaining;
        
        // time applied
        // time it will expire
        // time until it expires
        // effect

        public void Update()
        {
            if (!Finished)
            {
                --ticksRemaining;
            }
        }

        /// <summary>
        /// Number of ticks until this modifier expires
        /// </summary>
        public int TicksRemaining
        {
            get
            {
                return ticksRemaining;
            }
        }

        /// <summary>
        /// True if this modifier has expired 
        /// False if it has not
        /// </summary>
        public bool Finished
        {
            get
            {
                return TicksRemaining <= 0;
            }
        }

        public Texture2D Texture { get; }
    }
}
