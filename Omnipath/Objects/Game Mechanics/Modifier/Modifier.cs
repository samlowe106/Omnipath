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
    /// Represents a status modifer
    /// </summary>
    abstract class Modifier
    {
        #region fields
        private double timeInitialized;
        private double delay;
        private IDamageable target;
        private IDealDamage source;
        #endregion

        #region constructor
        public Modifier(double delay, IDamageable target, IDealDamage source, Texture2D texture)
        {
            GameTime timer = new GameTime();
            timeInitialized = timer.ElapsedGameTime.TotalSeconds;
            this.delay = delay;
            this.target = target;
            this.source = source;
            Texture = texture;
        }
        #endregion

        #region methods
        public Boolean CheckIfReady(double currentTime)
        {
            return currentTime - timeInitialized >= delay;
        }
        #endregion

        public abstract void applyEffect();

        #region properties
        public Texture2D Texture { get; }
        #endregion
    }
}
