using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omnipath
{
    /// <summary>
    /// Represents different obstacles, furniture, and decorations that can exist in a map
    /// </summary>
    public enum Decoration
    {
        RidgeNorth = 0,         // One-way ridge that can only be crossed from north to south
        RidgeEast = 1,          // One-way ridge that can only be crossed from east to west
        RidgeSouth = 2,         // One-way ridge that can only be crossed from south to north
        RidgeWest = 3,          // One-way ridge that can only be crossed from west to east

        SmallBoulder = 4,       // A small, impassible boulder
        MediumBoulder = 5,      // A medium-seized, impassible boulder

        LargeBolderOne = 6,     // Northwestern tile of a four-tile boulder
        LargeBolderTwo = 7,     // Northeastern tile of a four-tile boulder
        LargeBolderThree = 8,   // Southwestern tile of a four-tile boulder
        LargeBolderFour = 9,    // Southeastern tile of a four-tile boulder
        
        TreeOne = 10,           // The first of three varieties of tree
        TreeTwo = 11,           // The second of three varieties of tree
        TreeThree = 12,         // The third of three varieties of tree

        TallGrass = 13,         // Tall grass

        
        Chest = 14,

        ChairNorth,             // A chair with its seat facing north
        ChairEast,
        ChairWest,
        ChairSouth,

        CrateSingle,
        CrateStacked,
        LargeVase,
        Cauldron,

        TreeStump,


    }
}
