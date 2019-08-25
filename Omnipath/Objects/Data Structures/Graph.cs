using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omnipath
{
    class Graph
    {
        // Constants
        private const int COUNT = 5;
        // Fields
        private Dictionary<string, List<Terrain>> adjacencies;
        private Dictionary<string, Terrain> vertices;
        private Terrain[] TerrainArray;
        private int[,] adjacencyGrid;

        // Constructor
        public Graph() { }

        /// <summary>
        /// Performs a depth-first search on the graph
        /// </summary>
        /// <param name="name">The name of the Terrain at which to start</param>
        public void DepthFirst(string name)
        {
            // Return if the specified name is unrecognized
            if (!adjacencies.ContainsKey(name))
            {
                Console.WriteLine("Invalid name!");
                return;
            }
            Reset();
            Stack<Terrain> TerrainStack = new Stack<Terrain>();

            // Get the current Terrain, print its name, add it to the stack,
            //  mark is as visited
            TerrainStack.Push(vertices[name]);
            vertices[name].Visited = true;

            // While there's something on the stack:
            while (TerrainStack.Count > 0)
            {
                Terrain currentTerrain = TerrainStack.Peek();
                bool foundAdjacentUnvisited = false;
                // Loop through the list of adjacencies to find an adjacent and
                //  unvisited Terrain
                foreach (Terrain v in adjacencies[currentTerrain.Name])
                {
                    // When an unvisited Terrain has been found, print its name,
                    //  add it to the stack, and mark is as visited
                    if (!v.Visited)
                    {
                        Console.WriteLine(v.Name);
                        TerrainStack.Push(v);
                        v.Visited = true;
                        foundAdjacentUnvisited = true;
                    }
                }
                // If an adjacent, unvisited Terraint wasn't found,
                //  pop the current Terrain off the stack
                if (!foundAdjacentUnvisited)
                {
                    TerrainStack.Pop();
                }
            }
        }

        /// <param name="name">The name of a Terrain</param>
        /// <returns>
        /// The first unvisited Terrain adjacent to the specified Terrain
        /// </returns>
        public Terrain GetAdjacentUnvisited(string name)
        {
            // Ensure that the specified name is in the dictionary
            if (adjacencies.ContainsKey(name))
            {
                // Loop through each adjacent Terrain, returning the first unvisited adjacent Terrain
                foreach (Terrain v in adjacencies[name])
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
        /// Resets each Terrain's visited value to false
        /// </summary>
        public void Reset()
        {
            foreach (Terrain v in TerrainArray)
            {
                v.Visited = false;
            }
        }

        /// <param name="room">
        /// Room whose adjacencies will be returned
        /// </param>
        /// <returns>
        /// A list of the rooms adjacent to the specified room;
        /// null if the specified room does not exits
        /// </returns>
        public List<Terrain> GetAdjacentRooms(string room)
        {
            if (adjacencies.ContainsKey(room))
            {
                return adjacencies[room];
            }
            return null;
        }

        /// <param name="room1">
        /// The first room
        /// </param>
        /// <param name="room2">
        /// The second room
        /// </param>
        /// <returns>
        /// True if room2 is adjacent to room1, else false
        /// </returns>
        public bool IsConnected(string room1, string room2)
        {
            // If one of the rooms isn't recognized, return false
            if (!(adjacencies.ContainsKey(room1) && adjacencies.ContainsKey(room2)))
            {
                return false;
            }
            List<Terrain> adjacentRooms = GetAdjacentRooms(room1);
            // Search through room1's adjacencies for room2; if it's found, return true
            foreach (Terrain room in adjacentRooms)
            {
                if (room.Name == room2)
                {
                    return true;
                }
            }
            // room2 wasn't found in room1's adjacencies, return false
            return false;
        }

        /// <returns>
        /// A description of the specified room
        /// </returns>
        public string GetDescription(string room)
        {
            foreach (Terrain v in TerrainArray)
            {
                if (v.Name == room)
                {
                    return v.ToString();
                }
            }
            return null;
        }

        /// <summary>
        /// Lists all the vertices in the graph
        /// </summary>
        public void ListAllVertices()
        {
            foreach (Terrain v in TerrainArray)
            {
                Console.WriteLine(v);
            }
        }

        /// <summary>
        /// List of vertices in the graph
        /// </summary>
        public Terrain[] Vertices
        {
            get
            {
                return TerrainArray;
            }
        }
    }
}
