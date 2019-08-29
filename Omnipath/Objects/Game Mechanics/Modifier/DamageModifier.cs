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
    abstract class DamageModifier : Modifier
    {
        #region fields
        private DamageType type;
        private int damage;
        private Boolean percentBased;
        #endregion

        public DamageModifier(double delay, IDamageable target, IDealDamage source, Texture2D texture, DamageType type, int damage, Boolean percentBased) :base(delay, target, source, texture)
        {
            this.type = type;
            this.damage = damage;
            this.percentBased = percentBased;
        }

        override
        public void applyEffect()
        {
            int total = this.damage;
            if (type.Equals(DamageType.Burn))
            {
                
            }
            if (type.Equals(DamageType.Frost))
            {

            }
            if (type.Equals(DamageType.Shock))
            {

            }
            if (type.Equals(DamageType.Light))
            {

            }
            if (type.Equals(DamageType.Dark))
            {

            }
            if (type.Equals(DamageType.Impact))
            {

            }
            if (type.Equals(DamageType.Pierce))
            {

            }
            if (type.Equals(DamageType.Slice))
            {

            }
            if (type.Equals(DamageType.Poison))
            {

            }
        }

    }
}
