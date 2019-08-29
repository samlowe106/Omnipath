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
    class ModifierManager
    {
        #region fields
        private List<Modifier> modifiers;
        private List<Modifier> applyNow;
        private GameTime timer;
        private double currentTime;
        #endregion

        #region constructor
        public ModifierManager()
        {
            modifiers = new List<Modifier>();
            applyNow = new List<Modifier>();
            timer = new GameTime();
        }
        #endregion

        #region methods
        public void update()
        {
            for (int i = modifiers.Count - 1; i > -1; --i)
            {
                if (modifiers[i].CheckIfReady(currentTime))
                {
                    applyNow.Add(modifiers[i]);
                    modifiers.RemoveAt(i);
                }
            }
            for(int i = applyNow.Count - 1; i > -1; i--)
            {
                applyNow[i].applyEffect();
                applyNow.RemoveAt(i);
            }
        }
        #endregion

        #region properties
        public double CurrentTime
        {
            get
            {
                return timer.ElapsedGameTime.TotalSeconds;
            }
        }
        #endregion
    }
}
