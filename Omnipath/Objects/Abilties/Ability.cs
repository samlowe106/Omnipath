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
    /// Abstract class for abilities which players and NPCs can use
    /// May include a heal, an attack, a teleport, etc.
    /// </summary>
    abstract class Ability
    {
        #region Constructor
        public Ability(GameObject user, Texture2D icon)
        {
            this.User = user;
            this.Icon = icon;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Event which triggers when this ability is used
        /// </summary>
        public void OnUse()
        {

        }
        #endregion

        #region Properties
        /// <summary>
        /// User of this ability
        /// </summary>
        public GameObject User { get; }

        /// <summary>
        /// The max cooldown this ability has before being modified
        /// </summary>
        private float MaxCooldown
        {
            get
            {
                return MaxCooldown;
            }
        }

        /// <summary>
        /// The current, post-modifier cooldown this ability will have
        /// </summary>
        public float CurrentCooldown
        {
            get
            {
                return MaxCooldown; // calculate any modifiers here
            }
        }

        /// <summary>
        /// Countdown until this ability is off cooldown and can be used again
        /// </summary>
        public float TimeUntilUsable
        {
            get; // TODO
        }

        /// <summary>
        /// True if this ability has been used too recently and has not cooled down yet
        /// False if this ability is currently usable
        /// </summary>
        public bool OnCooldown
        {
            get
            {
                return TimeUntilUsable > 0;
            }
        }

        /// <summary>
        /// The icon of this abililty to be displayed in the HUD and UI
        /// </summary>
        public Texture2D Icon { get; }
        #endregion
    }
}
