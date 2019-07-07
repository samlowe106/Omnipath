using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Omnipath
{
    /// <summary>
    /// A playable area loaded from a file
    /// Centers on the camera and dynamically loads/unloads terrain based on the screen width/height
    /// </summary>
    class Map
    {
        private int mapWidth;
        private int mapHeight;

        private int screenWidth;
        private int screenHeight;

        private int centerX;
        private int centerY;

        private Graph localMap;

        public Map(string fileName, int centerX, int centerY, int screenWidth, int screenHeight)
        {
            localMap = new Graph();
            Load(fileName);
        }

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
                // Repeat as necessary:
                //      Skip over unnecessary X-coordinates
                //      Read in tile and decoration data
                //      Move to next line
                
                // Read in enemy spawn points once and only once
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
        /// The maximum width of this map
        /// </summary>
        public int Width
        {
            get
            {
                return mapWidth;
            }
        }

        /// <summary>
        /// The maximum height of this map
        /// </summary>
        public int Height
        {
            get
            {
                return mapHeight;
            }
        }

    }
}
