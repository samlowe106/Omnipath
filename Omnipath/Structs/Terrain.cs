﻿using System;
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
        /// Constructs a terrain object that is not animated
        /// </summary>
        /// <param name="texture"> the texture for the terrain</param>
        /// <param name="passable"> if the terrain is passable</param>
        /// <param name="X"> the x location of the terrain </param>
        /// <param name="Y"> the y location of the terrain </param>
        public Terrain(Texture2D texture, bool passable, int x, int y)
        {
            this.Animated = false;
            this.Textures = new Texture2D[1];
            this.Textures[0] = texture;
            this.Passable = passable;
            this.Active = true;
            this.frameNumber = 0;
            this.FrameCount = this.Textures.Length;
            this.Rectangle = new Rectangle(x, y, DIMENSIONS, DIMENSIONS);
        }

        /// <summary>
        /// Constructs a terrain object that is animated
        /// </summary>
        /// <param name="textures">the textures for the terrain</param>
        /// <param name="passable"> if the object is passable</param>
        /// <param name="X"> the x location of the terrain</param>
        /// /// <param name="Y"> the y location of the terrain </param>
        public Terrain(Texture2D[] textures, bool passable, int x, int y)
        {
            this.Animated = true;
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
            }    
            if (this.Animated)
            {
                frameNumber = (frameNumber + 1) % FrameCount;
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// The textures that make up the terrain
        /// </summary>
        public Texture2D[] Textures { get; }

        /// <summary>
        /// If the terrain is passable by a player
        /// </summary>
        public bool Passable { get; }

        /// <summary>
        /// if the texture is animated
        /// </summary>
        public bool Animated { get; }

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

        public bool Active { get; }

        public Texture2D Texture
        {
            get
            {
                return Textures[FrameNumber];
            }
        }

        /// <summary>
        /// a rectangle representing the hitbox of the terrain
        /// </summary>
        public Rectangle Rectangle { get; }

        /// <summary>
        /// Maximum number of frames
        /// </summary>
        public int FrameCount { get; }
        #endregion
    }
}