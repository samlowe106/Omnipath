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
        /// <param name="textures">The textures for the terrain</param>
        /// <param name="x"> the x location of the terrain</param>
        /// <param name="y"> the y location of the terrain </param>
        /// <param name="occupant"> the gameobject that spawns when this terrain is loaded in </param>
        public Terrain(Texture2D[] animationFrames, bool passableNorth, bool passableEast, bool passableWest, bool passableSouth, int x, int y, int dimensions, NPCType occupantID)
        {
            this.Textures = animationFrames;
            this.PassableNorth = passableNorth;
            this.PassableEast = passableEast;
            this.PassableSouth = passableSouth;
            this.PassableWest = passableWest;
            this.Active = true;
            this.frameNumber = 0;
            this.FrameCount = this.Textures.Length;
            this.Rectangle = new Rectangle(x, y, dimensions, dimensions);
            this.OccupantID = occupantID;
            this.Visited = false;
        }

        /// <param name="reader">The binaryreader from which to read the data for this Terrain</param>
        /// <param name="dimensions">Width and height, in pixels, of this Terrain</param>
        /// <param name="textures">Texture array used to associate IDs with Texture2Ds</param>
        public Terrain(BinaryReader reader, int dimensions, Texture2D[] textures)
        {
            Textures = new Texture2D[5];
            for (int i = 0; i < 5; ++i)
            {
                Textures[i] = textures[reader.ReadInt32()];
            }
            this.PassableNorth = reader.ReadBoolean();
            this.PassableEast = reader.ReadBoolean();
            this.PassableSouth = reader.ReadBoolean();
            this.PassableWest = reader.ReadBoolean();
            this.Active = true;
            this.frameNumber = 0;
            this.FrameCount = this.Textures.Length;
            this.Rectangle = new Rectangle(reader.ReadInt32(), reader.ReadInt32(), dimensions, dimensions);
            this.OccupantID = (NPCType)reader.ReadInt32();
            this.Visited = false;
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
        public NPCType OccupantID { get; set; }

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

        /// <summary>
        /// If this Terrain has been visited by a depth-first search
        /// </summary>
        public bool Visited { get; set; }
        #endregion
    }
}
