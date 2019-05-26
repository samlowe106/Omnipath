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
    abstract class TargetedAbility : Ability
    {

        public TargetedAbility(GameObject user, Texture2D icon)
            : base(user, icon)
        {

        }

        public void Use(GameObject target)
        {
            
        }
    }
}
