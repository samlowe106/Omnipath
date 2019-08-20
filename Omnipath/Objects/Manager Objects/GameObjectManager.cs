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
    class GameObjectManager<T> where T : GameObject
    {
        #region Fields
        private List<T> managedObjects;
        private Rectangle screen;
        #endregion

        #region Constructor
        public GameObjectManager(Rectangle screen)
        {
            managedObjects = new List<T>();
        }

        public GameObjectManager(Rectangle screen, List<T> objects)
        {
            managedObjects = objects;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Updates each NPC managed by this object
        /// </summary>
        public void Update()
        {
            foreach (T obj in managedObjects)
            {
                if (obj.Active)
                {
                    obj.Update();
                }
            }
        }

        /// <summary>
        /// Draws all on-screen, active objects using the specified SpriteBatch
        /// </summary>
        /// <param name="sp"></param>
        public void Draw(SpriteBatch sp)
        {
            foreach (T npc in npcs)
            {
                if (screen.Intersects(npc.Rectangle) && npc.Active)
                {
                    npc.Draw(sp);
                }
            }
        }

        /// <returns>True if this game object manager is managing the specified object</returns>
        public bool IsManaged(T obj)
        {
            return managedObjects.Contains(obj);
        }

        /// <summary>
        /// Adds a new NPC to the list of current NPCs
        /// </summary>
        /// <param name="newNPC">New NPC</param>
        public void AddNPC(T newNPC)
        {
            npcs.Add(newNPC);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Returns number of NPCs currently being managed
        /// </summary>
        public int Count
        {
            get
            {
                return managedObjects.Count;
            }
        }
        #endregion
    }
}
