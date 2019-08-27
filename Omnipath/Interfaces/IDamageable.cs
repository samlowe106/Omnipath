using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omnipath
{
    /// <summary>
    /// Interface for objects which can take damage
    /// </summary>
    interface IDamageable
    {
        /// <summary>
        /// The amount of Health this object has
        /// </summary>
        Resource Health { get; }

        /// <summary>
        /// Function that causes this object to take damage
        /// </summary>
        /// <param name="damage">Amount of damage dealt</param>
        /// <returns>Amount of damage taken by this object after damage is calculated</returns>
        float TakeDamage(IDealDamage source, DamageInstance[] damageInstances);
    }
}
