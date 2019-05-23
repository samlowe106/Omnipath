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
    class Terrain : IDisplayable
    {
        #region Constructor
        public Terrain(Texture2D texture, bool passable)
        {
            this.Animated = false;
            this.Texture = texture;
            this.Passable = passable;
        }

        public Terrain(Texture2D[] textures, bool passable)
        {
            this.Animated = true;
            this.Textures = textures;
            this.Passable = passable;
        }
        #endregion

        #region Methods
        public void Draw()
        {

        }
        #endregion

        #region Properties
        public Texture2D Texture { get; }

        public bool Passable { get; }

        public bool Animated { get; }

        public int FrameNumber { get; }

        /// <summary>
        /// Maximum number of frames
        /// </summary>
        public int FrameCount
        {
            get
            {
                return Textures.Length;
            }
        }
        #endregion
    }
}
