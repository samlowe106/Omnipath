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
        /// The maximum possible value for this resource to have
        /// </summary>
        public int MaxValue { get; set; }

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
                // Ensure the new value can't be less than the minimum possible value
                if (value > MaxValue)
                {
                    CurrentValue = MaxValue;
                }
                // Ensure the new value can't be less than the minimum possible value
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
        /// Returns the percent of health that's missing (ranges from 100.0 to 0.0)
        /// </summary>
        public float PercentMissing
        {
            get
            {
                return 100 * ((MaxValue - CurrentValue) / MaxValue);
            }
        }

        /// <summary>
        /// Returns the current percent of health remaining
        /// </summary>
        public float PercentRemaining
        {
            get
            {
                return 100 * (CurrentValue / MaxValue);
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
