using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omnipath
{
    /// <summary>
    /// Resource that could consist of health, mana, stamina, etc. and will probably be represented by a bar
    /// </summary>
    class Resource
    {
        private int currentValue;

        #region Constructor
        public Resource() { }
        #endregion

        #region Properties
        /// <summary>
        /// The maximum possible value for this resource to have (after modifiers)
        /// </summary>
        public int EffectiveMaxValue { get; set; }

        /// <summary>
        /// The maximum value this resource can have (before modifiers)
        /// </summary>
        public int BaseMaxValue { get; }

        /// <summary>
        /// Current value of 
        /// </summary>
        public int CurrentValue
        {
            get
            {
                return currentValue;
            }
            set
            {
                // Ensure the new value can't be less than the current maximum possible value
                if (value > EffectiveMaxValue)
                {
                    CurrentValue = EffectiveMaxValue;
                }
                // Ensure the new value can't be less than 0
                else if (value < 0)
                {
                    CurrentValue = 0;
                }
                else
                {
                    CurrentValue = value;
                }
            }
        }


        /// <summary>
        /// Equal to percent * AmountMissing
        /// </summary>
        /// <param name="percent">Percentage of missing resource to be returned</param>
        /// <returns></returns>
        public float PercentOfAmountMissing(float percent)
        {
            return percent * AmountMissing;
        }

        /// <summary>
        /// Equal to percent * CurrentValue
        /// </summary>
        /// <param name="percent">Percentage of current health to be returned</param>
        /// <returns></returns>
        public float PercentOfCurrentValue(float percent)
        {
            return percent * CurrentValue;
        }

        /// <summary>
        /// Equal to this resource's EffectiveMaxValue minus its CurrentValue
        /// </summary>
        public int AmountMissing
        {
            get
            {
                return EffectiveMaxValue - CurrentValue;
            }
        }

        /// <summary>
        /// Returns the percent of this resource that's missing (ranges from 100.0 to 0.0), rounded to two decimals
        /// </summary>
        public float PercentMissing
        {
            get
            {
                return (float)Math.Round(100f * (AmountMissing / EffectiveMaxValue), 2);
            }
        }

        /// <summary>
        /// Returns the current percent of this resource remaining
        /// </summary>
        public float PercentRemaining
        {
            get
            {
                return 100f * (CurrentValue / EffectiveMaxValue);
            }
        }

        /// <summary>
        /// Amount of this resource that will be regenerated per second
        /// </summary>
        public float RegenerationPerSecond { get; set; }

        /// <summary>
        /// List of stat modifiers affecting this resource
        /// ---could be a piority queue in order of modifier duration---
        /// </summary>
        public List<Modifier> Modifiers { get; }
        #endregion
    }
}
