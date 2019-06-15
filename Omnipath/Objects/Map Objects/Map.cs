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
    /// </summary>
    class Map
    {

        private int width;
        private int height;

        private Graph localMap;

        public Map(string fileName)
        {
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

                width = reader.ReadInt32();
                height = reader.ReadInt32();

                // Establish the 2d array which will hold the map in memory
                tiles = new Tile[width, height];

                // TODO: READ DATA HERE
                
            }
            // Catch any exceptions
            catch (Exception e)
            {
                // TODO: PRINT ERROR MESSAGES
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

        public int Width
        {
            get
            {
                return width;
            }
        }

        public int Height
        {
            get
            {
                return Height;
            }
        }

    }
}
