using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using Moon.Exceptions;
using Moon.Extensions;

namespace Moon.Helpers
{
    /// <summary>
    /// Helper to make sure the passed parameters are correct.
    /// The extra information this class returns is built using the ToString method. Make sure you've implemented it in your classes if necessary.
    /// </summary>
	[DebuggerDisplay("Moon's Guard. Usage of reflection is set currently to {UseReflection}")]
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
		///<exception cref="MoonGuardException">The expression is false.</exception>
		public static void Check<T>(Func<T , bool> func, params T[] arguments)
		{
			if (func == null) return;

			var throwException = false;

			if (arguments == null)
			{
				if (typeof (T).IsClass && !func(default (T)))
				{
					throwException = true;
				}
			}
			else if (arguments.Any(x => !func(x)))
			{
				throwException = true;
			}

			if (throwException)
			{
				var objectArray = arguments == null ? new object[] {null} : arguments.Cast<object>().ToArray();
				throw GuardException(Settings.Moon.Language.NotCompliant, objectArray);
			}
		}

    	/// <summary>
        /// Determines whether the typeToAssign can be assigned to the targetType
        /// </summary>
        /// <param name="typeToAssign">The type to assign.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <exception cref="MoonGuardException">The types can't be assigned.</exception>
        public static void CanBeAssigned(Type typeToAssign, Type targetType)
    	{
			if (typeToAssign == null || targetType == null)
			{
				throw GuardException(Settings.Moon.Language.NullHasBeenPassed);
			}

            if (!typeToAssign.CanBeAssignedTo(targetType))
            {
				string message = string.Format(CultureInfo.CurrentCulture,
                                               targetType.IsInterface
                                                       ? Settings.Moon.Language.TypeCannotBeAssignedInterface
                                                       : Settings.Moon.Language.TypeCannotBeAssigned,
                                               typeToAssign.Name, targetType.Name);

				throw GuardException(message);
            }
        }

        /// <summary>
        /// Checks the arguments to ensure they are parsable to guid
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <exception cref="MoonGuardException">Some of the strings can't be parsed to Guids, null, empty or whitespace.</exception>
        public static IEnumerable<Guid> CanParseToGuid(params string[] arguments)
		{
			if (arguments == null)
			{
				throw GuardException(Settings.Moon.Language.GuidParseError);
			}

			if (arguments.Length == 0)
			{
				return new List<Guid>();
			}

        	IEnumerable<Guid> list;

			if (!arguments.Any(x => x.IsNullOrWhiteSpace()))
        	{
        		try
        		{
        			list = arguments.Select(x => new Guid(x)).ToList();
        		}
        		catch
				{
					throw GuardException(Settings.Moon.Language.GuidParseError, arguments);
        		}

        		if (list.Any(x => x == Guid.Empty))
        		{
        			throw GuardException(Settings.Moon.Language.GuidParseError, arguments); 
        		}
        	}
			else
			{
				throw GuardException(Settings.Moon.Language.GuidParseError, arguments); 
			}

        	return list;
        }

        /// <summary>
        /// Checks the guids to ensure they are not empty.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <exception cref="MoonGuardException">Some of the guids are empty.</exception>
        public static void GuidNotEmpty(params Guid[] arguments)
        {
			if (arguments == null || arguments.Any(x => x == Guid.Empty))
        	{
        		throw GuardException(Settings.Moon.Language.EmptyGuids, arguments.BoxToArray());
        	}
        }

    	/// <summary>
        /// Checks the dates to ensure they are in the future.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <exception cref="MoonGuardException">Some of the dates are in the past.</exception>
        public static void InTheFuture(params DateTime[] arguments)
        {
        	var now = DateTime.Now;

			if (arguments == null || arguments.Any(x => x.CompareTo(now) <= 0))
            {
				throw GuardException(Settings.Moon.Language.NotAllDatesAreInTheFuture, arguments.BoxToArray());
            }
        }

        /// <summary>
        /// Checks the dates to ensure they are in the past.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <exception cref="MoonGuardException">Some of the dates are in the future.</exception>
        public static void InThePast(params DateTime[] arguments)
		{
			var now = DateTime.Now;

			if (arguments == null || arguments.Any(x => x.CompareTo(now) >= 0))
            {
				throw GuardException(Settings.Moon.Language.NotAllDatesAreInThePast, arguments.BoxToArray());
            }
        }

		///<summary>
		/// Checks if all arguments are true.
		///</summary>
		///<param name="arguments">A list of booleans.</param>
		///<exception cref="MoonGuardException">Some of the booleans are false.</exception>
		public static void IsTrue(params bool[] arguments)
		{
			if (arguments == null || arguments.Any(x => !x))
			{
				throw GuardException(Settings.Moon.Language.IsTrue, arguments.BoxToArray());
			}
		}

		///<summary>
		/// Checks if all arguments are false.
		///</summary>
		///<param name="arguments">A list of booleans.</param>
		///<exception cref="MoonGuardException">Some of the booleans are true.</exception>
		public static void IsFalse(params bool[] arguments)
		{
			if (arguments == null || arguments.Any(x => x))
			{
				throw GuardException(Settings.Moon.Language.IsFalse, arguments.BoxToArray());
			}
		}

		/// <summary>
		/// Checks if all types are serializable.
		/// </summary>
		/// <param name="arguments">A list of types.</param>
		///<exception cref="MoonGuardException">Some of the types aren't serializable.</exception>
		public static void IsSerializable(params Type[] arguments)
		{
			if (arguments == null || arguments.Any(x => !x.IsSerializable))
			{
				throw GuardException(Settings.Moon.Language.IsSerializable, arguments);
			}
		}

        /// <summary>
        /// Checks the arguments to ensure they aren't null.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <exception cref="MoonGuardException">Some of the arguments are null.</exception>
        public static void NotNull(params object[] arguments)
        {
			if (arguments == null || arguments.Any(x => x == null))
			{
				throw GuardException(Settings.Moon.Language.NullHasBeenPassed, arguments);
			}
        }

        /// <summary>
        /// Checks the arguments to ensure they aren't null, empty or whitespace.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <exception cref="MoonGuardException">Some of the arguments are null, empty or whitespace.</exception>
        public static void NotNullOrWhiteSpace(params string[] arguments)
        {
			if (arguments == null || arguments.Any(x => x.IsNullOrWhiteSpace()))
            {
				throw GuardException(Settings.Moon.Language.NullEmptyOrWhiteSpaceHasBeenPassed, arguments);
            }
        }

    	/// <summary>
        /// Checks if the start datetime is before the end datetime.
        /// </summary>
		/// <param name="startDate">The start datetime.</param>
		/// <param name="endDate">The end datetime.</param>
        /// <exception cref="MoonGuardException">The start datetime is after or equal to end datetime.</exception>
        public static void StartBeforeEnd(DateTime startDate, DateTime endDate)
        {
            if (!startDate.IsBefore(endDate))
            {
				throw GuardException(Settings.Moon.Language.StartBeforeEndDate, new object[] { startDate, endDate });
            }
        }

		/// <summary>
		/// An error has been found -> Throw an exception.
		/// </summary>
		/// <param name="message">The exception's inner message.</param>
		/// <param name="arguments">An array containing zero or more objects to format.</param>
		private static MoonGuardException GuardException(string message, params object[] arguments)
		{
			var builder = new StringBuilder();

			if (UseReflection)
			{
			    var frame = new StackFrame(2, false);
                MethodBase methodBase = frame.GetMethod();

				builder.Append(message);

				builder.Append(Settings.Moon.Language.SentenceSeparator);
				
                if (methodBase != null)
                {
					builder.Append(Settings.Moon.Language.GuardErrorMessageBase
                                    .FormatWith(methodBase.DeclaringType.FullName,
                                                methodBase.Name));
                }

                if (arguments != null)
                {
					builder.Append(Settings.Moon.Language.GuardErrorMessage);

                    for (int i = 0; i < arguments.Length; i++)
                    {
						builder.Append(Settings.Moon.Language.DatamemberBracketOpen);

						builder.Append(arguments[i] != null ? arguments[i].ToString() : Settings.Moon.Language.NullValue);

						builder.Append(Settings.Moon.Language.DatamemberBracketClose);

                        if (i != arguments.Length - 1)
                        {
							builder.Append(Settings.Moon.Language.ErrorSeparator);
                        }
                    }
                }
                else
                {
					builder.Append(Settings.Moon.Language.NoArguments);
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
		private static MoonGuardException GuardException(string message)
		{
			return new MoonGuardException(message + Settings.Moon.Language.EndOfSentence);
		}
    }
}