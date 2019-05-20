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
        /// Maximum amount of health that this object can have
        /// </summary>
        int MaximumHealth { get; }

        /// <summary>
        /// Current amount of health that this object has
        /// </summary>
        float CurrentHealth { get; }

        /// <summary>
        /// Difference between this object's maximum and current health
        /// </summary>
        float MissingHealth { get; }

        /// <summary>
        /// Percentage of how much of this object's maximum health is remaining
        /// 100% = full health, 0% = no health
        /// </summary>
        float CurrentHealthPercentage { get; }

        /// <summary>
        /// Equal to percent * MissingHealth
        /// </summary>
        /// <param name="percent">Percentage of missing health to be returned</param>
        /// <returns></returns>
        float PercentMissingHealth(float percent);

        /// <summary>
        /// Equal to percent * CurrentHealth
        /// </summary>
        /// <param name="percent">Percentage of current health to be returned</param>
        /// <returns></returns>
        float PercentCurrentHealth(float percent);

        /// <summary>
        /// Function that causes this object to take damage
        /// </summary>
        /// <param name="damage">Amount of damage dealt</param>
        /// <returns>Amount of damage taken by this object after damage is calculated</returns>
        float TakeDamage(IDealDamage source, DamageInstance[] damageInstances);
    }
}
