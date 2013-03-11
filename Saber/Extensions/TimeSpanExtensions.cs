#region License

// 
//  Copyright 2013 Steven Thuriot
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// 

#endregion

using System;

namespace Saber.Extensions
{
    /// <summary>
    /// Extensions to create readable timespans.
    /// </summary>
    public static class TimeSpanExtensions
    {
        /// <summary>
        /// Turns the supplied int into a TimeSpan.
        /// </summary>
        /// <param name="hours">The hours.</param>
        /// <returns>The relevant TimeSpan.</returns>
        /// <example>5.Hours() + 4.Minutes()</example>
        public static TimeSpan Hours(this int hours)
        {
            return new TimeSpan(hours, 0, 0);
        }

        /// <summary>
        /// Turns the supplied int into a TimeSpan.
        /// </summary>
        /// <param name="minutes">The minutes.</param>
        /// <returns>The relevant TimeSpan.</returns>
        /// <example>5.Minutes()</example>
        public static TimeSpan Minutes(this int minutes)
        {
            return new TimeSpan(0, minutes, 0);
        }

        /// <summary>
        /// Turns the supplied int into a TimeSpan.
        /// </summary>
        /// <param name="seconds">The seconds.</param>
        /// <returns>The relevant TimeSpan.</returns>
        /// <example>5.Seconds()</example>
        public static TimeSpan Seconds(this int seconds)
        {
            return new TimeSpan(0, 0, seconds);
        }

        /// <summary>
        /// Turns the supplied int into a TimeSpan.
        /// </summary>
        /// <param name="milliseconds">The milliseconds.</param>
        /// <returns>The relevant TimeSpan.</returns>
        /// <example>5.Milliseconds()</example>
        public static TimeSpan Milliseconds(this int milliseconds)
        {
            return new TimeSpan(0, 0, 0, 0, milliseconds);
        }

        /// <summary>
        /// Turns the supplied double into a TimeSpan.
        /// </summary>
        /// <param name="hours">The hours.</param>
        /// <returns>The relevant TimeSpan.</returns>
        /// <example>5.5.Hours()</example>
        public static TimeSpan Hours(this double hours)
        {
            
            return TimeSpan.FromHours(hours);
        }

        /// <summary>
        /// Turns the supplied double into a TimeSpan.
        /// </summary>
        /// <param name="minutes">The minutes.</param>
        /// <returns>The relevant TimeSpan.</returns>
        /// <example>5.5.Minutes()</example>
        public static TimeSpan Minutes(this double minutes)
        {
            return TimeSpan.FromMinutes(minutes);
        }

        /// <summary>
        /// Turns the supplied double into a TimeSpan.
        /// </summary>
        /// <param name="seconds">The seconds.</param>
        /// <returns>The relevant TimeSpan.</returns>
        /// <example>5.5.Seconds()</example>
        public static TimeSpan Seconds(this double seconds)
        {
            return TimeSpan.FromSeconds(seconds);
        }

        /// <summary>
        /// Turns the supplied double into a TimeSpan.
        /// </summary>
        /// <param name="milliseconds">The milliseconds.</param>
        /// <returns>The relevant TimeSpan.</returns>
        /// <example>5.5.Milliseconds()</example>
        public static TimeSpan Milliseconds(this double milliseconds)
        {
            return TimeSpan.FromMilliseconds(milliseconds);
        }
    }
}
