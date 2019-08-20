using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omnipath
{
    /// <summary>
    /// An enumeration for NPCs and enemies
    /// NPCs have a unique ID, enemies typically don't
    /// </summary>
    public enum NPCType
    {
        // 0 is reserved for "no NPC here"
        PlaceHolderEnemy = 1,
        SecondPlaceholderEnemy = 2
    }
}
