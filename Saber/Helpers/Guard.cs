using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using Saber.Exceptions;
using Saber.Extensions;

namespace Saber.Helpers
{
	/// <summary>
	/// Helper to make sure the passed parameters are correct.
	/// The extra information this class returns is built using the ToString method. Make sure you've implemented it in your classes if necessary.
	/// </summary>
	[DebuggerDisplay("Saber's Guard. Usage of reflection is set currently to {UseReflection}")]
	public static class Guard
	{
		private static bool _UseReflection = true;

		///<summary>
		/// When set to false, the extra information gathering about the calling method (using reflection) will be turned off.
		/// Default value is true
		///</summary>
		public static bool UseReflection
		{
			get { return _UseReflection; }
			set { _UseReflection = value; }
		}

		///<summary>
		/// Determines whether the passed arguments are compliant with the given expression.
		///</summary>
		///<param name="func">The expression to check.</param>
		///<param name="arguments">The arguments.</param>
		///<typeparam name="T">The specific type to check.</typeparam>
		///<exception cref="SaberGuardException">The expression is false.</exception>
		public static void Check<T>(Func<T, bool> func, params T[] arguments)
		{
			if (!Ensure.Check(func, arguments))
			{
				var objectArray = arguments == null ? new object[] {null} : arguments.Cast<object>().ToArray();
				throw GuardException(Settings.Saber.Language.NotCompliant, objectArray);
			}
		}

		/// <summary>
		/// Determines whether the typeToAssign can be assigned to the targetType
		/// </summary>
		/// <param name="typeToAssign">The type to assign.</param>
		/// <param name="targetType">Type of the target.</param>
		/// <exception cref="SaberGuardException">The types can't be assigned.</exception>
		public static void CanBeAssigned(Type typeToAssign, Type targetType)
		{
			if (typeToAssign == null || targetType == null)
			{
				throw GuardException(Settings.Saber.Language.NullHasBeenPassed);
			}

			if (!typeToAssign.CanBeAssignedTo(targetType))
			{
				string message = string.Format(CultureInfo.CurrentCulture,
				                               targetType.IsInterface
				                               	? Settings.Saber.Language.TypeCannotBeAssignedInterface
				                               	: Settings.Saber.Language.TypeCannotBeAssigned,
				                               typeToAssign.Name, targetType.Name);

				throw GuardException(message);
			}
		}

		/// <summary>
		/// Checks the arguments to ensure they are parsable to guid
		/// </summary>
		/// <param name="arguments">The arguments.</param>
		/// <exception cref="SaberGuardException">Some of the strings can't be parsed to Guids, null, empty or whitespace.</exception>
		public static IEnumerable<Guid> CanParseToGuid(params string[] arguments)
		{
			IEnumerable<Guid> guidList;
			if (Ensure.CanParseToGuid(arguments, out guidList))
			{
				return guidList;
			}

			throw GuardException(Settings.Saber.Language.GuidParseError, arguments);
		}

		/// <summary>
		/// Checks the guids to ensure they are not empty.
		/// </summary>
		/// <param name="arguments">The arguments.</param>
		/// <exception cref="SaberGuardException">Some of the guids are empty.</exception>
		public static void GuidNotEmpty(params Guid[] arguments)
		{
			if (!Ensure.GuidNotEmpty(arguments))
			{
				throw GuardException(Settings.Saber.Language.EmptyGuids, arguments.BoxToArray());
			}
		}

		/// <summary>
		/// Checks if all passed arguments are positive.
		/// </summary>
		/// <param name="arguments"></param>
		public static void Positive(params int[] arguments)
		{
			if (!Ensure.Positive(arguments))
			{
				throw GuardException(Settings.Saber.Language.NegativeNumbers, arguments.BoxToArray());
			}
		}

		/// <summary>
		/// Checks if all passed arguments are strict positive.
		/// </summary>
		/// <param name="arguments"></param>
		public static void StrictPositive(params int[] arguments)
		{
			if (!Ensure.StrictPositive(arguments))
			{
				throw GuardException(Settings.Saber.Language.NegativeNumbersOrZero, arguments.BoxToArray());
			}
		}

		/// <summary>
		/// Checks if all passed arguments are negative.
		/// </summary>
		/// <param name="arguments"></param>
		public static void Negative(params int[] arguments)
		{
			if (!Ensure.Negative(arguments))
			{
				throw GuardException(Settings.Saber.Language.PositiveNumbers, arguments.BoxToArray());
			}
		}

		/// <summary>
		/// Checks if all passed arguments are strict negative.
		/// </summary>
		/// <param name="arguments"></param>
		public static void StrictNegative(params int[] arguments)
		{
			if (!Ensure.StrictNegative(arguments))
			{
				throw GuardException(Settings.Saber.Language.PostiveNumbersOrZero, arguments.BoxToArray());
			}
		}

		/// <summary>
		/// Checks if all passed arguments are positive.
		/// </summary>
		/// <param name="arguments"></param>
		public static void Positive(params decimal[] arguments)
		{
			if (!Ensure.Positive(arguments))
			{
				throw GuardException(Settings.Saber.Language.NegativeNumbers, arguments.BoxToArray());
			}
		}

		/// <summary>
		/// Checks if all passed arguments are strict positive.
		/// </summary>
		/// <param name="arguments"></param>
		public static void StrictPositive(params decimal[] arguments)
		{
			if (!Ensure.StrictPositive(arguments))
			{
				throw GuardException(Settings.Saber.Language.NegativeNumbersOrZero, arguments.BoxToArray());
			}
		}

		/// <summary>
		/// Checks if all passed arguments are negative.
		/// </summary>
		/// <param name="arguments"></param>
		public static void Negative(params decimal[] arguments)
		{
			if (!Ensure.Negative(arguments))
			{
				throw GuardException(Settings.Saber.Language.PositiveNumbers, arguments.BoxToArray());
			}
		}

		/// <summary>
		/// Checks if all passed arguments are strict negative.
		/// </summary>
		/// <param name="arguments"></param>
		public static void StrictNegative(params decimal[] arguments)
		{
			if (!Ensure.StrictNegative(arguments))
			{
				throw GuardException(Settings.Saber.Language.PostiveNumbersOrZero, arguments.BoxToArray());
			}
		}

		/// <summary>
		/// Checks if all passed arguments are positive.
		/// </summary>
		/// <param name="arguments"></param>
		public static void Positive(params double[] arguments)
		{
			if (!Ensure.Positive(arguments))
			{
				throw GuardException(Settings.Saber.Language.NegativeNumbers, arguments.BoxToArray());
			}
		}

		/// <summary>
		/// Checks if all passed arguments are strict positive.
		/// </summary>
		/// <param name="arguments"></param>
		public static void StrictPositive(params double[] arguments)
		{
			if (!Ensure.StrictPositive(arguments))
			{
				throw GuardException(Settings.Saber.Language.NegativeNumbersOrZero, arguments.BoxToArray());
			}
		}

		/// <summary>
		/// Checks if all passed arguments are negative.
		/// </summary>
		/// <param name="arguments"></param>
		public static void Negative(params double[] arguments)
		{
			if (!Ensure.Negative(arguments))
			{
				throw GuardException(Settings.Saber.Language.PositiveNumbers, arguments.BoxToArray());
			}
		}

		/// <summary>
		/// Checks if all passed arguments are strict negative.
		/// </summary>
		/// <param name="arguments"></param>
		public static void StrictNegative(params double[] arguments)
		{
			if (!Ensure.StrictNegative(arguments))
			{
				throw GuardException(Settings.Saber.Language.PostiveNumbersOrZero, arguments.BoxToArray());
			}
		}

		/// <summary>
		/// Checks the dates to ensure they are in the future.
		/// </summary>
		/// <param name="arguments">The arguments.</param>
		/// <exception cref="SaberGuardException">Some of the dates are in the past.</exception>
		public static void InTheFuture(params DateTime[] arguments)
		{
			if (!Ensure.InTheFuture(arguments))
			{
				throw GuardException(Settings.Saber.Language.NotAllDatesAreInTheFuture, arguments.BoxToArray());
			}
		}

		/// <summary>
		/// Checks the dates to ensure they are in the past.
		/// </summary>
		/// <param name="arguments">The arguments.</param>
		/// <exception cref="SaberGuardException">Some of the dates are in the future.</exception>
		public static void InThePast(params DateTime[] arguments)
		{
			if (!Ensure.InThePast(arguments))
			{
				throw GuardException(Settings.Saber.Language.NotAllDatesAreInThePast, arguments.BoxToArray());
			}
		}

		///<summary>
		/// Checks if all arguments are true.
		///</summary>
		///<param name="arguments">A list of booleans.</param>
		///<exception cref="SaberGuardException">Some of the booleans are false.</exception>
		public static void True(params bool[] arguments)
		{
			if (!Ensure.True(arguments))
			{
				throw GuardException(Settings.Saber.Language.IsTrue, arguments.BoxToArray());
			}
		}

		///<summary>
		/// Checks if all arguments are false.
		///</summary>
		///<param name="arguments">A list of booleans.</param>
		///<exception cref="SaberGuardException">Some of the booleans are true.</exception>
		public static void False(params bool[] arguments)
		{
			if (!Ensure.False(arguments))
			{
				throw GuardException(Settings.Saber.Language.IsFalse, arguments.BoxToArray());
			}
		}

		/// <summary>
		/// Checks if the arguments are valid email addresses.
		/// </summary>
		/// <param name="arguments"></param>
		public static void ValidEmail(params string[] arguments)
		{
			if (!Ensure.ValidEmail(arguments))
			{
				throw GuardException(Settings.Saber.Language.InvalidEmail, arguments);
			}
		}

		/// <summary>
		/// Checks if all types are serializable.
		/// </summary>
		/// <param name="arguments">A list of types.</param>
		///<exception cref="SaberGuardException">Some of the types aren't serializable.</exception>
		public static void Serializable(params Type[] arguments)
		{
			if (!Ensure.Serializable(arguments))
			{
				throw GuardException(Settings.Saber.Language.IsSerializable, arguments);
			}
		}

		/// <summary>
		/// Checks the arguments to ensure they aren't null.
		/// </summary>
		/// <param name="arguments">The arguments.</param>
		/// <exception cref="SaberGuardException">Some of the arguments are null.</exception>
		public static void NotNull(params object[] arguments)
		{
			if (!Ensure.NotNull(arguments))
			{
				throw GuardException(Settings.Saber.Language.NullHasBeenPassed, arguments);
			}
		}

		/// <summary>
		/// Checks the arguments to ensure they aren't null, empty or whitespace.
		/// </summary>
		/// <param name="arguments">The arguments.</param>
		/// <exception cref="SaberGuardException">Some of the arguments are null, empty or whitespace.</exception>
		public static void NotNullOrWhiteSpace(params string[] arguments)
		{
			if (!Ensure.NotNullOrWhiteSpace(arguments))
			{
				throw GuardException(Settings.Saber.Language.NullEmptyOrWhiteSpaceHasBeenPassed, arguments);
			}
		}

		/// <summary>
		/// Checks if the start datetime is before the end datetime.
		/// </summary>
		/// <param name="startDate">The start datetime.</param>
		/// <param name="endDate">The end datetime.</param>
		/// <exception cref="SaberGuardException">The start datetime is after or equal to end datetime.</exception>
		public static void StartBeforeEnd(DateTime startDate, DateTime endDate)
		{
			if (!Ensure.StartBeforeEnd(startDate,endDate))
			{
				throw GuardException(Settings.Saber.Language.StartBeforeEndDate, new object[] {startDate, endDate});
			}
		}

		/// <summary>
		/// An error has been found -> Throw an exception.
		/// </summary>
		/// <param name="message">The exception's inner message.</param>
		/// <param name="arguments">An array containing zero or more objects to format.</param>
		private static SaberGuardException GuardException(string message, params object[] arguments)
		{
			var builder = new StringBuilder();

			if (UseReflection)
			{
				var frame = new StackFrame(2, false);
				MethodBase methodBase = frame.GetMethod();

				builder.Append(message);

				builder.Append(Settings.Saber.Language.SentenceSeparator);

				if (methodBase != null)
				{
					builder.Append(Settings.Saber.Language.GuardErrorMessageBase
					               	.FormatWith(methodBase.DeclaringType.FullName,
					               	            methodBase.Name));
				}

				if (arguments != null)
				{
					builder.Append(Settings.Saber.Language.GuardErrorMessage);

					for (int i = 0; i < arguments.Length; i++)
					{
						builder.Append(Settings.Saber.Language.DatamemberBracketOpen);

						builder.Append(arguments[i] != null ? arguments[i].ToString() : Settings.Saber.Language.NullValue);

						builder.Append(Settings.Saber.Language.DatamemberBracketClose);

						if (i != arguments.Length - 1)
						{
							builder.Append(Settings.Saber.Language.ErrorSeparator);
						}
					}
				}
				else
				{
					builder.Append(Settings.Saber.Language.NoArguments);
				}
			}
			else
			{
				builder.Append(message);
			}

			return GuardException(builder.ToString());
		}

		/// <summary>
		/// An error has been found -> Throw an exception.
		/// </summary>
		/// <param name="message">The exception's inner message.</param>
		private static SaberGuardException GuardException(string message)
		{
			return new SaberGuardException(message + Settings.Saber.Language.EndOfSentence);
		}
	}
}