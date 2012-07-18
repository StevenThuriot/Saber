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
	}
}