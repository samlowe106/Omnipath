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
    class Player : GameObject, IDamageable
    {
        #region Fields
        Dictionary<Keys, PlayerAction> controlMapping;

        #region Resources
        Resource health;
        Resource Mana;
        Resource stamina;
        #endregion

        #region Stats

        #region base stats
        private int physicalDamage{ get; set; }
        private int magicalDamage{ get; set; }
        private int speed{ get; set; }
        private int physicalDefense{ get; set; }
        private int magicalDefense{ get; set; }
        #endregion

        #region recovery stats
        private int healthRecovery{ get; set; }
        private int manaRecovery{ get; set; }
        private int staminaRecovery{ get; set; }
        #endregion

        #region elemental stats
        private int burnResist{ get; set; }
        private int burnAffinity{ get; set; }
        private int frostResist{ get; set; }
        private int frostAffinity{ get; set; }
        private int shockResist{ get; set; }
        private int shockAffinity{ get; set; }
        private int lightResist{ get; set; }
        private int lightAffinity{ get; set; }
        private int darkAffinity{ get; set; }
        private int darkResist{ get; set; }
        #endregion

        #region physical stats
        private int impactResist{ get; set; }
        private int impactAffinity{ get; set; }
        private int pierceResist{ get; set; }
        private int pierceAffinity{ get; set; }
        private int sliceResist{ get; set; }
        private int sliceAffinity{ get; set; }
        private int poisonResist{ get; set; }
        private int poisonAffinity{ get; set; }


        #endregion

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

        public float TakeDamage(IDealDamage source, DamageInstance[] damageInstances)
        {
            float damageTotal = 0;
            foreach (DamageInstance d in damageInstances)
            {
                damageTotal += d.Value;
            }

            // Subract the amount of damage taken from health, rounding down
            health.CurrentValue -= (int)damageTotal;

            // Return the amount of damage done
            return damageTotal;
        }

        public Resource Health { get; }
    }
}
