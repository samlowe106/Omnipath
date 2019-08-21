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
        /* Note - centerY INCREASES as it moves farther South, and DECREASES as it moves North */

        #region Constants
        const string TEXTURE_FILE = "";
        /// <summary>
        /// the dimensions of the square of the terrain
        /// </summary>
        const int TERRAIN_DIMENSIONS = 64;
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

                Terrain[,] terrain = new Terrain[mapWidth, mapHeight];

                // Read map data to create a rectangle (with dimensions based on the screenWidth and screenHeight)
                //  centered on (centerX, centerY)

                // Skip over unnecessary lines
                for (int i = 0; i < centerY - (loadedHeight / 2); ++i)
                {
                    for (int j = 0; j < mapWidth; ++j)
                    {
                        SkipTerrain(reader);
                    }
                }

                // Read data from the current line
                for (int i = 0; i < mapWidth; ++i)
                {
                    // Skip over unnecessary X-coordinates at the start of the current line
                    for (int j = 0; j < centerX - (loadedWidth / 2); ++j)
                    {
                        SkipTerrain(reader);
                    }

                    // Load in the tiles that will be in the LoadedZone
                    for (int j = 0; j < loadedWidth; ++j)
                    {
                        // Reading data for the Terrain object

                        // Read in animation data for the tile
                        Texture2D[] animationFrames = new Texture2D[5];
                        for (int k = 0; k < 5; ++i)
                        {
                            animationFrames[k] = textures[reader.ReadInt32()];
                        }
                        
                        terrain[i,j].Textures = animationFrames;
                        terrain[i,j].Passable = reader.ReadBoolean();
                        terrain[i,j].Rectangle = new Rectangle(reader.ReadInt32(), reader.ReadInt32(), TERRAIN_DIMENSIONS, TERRAIN_DIMENSIONS);
                        terrain[i,j].OccupantID = (NPCType)reader.ReadInt32();
                    }

                    // Skip over unnecessary x coordinates at the end of the current line
                    for (int j = 0; j < mapWidth - (centerX + (loadedWidth / 2)); ++j)
                    {
                        SkipTerrain(reader);
                    }
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

        /// <summary>
        /// Skips over a Terrain object; 8 32-bit Integers and a Boolean
        /// </summary>
        /// <param name="reader"></param>
        public void SkipTerrain(BinaryReader reader)
        {
            for (int k = 0; k < 8; ++k)
            {
                reader.ReadInt32();
            }
            reader.ReadBoolean();
        }

        /// <param name="identifier">An ID for a corresponding GameObject</param>
        /// <param name="coords">Where that GameObject will be placed</param>
        /// <returns></returns>
        public GameObject GetGameObject(int identifier, Point coords)
        {
            switch ((NPCType)identifier)
            {
                case NPCType.PlaceHolderEnemy:
                    return new PlaceHolderEnemy(coords, textures[identifier]);

                case NPCType.SecondPlaceholderEnemy:
                    return new SecondPlaceHolderEnemy(coords, textures[identifier]);
            }

            return null;
        }

        /// <summary>
        /// Jumps from the start of the file to the Terrain data at a specific x, y coordinate
        /// </summary>
        public void JumpTo(BinaryReader reader, int x, int y)
        {
            int mapWidth = reader.ReadInt32();
            // Throw an error if x is too large or small
            if (x < 0 || x > MapWidth)
            {
                throw new IndexOutOfRangeException("X coordinate is out of range!");
            }

            // Throw an error if y is too large or small
            int mapHeight = reader.ReadInt32();
            if (y < 0 || y > MapHeight)
            {
                throw new IndexOutOfRangeException("Y coordinate is out of range!");
            }
            
            // Skip unnecessary lines
            for (int i = 0; i < y; ++i)
            {
                for (int j = 0; j < mapWidth; ++j)
                {
                    SkipTerrain(reader);
                }
            }

            // Skip to the specified x coordinate
            for (int i = 0; i < x; ++i)
            {
                SkipTerrain(reader);
            }

        }

        /// <summary>
        /// Loads a column of Terrain structs
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="columnX">The X coordinate of the column</param>
        /// <param name="startY">The Y coordinate to start at</param>
        public void LoadColumn(BinaryReader reader, int columnX, int startY)
        {
            // Jump to the desired column
            JumpTo(reader, columnX, startY);

            for (int i = 0; i < loadedHeight; ++i)
            {
                // TODO: Read data about the Terrain

                // Skip to the next Terrain in the column
                for (int j = 0; j < mapWidth - 1; ++j)
                {
                    SkipTerrain(reader);
                }
            }
        }

        /// <summary>
        /// Loads a row of Terrain structs
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="startX">The X coordinate to start at</param>
        /// <param name="rowY">The Y coordinate of the row</param>
        public void LoadRow(BinaryReader reader, int startX, int rowY)
        {
            // Jump to the desired column
            JumpTo(reader, startX, rowY);

            for (int i = 0; i < loadedWidth; ++i)
            {
                // Read data about the terrain
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
