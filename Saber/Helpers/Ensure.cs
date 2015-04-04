using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Saber.Extensions;

namespace Saber.Helpers
{
	/// <summary>
	/// Helper to make sure the passed parameters are correct.
	/// </summary>
	public static class Ensure
	{
		private static readonly Regex ValidEmailRegex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,6}$");

		/// <summary>
		/// Determines whether the passed arguments are compliant with the given expression.
		/// </summary>
		/// <typeparam name="T">The specific type to check.</typeparam>
		/// <param name="func">The expression to check.</param>
		/// <param name="arguments">The arguments.</param>
		/// <returns>The result of the expression</returns>
		public static bool Check<T>(Func<T, bool> func, params T[] arguments)
		{
			if (func == null) return false;

			var isValid = false;

			if (arguments == null)
			{
				if (typeof(T).IsClass && !func(default(T)))
				{
					isValid = true;
				}
			}
			else if (arguments.Any(x => !func(x)))
			{
				isValid = true;
			}

			return !isValid;
		}

		/// <summary>
		/// Determines whether the typeToAssign can be assigned to the targetType
		/// </summary>
		/// <param name="typeToAssign">The type to assign.</param>
		/// <param name="targetType">Type of the target.</param>
		/// <returns>
		///   <c>true</c> if this instance [can be assigned] to the specified type to assign; otherwise, <c>false</c>.
		/// </returns>
		public static bool CanBeAssigned(Type typeToAssign, Type targetType)
		{
			return typeToAssign != null && targetType != null && typeToAssign.CanBeAssignedTo(targetType);
		}

		/// <summary>
		/// Checks the arguments to ensure they are parsable to guid
		/// </summary>
		/// <param name="arguments">The arguments.</param>
		/// <param name="list">The list.</param>
		/// <returns>
		///   <c>true</c> if this instance [can parse to GUID] the specified arguments; otherwise, <c>false</c>.
		/// </returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		public static bool CanParseToGuid(string[] arguments, out IEnumerable<Guid> list)
		{
			if (arguments == null)
			{
				list = null;
				return false;
			}

			if (arguments.Length == 0)
			{
				list = new List<Guid>();
				return true;
			}


			if (!arguments.Any(x => x.IsNullOrWhiteSpace()))
			{
				try
				{
					list = arguments.Select(x => new Guid(x)).ToList();
				}
				catch (FormatException)
				{
					list = null;
					return false;
				}

				if (list.Any(x => x == Guid.Empty))
				{
					list = null;
					return false;
				}
			}
			else
			{
				list = null;
				return false;
			}

			return true;
		}

		/// <summary>
		/// Checks the guids to ensure they are not empty.
		/// </summary>
		/// <param name="arguments">The arguments.</param>
		/// <returns>True if none of the guids are empty.</returns>
		public static bool GuidNotEmpty(params Guid[] arguments)
		{
			return arguments != null && !arguments.Any(x => x == Guid.Empty);
		}

		/// <summary>
		/// Checks if all passed arguments are positive.
		/// </summary>
		/// <param name="arguments">The arguments.</param>
		/// <returns>True if all arguments are positive.</returns>
		public static bool Positive(params int[] arguments)
		{
			return arguments != null && !arguments.Any(x => x < 0);
		}

		/// <summary>
		/// Checks if all passed arguments are strict positive.
		/// </summary>
		/// <param name="arguments">True if all arguments are strict positive.</param>
		public static bool StrictPositive(params int[] arguments)
		{
			return arguments != null && !arguments.Any(x => x <= 0);
		}

		/// <summary>
		/// Checks if all passed arguments are negative.
		/// </summary>
		/// <param name="arguments">The arguments.</param>
		/// <returns>True if all arguments are negative.</returns>
		public static bool Negative(params int[] arguments)
		{
			return arguments != null && !arguments.Any(x => x > 0);
		}

		/// <summary>
		/// Checks if all passed arguments are strict negative.
		/// </summary>
		/// <param name="arguments">The arguments.</param>
		/// <returns>True if all arguments are strictly negative.</returns>
		public static bool StrictNegative(params int[] arguments)
		{
			return arguments != null && !arguments.Any(x => x >= 0);
		}

		/// <summary>
		/// Checks if all passed arguments are positive.
		/// </summary>
		/// <param name="arguments">The arguments.</param>
		/// <returns>True if all arguments are positive.</returns>
		public static bool Positive(params decimal[] arguments)
		{
			return arguments != null && !arguments.Any(x => x < 0);
		}

		/// <summary>
		/// Checks if all passed arguments are strict positive.
		/// </summary>
		/// <param name="arguments">The arguments.</param>
		/// <returns>True if all arguments are strictly positive.</returns>
		public static bool StrictPositive(params decimal[] arguments)
		{
			return arguments != null && !arguments.Any(x => x <= 0);
		}

		/// <summary>
		/// Checks if all passed arguments are negative.
		/// </summary>
		/// <param name="arguments">The arguments.</param>
		/// <returns></returns>
		public static bool Negative(params decimal[] arguments)
		{
			return arguments != null && !arguments.Any(x => x > 0);
		}

		/// <summary>
		/// Checks if all passed arguments are strict negative.
		/// </summary>
		/// <param name="arguments"></param>
		public static bool StrictNegative(params decimal[] arguments)
		{
			return arguments != null && !arguments.Any(x => x >= 0);
		}

		/// <summary>
		/// Checks if all passed arguments are positive.
		/// </summary>
		/// <param name="arguments">The arguments.</param>
		/// <returns>True if all arguments are positive.</returns>
		public static bool Positive(params double[] arguments)
		{
			return arguments != null && !arguments.Any(x => x < 0);
		}

		/// <summary>
		/// Checks if all passed arguments are strict positive.
		/// </summary>
		/// <param name="arguments">The arguments.</param>
		/// <returns>True if all arguments are strictly positive.</returns>
		public static bool StrictPositive(params double[] arguments)
		{
			return arguments != null && !arguments.Any(x => x <= 0);
		}

		/// <summary>
		/// Checks if all passed arguments are negative.
		/// </summary>
		/// <param name="arguments">The arguments.</param>
		/// <returns>True if all arguments are strictly negative.</returns>
		public static bool Negative(params double[] arguments)
		{
			return arguments != null && !arguments.Any(x => x > 0);
		}

		/// <summary>
		/// Checks if all passed arguments are strict negative.
		/// </summary>
		/// <param name="arguments">The arguments.</param>
		/// <returns>True if all arguments are strictly negative.</returns>
		public static bool StrictNegative(params double[] arguments)
		{
			return arguments != null && !arguments.Any(x => x >= 0);
		}

		/// <summary>
		/// Checks the dates to ensure they are in the future.
		/// </summary>
		/// <param name="arguments">The arguments.</param>
		/// <returns>True if all arguments are in the future.</returns>
		public static bool InTheFuture(params DateTime[] arguments)
		{
			var now = DateTime.Now;
			return arguments != null && !arguments.Any(x => x.CompareTo(now) <= 0);
		}

		/// <summary>
		/// Checks the dates to ensure they are in the past.
		/// </summary>
		/// <param name="arguments">The arguments.</param>
		/// <returns>True if all arguments are in the past.</returns>
		public static bool InThePast(params DateTime[] arguments)
		{
			var now = DateTime.Now;
			return arguments != null && !arguments.Any(x => x.CompareTo(now) >= 0);
		}

		/// <summary>
		/// Checks if all arguments are true.
		/// </summary>
		/// <param name="arguments">A list of booleans.</param>
		/// <returns>True if all arguments are true.</returns>
		public static bool True(params bool[] arguments)
		{
			return arguments != null && arguments.All(x => x);
		}

		/// <summary>
		/// Checks if all arguments are false.
		/// </summary>
		/// <param name="arguments">A list of booleans.</param>
		/// <returns>True is all arguments are false.</returns>
		public static bool False(params bool[] arguments)
		{
			return arguments != null && !arguments.Any(x => x);
		}

		/// <summary>
		/// Checks if the arguments are valid email addresses.
		/// </summary>
		/// <param name="arguments">True if all arguments are valid.</param>
		public static bool ValidEmail(params string[] arguments)
		{
			return arguments != null && arguments.All(x => ValidEmailRegex.IsMatch(x));
		}

		/// <summary>
		/// Checks if all types are serializable.
		/// </summary>
		/// <param name="arguments">A list of types.</param>
		/// <returns>True if all arguments are serializable.</returns>
		public static bool Serializable(params Type[] arguments)
		{
			return arguments != null && arguments.All(x => x.IsSerializable);
		}

		/// <summary>
		/// Checks the arguments to ensure they aren't null.
		/// </summary>
		/// <param name="arguments">The arguments.</param>
		/// <returns>True if all arguments differ from null.</returns>
		public static bool NotNull(params object[] arguments)
		{
			return arguments != null && !arguments.Any(x => x == null);
		}

		/// <summary>
		/// Checks the arguments to ensure they aren't null, empty or whitespace.
		/// </summary>
		/// <param name="arguments">The arguments.</param>
		/// <returns>True if all arguments differ from null, empty or whitespace.</returns>
		public static bool NotNullOrWhiteSpace(params string[] arguments)
		{
			return arguments != null && !arguments.Any(x => x.IsNullOrWhiteSpace());
		}

		/// <summary>
		/// Checks if the start datetime is before the end datetime.
		/// </summary>
		/// <param name="startDate">The start datetime.</param>
		/// <param name="endDate">The end datetime.</param>
		/// <returns>True if the start datetime is before the end datetime.</returns>
		public static bool StartBeforeEnd(DateTime startDate, DateTime endDate)
		{
			return startDate.IsBefore(endDate);
		}
	}
}
