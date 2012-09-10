#region License
// 
//  Copyright 2012 Steven Thuriot
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
using System.Linq;
using System.Linq.Expressions;
using System.Diagnostics.CodeAnalysis;

namespace Saber.Extensions
{
    /// <summary>
    /// General extension methods
    /// </summary>
    public static class GeneralExtensions
	{
        /// <summary>
        /// Checks if the specified target is between start and end.
        /// </summary>
        /// <typeparam name="T">The type of IComparable</typeparam>
        /// <param name="target">The target.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns>True if inbetween, false if not.</returns>
        public static bool IsBetween<T>(this T target, T start, T end)
                where T : IComparable<T>
        {
            if (start.CompareTo(end) == 1)
                return (target.CompareTo(end) >= 0) && (target.CompareTo(start) <= 0);

            return (target.CompareTo(start) >= 0) && (target.CompareTo(end) <= 0);
        }


		/// <summary>
		/// Determines whether the typeToAssign can be assigned to the targetType
		/// </summary>
		/// <param name="typeToAssign">The type to assign.</param>
		/// <param name="targetType">Type of the target.</param>
		/// <exception cref="ArgumentException">The types can't be assigned.</exception>
		/// <returns>True if it can be assigned, false if not.</returns>
        public static bool CanBeAssignedTo(this Type typeToAssign, Type targetType)
        {
            return typeToAssign != null && targetType != null &&
                   targetType.IsAssignableFrom(typeToAssign);
        }

		/// <summary>
		/// Tries to cast a given value to the generic type. 
		/// </summary>
		/// <typeparam name="TFrom">The original type.</typeparam>
		/// <typeparam name="TTo">The type to cast to.</typeparam>
		/// <param name="value">The value to cast.</param>
		/// <param name="castedValue">The out parameter which will contain the casted value.</param>
		/// <returns>Returns true if succeeded.</returns>
		public static bool TryToCast<TFrom, TTo>(this TFrom value, out TTo castedValue) where TTo : class
		{
			castedValue = value as TTo;
			return castedValue != null;
		}

		/// <summary>
		/// Checks if the source is equal to all given values.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="values">The values.</param>
		public static bool EqualToAll(this object source, params object[] values)
		{
			return values.All(x => Equals(x, source));
		}

		/// <summary>
		/// Checks if the source is different from all given values.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="values">The values.</param>
		public static bool NotEqualToAll(this object source, params object[] values)
		{
			return !source.EqualToAny(values);
		}

		/// <summary>
		/// Checks if the source is equal to any of the given values.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="values">The values.</param>
		public static bool EqualToAny(this object source, params object[] values)
		{
			return values.Any(x => Equals(x, source));
		}

		/// <summary>
		/// Checks if the source is different from any of the given values.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="values">The values.</param>
		public static bool NotEqualToAny(this object source, params object[] values)
		{
			return !source.EqualToAll(values);
		}
    }
}