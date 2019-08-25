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
    /// A graph of terrain, representing playable area loaded from a file
    /// Dynamically loads/unloads terrain based on the camera position, width, and height
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

        /// <summary>
        /// The maximum width of this map
        /// </summary>
        public readonly int MapWidth;

        /// <summary>
        /// The maximum height of this map
        /// </summary>
        public readonly int MapHeight;

        private GameObject centerObject;
        #endregion

        #region Constructor
        public Map(string fileName, GameObject centerObject, int activeWidth, int activeHeight, int loadedWidth, int loadedHeight, Texture2D[] textures)
        {
            this.textures = textures;
            this.activeWidth = activeWidth;
            this.activeHeight = activeHeight;
            this.loadedWidth = loadedWidth;
            this.loadedHeight = loadedHeight;
            this.centerObject = centerObject;

            #region Read map data from file
            FileStream inStream = null;
            BinaryReader reader = null;

            try
            {
                inStream = File.OpenRead(fileName);
                reader = new BinaryReader(inStream);

                // Begin reading data from the file

                MapWidth = reader.ReadInt32();
                MapHeight = reader.ReadInt32();

                terrainArray = new Terrain[MapWidth, MapHeight];

                // Read map data to create a rectangle (with dimensions based on the screenWidth and screenHeight)
                //  centered on (centerX, centerY)

                // Skip over unnecessary lines
                for (int i = 0; i < CenterY - (loadedHeight / 2); ++i)
                {
                    for (int j = 0; j < MapWidth; ++j)
                    {
                        SkipTerrain(reader);
                    }
                }

                // Read data from the current line
                for (int i = 0; i < MapWidth; ++i)
                {
                    // Skip over unnecessary X-coordinates at the start of the current line
                    for (int j = 0; j < CenterX - (loadedWidth / 2); ++j)
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
                        terrainArray[i, j].PassableNorth = reader.ReadBoolean();
                        terrainArray[i, j].Rectangle = new Rectangle(reader.ReadInt32(), reader.ReadInt32(), TERRAIN_DIMENSIONS, TERRAIN_DIMENSIONS);
                        terrainArray[i, j].OccupantID = (NPCType)reader.ReadInt32();
                    }

                    // Skip over unnecessary x coordinates at the end of the current line
                    for (int j = 0; j < MapWidth - (CenterX + (loadedWidth / 2)); ++j)
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
        /// <returns>
        /// An array of the terrain North, East, South, and West of
        /// the Terrain at the specified (x,y) coordinate pair
        /// </returns>
        public Terrain[] EstablishAdjacent(int xCoord, int yCoord)
        {
            if (0 > xCoord || xCoord > MapWidth || 0 > yCoord || yCoord > MapHeight)
            {
                throw new IndexOutOfRangeException();
            }
            
            Terrain[] adjacencies = new Terrain[4];

            // North
            if (yCoord != 0 && terrainArray[xCoord, yCoord - 1] != null && terrainArray[xCoord, yCoord - 1].PassableSouth)
            {
                adjacencies[(int)Directions.North] = terrainArray[xCoord, yCoord - 1];
            }
            else
            {
                adjacencies[(int)Directions.North] = null;
            }
            // East
            if (xCoord != MapWidth && terrainArray[xCoord + 1, yCoord] != null && terrainArray[xCoord + 1, yCoord].PassableWest)
            {
                adjacencies[(int)Directions.East] = terrainArray[xCoord + 1, yCoord];
            }
            else
            {
                adjacencies[(int)Directions.East] = null;
            }
            // South
            if (yCoord != MapHeight && terrainArray[xCoord, yCoord + 1] != null && terrainArray[xCoord, yCoord + 1].PassableNorth)
            {
                adjacencies[(int)Directions.South] = (terrainArray[xCoord, yCoord + 1]);
            }
            else
            {
                adjacencies[(int)Directions.South] = null;
            }
            // West
            if (xCoord != 0 && terrainArray[xCoord - 1, yCoord] != null && terrainArray[xCoord - 1, yCoord].PassableWest)
            {
                adjacencies[(int)Directions.West] = terrainArray[xCoord - 1, yCoord];
            }
            else
            {
                adjacencies[(int)Directions.West] = null;
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
                        adjacencies[currentTerrain][i].Visited = true;
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

        /// <returns>The first Terrain adjacent to the specified Terrain that is
        /// unvisited and enter-able from the specified Terrrain</returns>
        private Terrain GetAdjacentUnvisited(Terrain tile)
        {
            // Ensure that the specified name is in the dictionary
            if (adjacencies.ContainsKey(tile))
            {
                // Go through each adjacent Terrain, returning the first unvisited, adjacent, and passable Terrain
                if (adjacencies[tile][(int)Directions.North] != null && !adjacencies[tile][(int)Directions.North].Visited)
                {
                    return adjacencies[tile][(int)Directions.North];
                }
                if (adjacencies[tile][(int)Directions.East] != null && !adjacencies[tile][(int)Directions.East].Visited)
                {
                    return adjacencies[tile][(int)Directions.East];
                }
                if (adjacencies[tile][(int)Directions.South] != null && !adjacencies[tile][(int)Directions.South].Visited)
                {
                    return adjacencies[tile][(int)Directions.South];
                }
                if (adjacencies[tile][(int)Directions.West] != null && !adjacencies[tile][(int)Directions.West].Visited)
                {
                    return adjacencies[tile][(int)Directions.West];
                }
            }
            // Return null if the specified name is invalid
            //  or no valid Terrain was found
            return null;
        }

        /// <summary>
        /// Resets each Terrain's Visited value to false
        /// </summary>
        private void Reset()
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
        /// True if tile2 is passable from tile1, else false
        /// </returns>
        public bool IsConnected(Terrain tile1, Terrain tile2)
        {
            // If one of the tiles is unrecognized, return false
            if (!(adjacencies.ContainsKey(tile1) && adjacencies.ContainsKey(tile2)))
            {
                return false;
            }
            List<Terrain> adjacentTiles = GetAdjacencies(tile1);
            // Search through tile1's adjacencies for tile2; if it's found, return true
            foreach (Terrain tile in adjacentTiles)
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
        private void SkipTerrain(BinaryReader reader)
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
        private GameObject GetGameObject(int identifier, Point coords)
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
        private void JumpTo(BinaryReader reader, int x, int y)
        {
            int MapWidth = reader.ReadInt32();
            // Throw an error if x is too large or small
            if (x < 0 || x > MapWidth)
            {
                throw new IndexOutOfRangeException("X coordinate is out of range!");
            }

            // Throw an error if y is too large or small
            int MapHeight = reader.ReadInt32();
            if (y < 0 || y > MapHeight)
            {
                throw new IndexOutOfRangeException("Y coordinate is out of range!");
            }
            
            // Skip unnecessary lines
            for (int i = 0; i < y; ++i)
            {
                for (int j = 0; j < MapWidth; ++j)
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
                return new Rectangle(CenterX - (activeWidth / 2), CenterY - (activeHeight / 2), activeWidth, activeHeight);
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
                return new Rectangle(CenterX - (loadedWidth / 2), CenterY - (loadedHeight / 2), loadedWidth, loadedHeight);
            }
        }

        /// <summary>
        /// Number of Tiles in the Loaded Zone
        /// </summary>
        public int CountLoaded
        {
            get
            {
                return loadedWidth * loadedHeight;
            }
        }

        /// <summary>
        /// Number of Tiles in the Active Zone
        /// </summary>
        public int CountActive
        {
            get
            {
                return activeWidth * activeHeight;
            }
        }

        /// <summary>
        /// How many total tiles this map can have
        /// </summary>
        public int Size
        {
            get
            {
                return MapWidth * MapHeight;
            }
        }

        /// <summary>
        /// The X-Coordinate this map is centered on
        /// </summary>
        private int CenterX
        {
            get
            {
                return centerObject.Center.X;
            }
        }
        
        /// <summary>
        /// The Y-Coordinate this map is centered on
        /// </summary>
        private int CenterY
        {
            get
            {
                return centerObject.Center.Y;
            }
        }
        #endregion
    }
}
