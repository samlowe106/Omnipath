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
        private List<NPC> npcs;
        private Rectangle screen;
        #endregion

        #region Constructor
        public NPCManager(Rectangle screen, int count = -1)
        {
            if (count > -1)
            {
                npcs = new List<NPC>(count);
            }
            else
            {
                npcs = new List<NPC>();
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Updates each NPC managed by this object
        /// </summary>
        public void Update()
        {
            foreach (GameObject npc in npcs)
            {
                if (npc.Active)
                {
                    npc.Update();
                }
            }
        }

        /// <summary>
        /// Draws all on-screen, active objects using the specified SpriteBatch
        /// </summary>
        /// <param name="sp"></param>
        public void Draw(SpriteBatch sp)
        {
            foreach (GameObject npc in npcs)
            {
                if (screen.Intersects(npc.Rectangle) && npc.Active)
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
