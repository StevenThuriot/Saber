using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Saber.Extensions
{
	/// <summary>
	/// Extensions for the Assembly Class.
	/// </summary>
	public static class AssemblyExtensions
	{
		/// <summary>
		/// Gets the assembly attributes of the passed type.
		/// </summary>
		/// <typeparam name="T">The type of attribute to retrieve.</typeparam>
		/// <param name="assembly">The assembly to retrieve them from.</param>
		/// <returns>The assembly attributes of type T.</returns>
		public static IEnumerable<T> GetAttributes<T>(this Assembly assembly)
			where T : Attribute
		{
			return assembly.GetAttributes<T>(true);
		}

		/// <summary>
		/// Gets the assembly attributes of the passed type.
		/// </summary>
		/// <typeparam name="T">The type of attribute to retrieve.</typeparam>
		/// <param name="assembly">The assembly to retrieve them from.</param>
		/// <param name="inherited">if set to <c>true</c> [inherited].</param>
		/// <returns>
		/// The assembly attributes of type T.
		/// </returns>
		public static IEnumerable<T> GetAttributes<T>(this Assembly assembly, bool inherited)
			where T : Attribute
		{
			return assembly.GetCustomAttributes(typeof (T), inherited).OfType<T>().AsReadOnly();
		}
	}
}