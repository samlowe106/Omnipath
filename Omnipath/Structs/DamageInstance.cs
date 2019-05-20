using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Omnipath
{
    /// <summary>
    /// Struct which represents a specific instance of damage
    /// </summary>
    struct DamageInstance
    {
        /// <summary>
        /// The name of this instance of damage
        /// </summary>
        public string Name;

        /// <summary>
        /// The type of damage done
        /// </summary>
        public DamageType Type;

        /// <summary>
        /// The amount of damage done
        /// </summary>
        public float Value;

        /// <summary>
        /// The object which inflicted this damage
        /// </summary>
        public IDealDamage Source;

        /// <summary>
        /// The icon for this instance of damage
        /// </summary>
        public Texture2D Icon;

        public DamageInstance(string name, DamageType type, float value, IDealDamage source, Texture2D icon)
        {
            this.Name = name;
            this.Type = type;
            this.Value = value;
            this.Source = source;
            this.Icon = icon;
        }
    }
}
