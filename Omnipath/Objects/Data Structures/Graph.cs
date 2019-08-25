using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omnipath
{
    class Graph
    {
        #region Fields
        private Dictionary<Terrain, List<Terrain>> adjacencies;
        private Terrain[,] terrainArray;
        private int[,] adjacencyGrid;
        #endregion

        #region Constructor
        public Graph() { }
        #endregion

        #region Methods
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

        /// <param name="name">The name of a Terrain</param>
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

        #endregion

        #region Properties
        /// <summary>
        /// 2d array of vertices in the graph
        /// </summary>
        public Terrain[,] Vertices
        {
            get
            {
                return terrainArray;
            }
        }
        #endregion
    }
}
