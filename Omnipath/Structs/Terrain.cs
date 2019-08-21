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
        #region Constants
        /// <summary>
        /// the dimensions of the square of the terrain
        /// </summary>
        private const int DIMENSIONS = 64;
        #endregion

        #region Fields
        private int frameNumber;
        #endregion

        #region Constructor
        /// <summary>
        /// A terrain struct; it's faster to do the assignments manually
        /// </summary>
        /// <param name="textures">the textures for the terrain</param>
        /// <param name="passable"> if the object is passable</param>
        /// <param name="x"> the x location of the terrain</param>
        /// <param name="y"> the y location of the terrain </param>
        /// <param name="occupant"> the gameobject that spawns when this terrain is loaded in </param>
        public Terrain(Texture2D[] textures, bool passable, int x, int y, GameObject occupant)
        {
            this.Textures = textures;
            this.Passable = passable;
            this.Active = true;
            this.frameNumber = 0;
            this.FrameCount = this.Textures.Length;
            this.Rectangle = new Rectangle(x, y, DIMENSIONS, DIMENSIONS);
        }
        #endregion

        #region Methods
        public void Draw(SpriteBatch sp)
        {
            if (this.Active)
            {
                sp.Draw(this.Textures[FrameNumber], this.Rectangle, Color.White);
                frameNumber = (frameNumber + 1) % FrameCount;
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// The textures that make up the terrain
        /// </summary>
        public Texture2D[] Textures { get; set; }

        /// <summary>
        /// The NPC or enemy that spawns when this tile is loaded in
        /// </summary>
        public GameObject Occupant { get; set; }

        /// <summary>
        /// If the terrain is passable by a player
        /// </summary>
        public bool Passable { get; set; }

        /// <summary>
        /// the current frame number (always 0 for non-animated terrains)
        /// </summary>
        public int FrameNumber
        {
            get
            {
                return frameNumber;
            }
        }

        /// <summary>
        /// If this terrain is on-screen and drawable
        /// </summary>
        public bool Active
        {
            get;
        }

        public Texture2D Texture
        {
            get
            {
                return Textures[FrameNumber];
            }
        }

        /// <summary>
        /// a rectangle representing the terrain
        /// </summary>
        public Rectangle Rectangle { get; set; }

        /// <summary>
        /// Maximum number of frames in this terrain's animation cycle
        /// </summary>
        public int FrameCount { get; set; }
        #endregion
    }
}
