using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace Omnipath
{
    class Terrain : IDisplayable
    {
        #region Fields
        private int frameNumber;
        #endregion

        #region Constructor
        /// <param name="reader">The binaryreader from which to read the data for this Terrain</param>
        /// <param name="dimensions">Width and height, in pixels, of this Terrain</param>
        public Terrain(BinaryReader reader, int dimensions, int x, int y)
        {
            // Read this Terrain's ID from the file
            int terrainID = reader.ReadInt32();
            Textures = Game1.terrainTextures[terrainID];
            this.EnterableFrom = Game1.terrainEnterableFrom[terrainID];
            this.Active = true;
            this.frameNumber = 0;
            this.Rectangle = new Rectangle(x * dimensions, y * dimensions, dimensions, dimensions);
            this.Visited = false;
        }

        public Terrain(BinaryReader reader) : this(reader, 0, 0, 0) { }
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
        public NPCType OccupantID { get; set; }

        /// <summary>
        /// Describes if this Terrain can be entered from the North (0), the East (1), the South (2), and the West (3);
        /// true if this Terrain can be entered from that direction, false if it cannot
        /// (Index this array with the Direction enum for clarity)
        /// </summary>
        public bool[] EnterableFrom { get; }

        /// <summary>
        /// If the terrain can be entered from the North
        /// </summary>
        public bool PassableNorth { get; set; }

        /// <summary>
        /// If the terrain can be entered from the East
        /// </summary>
        public bool PassableEast { get; set; }

        /// <summary>
        /// If the terrain can be entered from the South
        /// </summary>
        public bool PassableSouth { get; set; }

        /// <summary>
        /// If the terrain can be entered from the West
        /// </summary>
        public bool PassableWest { get; set; }

        /// <summary>
        /// How many frames are in this terrain's animation cycle
        /// </summary>
        public int FrameCount
        {
            get
            {
                return this.Textures.Length;
            }
        }

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
        /// If this Terrain has been visited by a depth-first search
        /// </summary>
        public bool Visited { get; set; }
        #endregion
    }
}
