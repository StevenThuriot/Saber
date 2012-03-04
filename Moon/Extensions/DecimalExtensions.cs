using System;
using System.Globalization;

namespace Moon.Extensions
{
    ///<summary>
    /// Extension methods on decimals
    ///</summary>
    public static class DecimalExtensions
    {
        ///<summary>
        /// Gets the fractionals for the given decimal.
        ///</summary>
        ///<param name="value">The value</param>
        ///<returns>The fractionals</returns>
        public static decimal GetFractional(this decimal value)
        {
            return value - Math.Truncate(value);
        }
        
        ///<summary>
        /// Gets the fractionals for the given decimal.
        ///</summary>
        ///<param name="value">The value</param>
        ///<returns>The fractionals</returns>
        public static decimal GetFractional(this decimal? value)
        {
            return value.GetFractional(0);
        }

        ///<summary>
        /// Gets the fractionals for the given decimal.
        ///</summary>
        ///<param name="value">The value</param>
        ///<param name="defaultValue">The default value, in case "value" is null.</param>
        ///<returns>The fractionals</returns>
        public static decimal GetFractional(this decimal? value, decimal defaultValue)
        {
            return value.HasValue ? value.Value.GetFractional() : defaultValue;
        }

        ///<summary>
        /// Prints the specified number with the chosen number of decimals.
        ///</summary>
        ///<param name="value">The number to print.</param>
        ///<param name="numberOfDecimals">The amount of decimals.</param>
        ///<returns>A string of the number with the chosen amount of decimals.</returns>
        public static string Print(this double value, byte numberOfDecimals)
        {
            return value.ToString("N" + numberOfDecimals, CultureInfo.CurrentCulture);
        }
    }
}
