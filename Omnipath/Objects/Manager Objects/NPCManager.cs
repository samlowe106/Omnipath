using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Omnipath
{
    /// <summary>
    /// Manages NPCs, calls update and draw methods, etc
    /// </summary>
    class NPCManager
    {
        #region Fields
        List<NPC> npcs;
        Rectangle screen;
        #endregion

        #region Constructor
        public NPCManager(Rectangle screen)
        {

        }
        #endregion

        #region Methods
        public void Update()
        {
            foreach (GameObject npc in npcs)
            {
                npc.Update();
            }
        }

        public void Draw(SpriteBatch sp)
        {
            foreach (GameObject npc in npcs)
            {
                if (screen.Contains(npc.Rectangle) && npc.Active)
                {
                    npc.Draw(sp);
                }
            }
        }

        /// <summary>
        /// Adds a new NPC to the list of current NPCs
        /// </summary>
        /// <param name="newNPC">New NPC</param>
        public void AddNPC(NPC newNPC)
        {
            npcs.Add(newNPC);
        }
        #endregion
    }
}
