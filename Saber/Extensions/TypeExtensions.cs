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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Saber.Extensions
{
	/// <summary>
	/// Extensions for the Types.
	/// </summary>
	public static class TypeExtensions
	{
		/// <summary>
		/// Gets the type attributes of the passed type.
		/// </summary>
		/// <typeparam name="T">The type of attribute to retrieve.</typeparam>
		/// <param name="type">The type to retrieve them from.</param>
		/// <returns>
		/// The type attributes of type T.
		/// </returns>
		public static IEnumerable<T> GetAttributes<T>(this MemberInfo type)
			where T : Attribute
		{
			return type.GetAttributes<T>(true);
		}

		/// <summary>
		/// Gets the type attributes of the passed type.
		/// </summary>
		/// <typeparam name="T">The type of attribute to retrieve.</typeparam>
		/// <param name="type">The type to retrieve them from.</param>
		/// <param name="inherited">if set to <c>true</c> [inherited].</param>
		/// <returns>
		/// The type attributes of type T.
		/// </returns>
		public static IEnumerable<T> GetAttributes<T>(this MemberInfo type, bool inherited)
			where T : Attribute
		{
			return type.GetCustomAttributes(typeof(T), inherited).OfType<T>().AsReadOnly();
		}

        /// <summary>
        /// Determines whether the passed type of a generic subclass of the generic type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="genericType">Type of the generic.</param>
        /// <returns>
        ///   <c>true</c> if the passed type of a generic subclass of the generic type.; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsGenericSubclassOf(this Type type, Type genericType)
        {
            if (type == null)
                return false;

            if (type.IsGenericType && type.GetGenericTypeDefinition() == genericType)
                return true;

            return IsGenericSubclassOf(type.BaseType, genericType);
        }
	}
}