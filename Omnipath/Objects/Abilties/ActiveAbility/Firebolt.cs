using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Omnipath.Objects.Abilties.ActiveAbility
{
    class Firebolt : Ability
    {
        public Firebolt(GameObject go) : base(go, Game1.abilitiesTextures[(int)TextureID.Firebolt][0], Game1.abilitiesTextures[(int)TextureID.Firebolt])
        {
            
        }
    }
}
