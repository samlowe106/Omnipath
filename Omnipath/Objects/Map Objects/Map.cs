using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Omnipath
{
    /// <summary>
    /// A playable area loaded from a file
    /// Centers on the camera and dynamically loads/unloads terrain based on the screen width/height
    /// </summary>
    class Map
    {
        #region Constants
        const string TEXTURE_FILE = "";
        #endregion

        #region Fields
        private Texture2D[] textures;

        private int activeWidth;
        private int activeHeight;

        private int loadedWidth;
        private int loadedHeight;

        private int mapWidth;
        private int mapHeight;

        private int centerX;
        private int centerY;

        private Graph localMap;
        #endregion

        #region Constructor
        public Map(string fileName, int centerX, int centerY, int activeWidth, int activeHeight, int loadedWidth, int loadedHeight, Texture2D[] textures)
        {
            this.textures = textures;
            localMap = new Graph();
            Load(fileName);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Loads map from a file
        /// </summary>
        public void Load(string fileName)
        {
            // Establish the stream and reader as null
            FileStream inStream = null;
            BinaryReader reader = null;

            // Try reading from the file
            try
            {
                inStream = File.OpenRead(fileName);
                reader = new BinaryReader(inStream);

                // Begin reading data from the file

                mapWidth = reader.ReadInt32();
                mapHeight = reader.ReadInt32();

                // Read map data to create a rectangle (with dimensions based on the screenWidth and screenHeight)
                //  centered on (centerX, centerY)

                // Skip over unnecessary lines
                for(; ; )
                {
                    // Skip over unnecessary X-coordinates

                    // Read in animation data for the tile
                    Texture2D[] animationFrames = new Texture2D[5];
                    for (int i = 0; i < 5; ++i)
                    {
                        animationFrames[i] = textures[reader.ReadInt32()];
                    }

                    /*new Terrain(
                        animationFrames,
                        reader.ReadBoolean(),
                        reader.ReadInt32(),
                        reader.ReadInt32(),
                        reader.ReadInt32());
                    */    
                    //        
                    //      Move to next line
                }

            }
            // Catch any exceptions
            catch (Exception e)
            {
                // TODO: HANDLE OR LOG ERROR MESSAGES
            }
            // Finally, close the file (if it was successfully opened)
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// The area on screen, and the zone in which GameObjects are active
        /// </summary>
        public Rectangle ActiveZone
        {
            get
            {
                return new Rectangle(centerX - (activeWidth / 2), centerY - (activeHeight / 2), activeWidth, activeHeight);
            }
        }
        
        /// <summary>
        /// The area including the screen and the area just outside the screen, in which GameObjects are loaded in
        /// (but not necessarily active)
        /// Contains the ActiveZone in the center
        /// </summary>
        public Rectangle LoadedZone
        {
            get
            {
                return new Rectangle(centerX - (loadedWidth / 2), centerY - (loadedHeight / 2), loadedWidth, loadedHeight);
            }
        }


        /// <summary>
        /// The maximum width of this map
        /// </summary>
        public int MapWidth
        {
            get
            {
                return mapWidth;
            }
        }

        /// <summary>
        /// The maximum height of this map
        /// </summary>
        public int MapHeight
        {
            get
            {
                return mapHeight;
            }
        }
        #endregion
    }
}
