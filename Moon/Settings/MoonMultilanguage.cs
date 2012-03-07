using System.Diagnostics;

namespace Moon.Settings
{
	///<summary>
	/// Standard language class for the Moon Framework.
	/// This language class makes use of resource files (resx).
	///</summary>
	[DebuggerDisplay("Moon's Default Multilanguage Class.")]
	internal class MoonMultilanguage : IMoonMultilanguage
	{
		///<summary>
		/// Looks up a string similar to "]".
		///</summary>
		public string DatamemberBracketClose
		{
			get { return MoonRes.DatamemberBracketClose; }
		}

		/// <summary>
		///   Looks up a string similar to "[".
		/// </summary>
		public string DatamemberBracketOpen
		{
			get { return MoonRes.DatamemberBracketOpen; }
		}

		/// <summary>
		///   Looks up a string similar to " = ".
		/// </summary>
		public string DatamemberEquals
		{
			get { return MoonRes.DatamemberEquals; }
		}

		/// <summary>
		///   Looks up a string similar to "Some of the passed Guids are empty".
		/// </summary>
		public string EmptyGuids
		{
			get { return MoonRes.EmptyGuids; }
		}

		/// <summary>
		///   Looks up a string similar to ".".
		/// </summary>
		public string EndOfSentence
		{
			get { return MoonRes.EndOfSentence; }
		}

		/// <summary>
		///   Looks up a string similar to ", ".
		/// </summary>
		public string ErrorSeparator
		{
			get { return MoonRes.ErrorSeparator; }
		}

		/// <summary>
		///   Looks up a localized string similar to "Exception of type {0} should have been thrown".
		/// </summary>
		public string ExceptionNotThrown
		{
			get { return MoonRes.ExceptionNotThrown; }
		}

		/// <summary>
		///   Looks up a string similar to "Class: {0} - Method: {1} - {2}".
		/// </summary>
		public string GuardErrorMessageBase
		{
			get { return MoonRes.GuardErrorMessageBase; }
		}

		/// <summary>
		///   Looks up a string similar to "Args: {0}".
		/// </summary>
		public string GuardErrorMessage
		{
			get { return MoonRes.GuardErrorMessage; }
		}

		/// <summary>
		///   Looks up a string similar to "Not all arguments can not be converted to Guids".
		/// </summary>
		public string GuidParseError
		{
			get { return MoonRes.GuidParseError; }
		}

		/// <summary>
		///   Looks up a localized string similar to An invalid email has been passed..
		/// </summary>
		public string InvalidEmail
		{
			get { return MoonRes.InvalidEmail; }
		}

		/// <summary>
		///   Looks up a string similar to "Not all booleans are false".
		/// </summary>
		public string IsFalse
		{
			get { return MoonRes.IsFalse; }
		}

		/// <summary>
		///   Looks up a string similar to "Not all booleans are true".
		/// </summary>
		public string IsTrue
		{
			get { return MoonRes.IsTrue; }
		}

		/// <summary>
		///   Looks up a localized string similar to "Negative numbers have been passed.".
		/// </summary>
		public string NegativeNumbers
		{
			get { return MoonRes.NegativeNumbers; }
		}

		/// <summary>
		///   Looks up a localized string similar to "Negative numbers or zero have been passed.".
		/// </summary>
		public string NegativeNumbersOrZero
		{
			get { return MoonRes.NegativeNumbersOrZero; }
		}

		/// <summary>
		///   Looks up a localized string similar to "A type was passed that wasn't serializable".
		/// </summary>
		public string IsSerializable
		{
			get { return MoonRes.IsSerializable; }
		}

		/// <summary>
		///   Looks up a string similar to "No arguments".
		/// </summary>
		public string NoArguments
		{
			get { return MoonRes.NoArguments; }
		}

		/// <summary>
		///   Looks up a string similar to "Not all passed dates are in the future".
		/// </summary>
		public string NotAllDatesAreInTheFuture
		{
			get { return MoonRes.NotAllDatesAreInTheFuture; }
		}

		/// <summary>
		///   Looks up a string similar to "Not all passed dates are in the past".
		/// </summary>
		public string NotAllDatesAreInThePast
		{
			get { return MoonRes.NotAllDatesAreInThePast; }
		}

		///<summary>
		/// Looks up a string similar to "Some of the passed arguments aren't compliant with the given expression".
		///</summary>
		public string NotCompliant
		{
			get { return MoonRes.NotCompliant; }
		}

		/// <summary>
		///   Looks up a string similar to "NULL, empty or whitespace has been passed".
		/// </summary>
		public string NullEmptyOrWhiteSpaceHasBeenPassed
		{
			get { return MoonRes.NullEmptyOrWhiteSpaceHasBeenPassed; }
		}

		/// <summary>
		///   Looks up a string similar to "NULL has been passed".
		/// </summary>
		public string NullHasBeenPassed
		{
			get { return MoonRes.NullHasBeenPassed; }
		}

		/// <summary>
		///   Looks up a string similar to "NULL".
		/// </summary>
		public string NullValue
		{
			get { return MoonRes.NullValue; }
		}

		/// <summary>
		///   Looks up a localized string similar to "Positive numbers have been passed.".
		/// </summary>
		public string PositiveNumbers
		{
			get { return MoonRes.PositiveNumbers; }
		}

		/// <summary>
		///   Looks up a localized string similar to Positive numbers or zero have been passed..
		/// </summary>
		public string PostiveNumbersOrZero
		{
			get { return MoonRes.PostiveNumbersOrZero; }
		}

		/// <summary>
		///   Looks up a string similar to ": ".
		/// </summary>
		public string SentenceSeparator
		{
			get { return MoonRes.SentenceSeparator; }
		}

		/// <summary>
		///   Looks up a string similar to "The start datetime is after or equal to end datetime".
		/// </summary>
		public string StartBeforeEndDate
		{
			get { return MoonRes.StartBeforeEndDate; }
		}

		/// <summary>
		///   Looks up a string similar to "Type {0} can not be assigned to {1}".
		/// </summary>
		public string TypeCannotBeAssigned
		{
			get { return MoonRes.TypeCannotBeAssigned; }
		}

		/// <summary>
		///   Looks up a string similar to "Type {0} can not be assigned to {1}: interface is not implemented".
		/// </summary>
		public string TypeCannotBeAssignedInterface
		{
			get { return MoonRes.TypeCannotBeAssignedInterface; }
		}

		/// <summary>
		///   Looks up a string similar to "An exception of the wrong type was thrown.\n\nExpected: {0}\nActual: {1}".
		/// </summary>
		public string WrongException
		{
			get { return MoonRes.WrongException; }
		}

		/// <summary>
		///   Looks up a string similar to "The exception messages are not equal.\n\nExpected: {0}\nActual: {1}".
		/// </summary>
		public string WrongMessage
		{
			get { return MoonRes.WrongMessage; }
		}
	}
}
