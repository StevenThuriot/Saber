using System.Diagnostics;

namespace Saber.Settings
{
	///<summary>
	/// Standard language class for the Saber Framework.
	/// This language class makes use of resource files (resx).
	///</summary>
	[DebuggerDisplay("Saber's Default Multilanguage Class.")]
	internal class SaberMultilanguage : ISaberMultilanguage
	{
		///<summary>
		/// Looks up a string similar to "]".
		///</summary>
		public string DatamemberBracketClose
		{
			get { return SaberRes.DatamemberBracketClose; }
		}

		/// <summary>
		///   Looks up a string similar to "[".
		/// </summary>
		public string DatamemberBracketOpen
		{
			get { return SaberRes.DatamemberBracketOpen; }
		}

		/// <summary>
		///   Looks up a string similar to " = ".
		/// </summary>
		public string DatamemberEquals
		{
			get { return SaberRes.DatamemberEquals; }
		}

		/// <summary>
		///   Looks up a string similar to "Some of the passed Guids are empty".
		/// </summary>
		public string EmptyGuids
		{
			get { return SaberRes.EmptyGuids; }
		}

		/// <summary>
		///   Looks up a string similar to ".".
		/// </summary>
		public string EndOfSentence
		{
			get { return SaberRes.EndOfSentence; }
		}

		/// <summary>
		///   Looks up a string similar to ", ".
		/// </summary>
		public string ErrorSeparator
		{
			get { return SaberRes.ErrorSeparator; }
		}

		/// <summary>
		///   Looks up a localized string similar to "Exception of type {0} should have been thrown".
		/// </summary>
		public string ExceptionNotThrown
		{
			get { return SaberRes.ExceptionNotThrown; }
		}

		/// <summary>
		///   Looks up a string similar to "Class: {0} - Method: {1} - {2}".
		/// </summary>
		public string GuardErrorMessageBase
		{
			get { return SaberRes.GuardErrorMessageBase; }
		}

		/// <summary>
		///   Looks up a string similar to "Args: {0}".
		/// </summary>
		public string GuardErrorMessage
		{
			get { return SaberRes.GuardErrorMessage; }
		}

		/// <summary>
		///   Looks up a string similar to "Not all arguments can not be converted to Guids".
		/// </summary>
		public string GuidParseError
		{
			get { return SaberRes.GuidParseError; }
		}

		/// <summary>
		///   Looks up a localized string similar to An invalid email has been passed..
		/// </summary>
		public string InvalidEmail
		{
			get { return SaberRes.InvalidEmail; }
		}

		/// <summary>
		///   Looks up a string similar to "Not all booleans are false".
		/// </summary>
		public string IsFalse
		{
			get { return SaberRes.IsFalse; }
		}

		/// <summary>
		///   Looks up a string similar to "Not all booleans are true".
		/// </summary>
		public string IsTrue
		{
			get { return SaberRes.IsTrue; }
		}

		/// <summary>
		///   Looks up a localized string similar to "Negative numbers have been passed.".
		/// </summary>
		public string NegativeNumbers
		{
			get { return SaberRes.NegativeNumbers; }
		}

		/// <summary>
		///   Looks up a localized string similar to "Negative numbers or zero have been passed.".
		/// </summary>
		public string NegativeNumbersOrZero
		{
			get { return SaberRes.NegativeNumbersOrZero; }
		}

		/// <summary>
		///   Looks up a localized string similar to "A type was passed that wasn't serializable".
		/// </summary>
		public string IsSerializable
		{
			get { return SaberRes.IsSerializable; }
		}

		/// <summary>
		///   Looks up a string similar to "No arguments".
		/// </summary>
		public string NoArguments
		{
			get { return SaberRes.NoArguments; }
		}

		/// <summary>
		///   Looks up a string similar to "Not all passed dates are in the future".
		/// </summary>
		public string NotAllDatesAreInTheFuture
		{
			get { return SaberRes.NotAllDatesAreInTheFuture; }
		}

		/// <summary>
		///   Looks up a string similar to "Not all passed dates are in the past".
		/// </summary>
		public string NotAllDatesAreInThePast
		{
			get { return SaberRes.NotAllDatesAreInThePast; }
		}

		///<summary>
		/// Looks up a string similar to "Some of the passed arguments aren't compliant with the given expression".
		///</summary>
		public string NotCompliant
		{
			get { return SaberRes.NotCompliant; }
		}

		/// <summary>
		///   Looks up a string similar to "NULL, empty or whitespace has been passed".
		/// </summary>
		public string NullEmptyOrWhiteSpaceHasBeenPassed
		{
			get { return SaberRes.NullEmptyOrWhiteSpaceHasBeenPassed; }
		}

		/// <summary>
		///   Looks up a string similar to "NULL has been passed".
		/// </summary>
		public string NullHasBeenPassed
		{
			get { return SaberRes.NullHasBeenPassed; }
		}

		/// <summary>
		///   Looks up a string similar to "NULL".
		/// </summary>
		public string NullValue
		{
			get { return SaberRes.NullValue; }
		}

		/// <summary>
		///   Looks up a localized string similar to "Positive numbers have been passed.".
		/// </summary>
		public string PositiveNumbers
		{
			get { return SaberRes.PositiveNumbers; }
		}

		/// <summary>
		///   Looks up a localized string similar to Positive numbers or zero have been passed..
		/// </summary>
		public string PostiveNumbersOrZero
		{
			get { return SaberRes.PostiveNumbersOrZero; }
		}

		/// <summary>
		///   Looks up a string similar to ": ".
		/// </summary>
		public string SentenceSeparator
		{
			get { return SaberRes.SentenceSeparator; }
		}

		/// <summary>
		///   Looks up a string similar to "The start datetime is after or equal to end datetime".
		/// </summary>
		public string StartBeforeEndDate
		{
			get { return SaberRes.StartBeforeEndDate; }
		}

		/// <summary>
		///   Looks up a string similar to "Type {0} can not be assigned to {1}".
		/// </summary>
		public string TypeCannotBeAssigned
		{
			get { return SaberRes.TypeCannotBeAssigned; }
		}

		/// <summary>
		///   Looks up a string similar to "Type {0} can not be assigned to {1}: interface is not implemented".
		/// </summary>
		public string TypeCannotBeAssignedInterface
		{
			get { return SaberRes.TypeCannotBeAssignedInterface; }
		}

		/// <summary>
		///   Looks up a string similar to "An exception of the wrong type was thrown.\n\nExpected: {0}\nActual: {1}".
		/// </summary>
		public string WrongException
		{
			get { return SaberRes.WrongException; }
		}

		/// <summary>
		///   Looks up a string similar to "The exception messages are not equal.\n\nExpected: {0}\nActual: {1}".
		/// </summary>
		public string WrongMessage
		{
			get { return SaberRes.WrongMessage; }
		}
	}
}
