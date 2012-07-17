using System;
using System.Globalization;

namespace Saber.Extensions
{
    ///<summary>
    /// Extension methods on doubles.
    ///</summary>
    public static class DoubleExtensions
    {
        ///<summary>
        /// Gets the fractionals for the given double.
        ///</summary>
        ///<param name="value">The value</param>
        ///<returns>The fractionals</returns>
        public static double GetFractional(this double value)
        {
            return value - Math.Truncate(value);
        }
        
        ///<summary>
        /// Gets the fractionals for the given double.
        ///</summary>
        ///<param name="value">The value</param>
        ///<returns>The fractionals</returns>
        public static double GetFractional(this double? value)
        {
            return value.GetFractional(0);
        }

        ///<summary>
        /// Gets the fractionals for the given double.
        ///</summary>
        ///<param name="value">The value</param>
        ///<param name="defaultValue">The default value, in case "value" is null.</param>
        ///<returns>The fractionals</returns>
        public static double GetFractional(this double? value, double defaultValue)
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
