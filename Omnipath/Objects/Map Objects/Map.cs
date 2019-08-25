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
        private Dictionary<Terrain, List<Terrain>> adjacencies;
        private Terrain[,] terrainArray;

        private Texture2D[] textures;

        private int activeWidth;
        private int activeHeight;

        private int loadedWidth;
        private int loadedHeight;

        private int mapWidth;
        private int mapHeight;

        private int centerX;
        private int centerY;
        #endregion

        #region Constructor
        public Map(string fileName, int centerX, int centerY, int activeWidth, int activeHeight, int loadedWidth, int loadedHeight, Texture2D[] textures)
        {
            this.textures = textures;
            this.activeWidth = activeWidth;
            this.activeHeight = activeHeight;
            this.loadedWidth = loadedWidth;
            this.loadedHeight = loadedHeight;

            #region Read map data from file
            FileStream inStream = null;
            BinaryReader reader = null;

            try
            {
                inStream = File.OpenRead(fileName);
                reader = new BinaryReader(inStream);

                // Begin reading data from the file

                mapWidth = reader.ReadInt32();
                mapHeight = reader.ReadInt32();

                terrainArray = new Terrain[mapWidth, mapHeight];

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

                        terrainArray[i, j].Textures = animationFrames;
                        terrainArray[i, j].Passable = reader.ReadBoolean();
                        terrainArray[i, j].Rectangle = new Rectangle(reader.ReadInt32(), reader.ReadInt32(), TERRAIN_DIMENSIONS, TERRAIN_DIMENSIONS);
                        terrainArray[i, j].OccupantID = (NPCType)reader.ReadInt32();
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
            #endregion
        }
        #endregion

        #region Methods
        /// <param name="xCoord"></param>
        /// <param name="yCoord"></param>
        /// <returns>A list of all the terrain adjacent to the specified (x,y) coordinate pair</returns>
        public List<Terrain> EstablishAdjacent(int xCoord, int yCoord)
        {
            if (0 > xCoord || xCoord > mapWidth || 0 > yCoord || yCoord > mapHeight)
            {
                throw new IndexOutOfRangeException();
            }
            
            List<Terrain> adjacencies = new List<Terrain>();

            // North
            if (yCoord != 0)
            {
                adjacencies.Add(terrainArray[xCoord, yCoord - 1]);
            }
            // East
            if (xCoord != mapWidth)
            {
                adjacencies.Add(terrainArray[xCoord + 1, yCoord]);
            }
            // South
            if (yCoord != mapHeight)
            {
                adjacencies.Add(terrainArray[xCoord, yCoord + 1]);
            }
            // West
            if (xCoord != 0)
            {
                adjacencies.Add(terrainArray[xCoord - 1, yCoord]);
            }

            return adjacencies;
        }


        /// <summary>
        /// Performs a depth-first search on the graph
        /// </summary>
        /// <param name="name">The name of the Terrain at which to start</param>
        public void DepthFirst(Terrain tile)
        {
            // Throw an error if the specified tile is unrecognized
            if (!adjacencies.ContainsKey(tile))
            {
                throw new KeyNotFoundException("The specified tile couldn't be found in this graph!");
            }
            Reset();
            Stack<Terrain> TerrainStack = new Stack<Terrain>();

            // Get the current Terrain, print its name, add it to the stack,
            //  mark is as visited
            TerrainStack.Push(tile);
            tile.Visited = true;

            // While there's something on the stack:
            while (TerrainStack.Count > 0)
            {
                Terrain currentTerrain = TerrainStack.Peek();
                bool foundAdjacentUnvisited = false;
                // Loop through the list of adjacencies to find an adjacent and unvisited Terrain
                for (int i = 0; i < adjacencies[currentTerrain].Count; ++i)
                {
                    // When an unvisited Terrain has been found, add it to the stack, and mark is as visited
                    if (!adjacencies[currentTerrain][i].Visited)
                    {
                        // These assignments are necessary to change the Visited property of the Terrain to true
                        Terrain visitedTerrain = adjacencies[currentTerrain][i];
                        visitedTerrain.Visited = true;
                        adjacencies[currentTerrain][i] = visitedTerrain;
                        // Push that visited terriain to the stack and
                        //  note that an adjacent unvisited piece of terrain has been found
                        TerrainStack.Push(adjacencies[currentTerrain][i]);
                        foundAdjacentUnvisited = true;
                    }
                }

                // If an adjacent, unvisited Terraint wasn't found, pop the current Terrain off the stack
                if (!foundAdjacentUnvisited)
                {
                    TerrainStack.Pop();
                }
            }
        }

        /// <returns>
        /// The first unvisited Terrain adjacent to the specified Terrain
        /// Null if no adjacent unvisited Terrain could be found
        /// </returns>
        public Terrain? GetAdjacentUnvisited(Terrain tile)
        {
            // Ensure that the specified name is in the dictionary
            if (adjacencies.ContainsKey(tile))
            {
                // Loop through each adjacent Terrain, returning the first unvisited adjacent Terrain
                foreach (Terrain v in adjacencies[tile])
                {
                    if (!v.Visited)
                    {
                        return v;
                    }
                }
            }
            // Return null if the specified name is invalid
            //  or if no unvisited adjacent Terrain could be found
            return null;
        }

        public Terrain? GetAdjacentPassable(Terrain tile)
        {
            // Ensure that the specified name is in the dictionary
            if (adjacencies.ContainsKey(tile))
            {
                // Loop through each adjacent Terrain, returning the first unvisited, adjacent, and passable Terrain
                foreach (Terrain v in adjacencies[tile])
                {
                    if (!v.Visited && v.Passable)
                    {
                        return v;
                    }
                }
            }
            // Return null if the specified name is invalid
            //  or if no unvisited adjacent Terrain could be found
            return null;
        }

        /// <summary>
        /// Resets each Terrain's Visited value to false
        /// </summary>
        public void Reset()
        {
            for (int i = 0; i < terrainArray.GetLength(0); ++i)
            {
                for (int j = 0; j < terrainArray.GetLength(1); ++i)
                {
                    terrainArray[i, j].Visited = false;
                }
            }
        }

        /// <returns>
        /// List of tiles adjacent to the specified tile
        /// null if the specified tile does not exist
        /// </returns>
        public List<Terrain> GetAdjacencies(Terrain tile)
        {
            if (adjacencies.ContainsKey(tile))
            {
                return adjacencies[tile];
            }
            return null;
        }

        /// <param name="tile1">
        /// The first tile
        /// </param>
        /// <param name="tile2">
        /// The second tile
        /// </param>
        /// <returns>
        /// True if tile2 is adjacent to tile1, else false
        /// </returns>
        public bool IsConnected(Terrain tile1, Terrain tile2)
        {
            // If one of the tiles is unrecognized, return false
            if (!(adjacencies.ContainsKey(tile1) && adjacencies.ContainsKey(tile2)))
            {
                return false;
            }
            List<Terrain> adjacenttiles = GetAdjacencies(tile1);
            // Search through tile1's adjacencies for tile2; if it's found, return true
            foreach (Terrain tile in adjacenttiles)
            {
                if (ReferenceEquals(tile, tile2))
                {
                    return true;
                }
            }
            // tile2 wasn't found in tile1's adjacencies, return false
            return false;
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
        #endregion

        #region Indexer
        public Terrain this[int x, int y]
        {
            get
            {
                return terrainArray[x, y];
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
