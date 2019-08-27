using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace Omnipath
{
    class Player : GameObject
    {
        #region Fields
        Dictionary<Keys, PlayerAction> controlMapping;

        #region Resources
        Resource health;
        Resource Mana;
        Resource Stamina;
        #endregion

        #region Stats
        float fireresist;
        float fireAffinity;
        float magicAttack;
        
        #endregion

        #endregion

        /// <summary>
        /// Most of the initialization logic is done in the initalize method
        /// </summary>
        /// <param name="texture"></param>
        public Player(Texture2D texture, Dictionary<Keys, PlayerAction> controlMapping)
            : base(new Rectangle(), texture)
        {
            this.controlMapping = controlMapping;
        }

        /// <summary>
        /// Reads information about the Player from a file
        /// </summary>
        /// <returns>
        /// False if the specified file can't be found and read
        /// True if a file can be found, read, and can be used to initialize Player data
        /// </returns>
        public bool Initialize()
        {
            return true;
            /*
            if ()
            {
                return true;
            }
            else
            {
                return false;
            }
            */

        }

        public void Update(KeyboardState currentkbState, KeyboardState previouskbState,
            MouseState currentMouseState, MouseState previousMouseState)
        {
            ProcessInput(currentkbState, previouskbState, currentMouseState, previousMouseState);



            
            base.Update();
        }

        /// <summary>
        /// Processes the player's inputs and acts accordingly
        /// </summary>
        /// <param name="currentState"></param>
        public void ProcessInput(KeyboardState currentkbState, KeyboardState previouskbState,
            MouseState currentMouseState, MouseState previousMouseState)
        {
            if (currentMouseState.RightButton == ButtonState.Pressed /* And mouse is on-screen */)
            {
                AStar(currentMouseState.Position);
            }

            // Process keyboard input
            foreach (Keys k in currentkbState.GetPressedKeys())
            {
                if (controlMapping.ContainsKey(k))
                {
                    switch (controlMapping[k])
                    {



                    }
                }
            }
        }
    }
}
