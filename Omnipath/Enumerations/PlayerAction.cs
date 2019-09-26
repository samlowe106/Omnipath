using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omnipath
{
    /// <summary>
    /// Actions that the player can take while the game is open; associated with key presses
    /// </summary>
    enum PlayerAction
    {
        Pause = 0,
        MainAttack = 1,
        SecondaryAttack = 2,
        MoveNorth,
        MoveEast,
        MoveSouth,
        MoveWest

    }
}
