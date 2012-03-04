using System;
using System.Globalization;

namespace Moon.Extensions
{
    /// <summary>
    /// Extension methods on DateTime
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Checks the dates to ensure they are in the future.
        /// </summary>
        /// <param name="value">The date time.</param>
        /// <returns>A boolean representing if the date is in the future.</returns>
        public static Boolean InTheFuture(this DateTime value)
        {
            return value.CompareTo(DateTime.Now) > 0;
        }

        /// <summary>
        /// Checks the dates to ensure they are in the past.
        /// </summary>
        /// <param name="value">The date time.</param>
        /// <returns>A boolean representing if the date is in the past.</returns>
        public static Boolean InThePast(this DateTime value)
        {
            return value.CompareTo(DateTime.Now) < 0;
        }

        /// <summary>
        /// Determines whether the specified dateTime is after the dateTimeToCheck.
        /// </summary>
        /// <param name="value">The date time.</param>
        /// <param name="valueToCheck">The date time to check.</param>
        /// <returns>
        ///   <c>true</c> if the specified date time is before; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean IsAfter(this DateTime value, DateTime valueToCheck)
        {
            return value > valueToCheck;
        }

        /// <summary>
        /// Determines whether the specified dateTime is before the dateTimeToCheck.
        /// </summary>
        /// <param name="value">The date time.</param>
        /// <param name="valueToCheck">The date time to check.</param>
        /// <returns>
        ///   <c>true</c> if the specified date time is before; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean IsBefore(this DateTime value, DateTime valueToCheck)
        {
            return value < valueToCheck;
        }

        /// <summary>
        /// Prints the month and year.
        /// </summary>
        /// <param name="value">The date time.</param>
        /// <returns>A string representation of the date in month and year, printed in title case.</returns>
        public static string PrintMonthAndYear(this DateTime value)
        {
            return value.Print("MMMM yyyy");
        }

        /// <summary>
        /// Prints the datetime using the format string.
        /// </summary>
        /// <param name="value">The date time.</param>
        /// <param name="format">The format to print the string with.</param>
        /// <returns>A string representation of the date in month and year, printed in title case.</returns>
        public static string Print(this DateTime value, string format)
        {
            return value.ToString(format, CultureInfo.CurrentCulture).ToTitleCase();
        }

        /// <summary>
        /// Prints the datetime using dd/MM/yyyy as the format.
        /// </summary>
        /// <param name="value">The date time.</param>
        /// <returns>A string representation of the date in month and year, printed in title case.</returns>
        public static string Print(this DateTime value)
        {
            return value.Print("dd/MM/yyyy");
        }


        private static readonly CultureInfo Culture = new CultureInfo("en-GB");
        /// <summary>
        /// Prints the specified date and time, including ordinals.
        /// dddd, of MMMM yyyy at HH:mm:ss
        /// This print method always uses the en-GB culture.
        /// </summary>
        /// <param name="value">The date time.</param>
        /// <returns>A string representation of the date with the correct ordinals, printed in title case.</returns>
        public static string PrintWithOrdinals(this DateTime value)
        {

            return value.ToString
                    (
                     "dddd, '" +
                     AddOrdinal(value.Day) +
                     " of' MMMM yyyy 'at' HH:mm:ss",
                     Culture
                    ).ToTitleCase();
        }

        /// <summary>
        /// Adds the ordinal.
        /// </summary>
        /// <param name="day">The day.</param>
        /// <returns>The correct ordinal.</returns>
        private static string AddOrdinal(int day)
        {
            switch (day % 100)
            {
                case 11:
                case 12:
                case 13:
                    return day + "th";
            }
            switch (day % 10)
            {
                case 1:
                    return day + "st";
                case 2:
                    return day + "nd";
                case 3:
                    return day + "rd";
                default:
                    return day + "th";
            }
        }
    }
}