using System;
using System.Collections;
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
	        while (true)
	        {
	            if (type == null)
	                return false;

	            if (type.IsGenericType && type.GetGenericTypeDefinition() == genericType)
	                return true;

	            type = type.BaseType;
	        }
	    }

	    /// <summary>
        /// Returns the full readable name for a type.
        /// </summary>
        /// <remarks>Strips out the ` tags in a generic type and turns it readable.</remarks>
        /// <param name="type">The type to get the fullname of.</param>
        /// <returns>A string representation for the passed type.</returns>
        public static string GetFullName(this Type type)
        {
            if (type == null)
                return string.Empty;

            if (!type.IsGenericType) 
                return type.FullName;



            var fullName = type.GetGenericTypeDefinition().FullName;
            var baseType = fullName.Substring(0, fullName.IndexOf('`'));
            var generics = type.GetGenericArguments().Select(GetFullName).Aggregate((x, y) => x + ", " + y);

            return string.Format("{0}<{1}>", baseType, generics);
        }

	    /// <summary>
	    /// Returns the full readable name for a type.
	    /// </summary>
	    /// <remarks>Strips out the ` tags in a generic type and turns it readable.</remarks>
	    /// <param name="type">The type to get the fullname of.</param>
	    /// <param name="generics">Instead of the actual generics, it uses the (correct amount) of passed generics instead.</param>
	    /// <returns>A string representation for the passed type.</returns>
	    public static string GetFullName(this Type type, params string[] generics)
        {
            if (type.IsGenericType)
            {
                var fullName = type.GetGenericTypeDefinition().FullName;
                var baseType = fullName.Substring(0, fullName.IndexOf('`'));
                var genericCount = type.GetGenericArguments().Length;
                return string.Format("{0}<{1}>", baseType, string.Join(", ", generics.Take(genericCount).ToArray()));
            }

            return type.FullName;
        }

        /// <summary>
        /// Checks if a given type is an enumerable type.
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns>True if enumerable.</returns>
        public static bool IsEnumerable(this Type type)
        {
            var enumerableType = typeof(IEnumerable);
            if (type == enumerableType) return true;

            var enumerableInterface = type.GetInterface(enumerableType.FullName);
            return enumerableInterface != null;
        }

	    /// <summary>
	    /// Checks if a given type is a generic enumerable type.
	    /// </summary>
	    /// <param name="type">The type</param>
	    /// <param name="enumerableOf">The generic.</param>
	    /// <returns>True if enumerable.</returns>
	    public static bool IsEnumerable(this Type type, out Type enumerableOf)
        {
            var enumerableType = typeof(IEnumerable<>);

            if (type.IsGenericType && type.IsInterface)
            {
                if (type.GetGenericTypeDefinition() == enumerableType)
                {
                    enumerableOf = type.GetGenericArguments().First();
                    return true;
                }
            }

            var enumerableInterface = type.GetInterface(enumerableType.FullName);

            if (enumerableInterface != null)
            {
                enumerableOf = enumerableInterface.GetGenericArguments().First();
                return true;
            }

            enumerableOf = null;
            return false;
        }

        /// <summary>
        /// Unwraps the element type from a generic IEnumerable type.
        /// </summary>
        /// <param name="type">The type to unwrap</param>
        /// <returns>The generic type</returns>
        /// <remarks>
        /// - A string will return as string rather than IEnumerable of char.
        /// - If the type is not IEnumerable, the original type will be returned (hence 'if possible')
        /// </remarks>
        public static Type UnwrapEnumerableIfPossible(this Type type)
        {
            if (type == typeof(string))
                return type;

            var enumerable = typeof(IEnumerable<>);

            if (type.IsGenericType && type.GetGenericTypeDefinition() == enumerable)
                return type.GetGenericArguments().First();

            var enumerableInterface = type.GetInterface(enumerable.Name);
            if (enumerableInterface == null)
                return type;

            return enumerableInterface.GetGenericArguments().First();
        }
	}
}