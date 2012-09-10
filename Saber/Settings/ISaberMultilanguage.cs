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
namespace Saber.Settings
{
	///<summary>
	/// Interface for easily making the Saber Framework multilanguage.
	///</summary>
	public interface ISaberMultilanguage
	{
		///<summary>
		/// Looks up a string similar to "]".
		///</summary>
		string DatamemberBracketClose { get; }

		/// <summary>
		///   Looks up a string similar to "[".
		/// </summary>
		string DatamemberBracketOpen { get; }

		/// <summary>
		///   Looks up a string similar to " = ".
		/// </summary>
		string DatamemberEquals { get; }

		/// <summary>
		///   Looks up a string similar to "Some of the passed Guids are empty".
		/// </summary>
		string EmptyGuids { get; }

		/// <summary>
		///   Looks up a string similar to ".".
		/// </summary>
		string EndOfSentence { get; }

		/// <summary>
		///   Looks up a string similar to ", ".
		/// </summary>
		string ErrorSeparator { get; }

		/// <summary>
		///   Looks up a localized string similar to "Exception of type {0} should have been thrown".
		/// </summary>
		string ExceptionNotThrown { get; }

		/// <summary>
		///   Looks up a string similar to "Class: {0} - Method: {1} - {2}".
		/// </summary>
		string GuardErrorMessageBase { get; }

		/// <summary>
		///   Looks up a string similar to "Args: {0}".
		/// </summary>
		string GuardErrorMessage { get; }

		/// <summary>
		///   Looks up a string similar to "Not all arguments can not be converted to Guids".
		/// </summary>
		string GuidParseError { get; }
		
		/// <summary>
		///   Looks up a localized string similar to An invalid email has been passed..
		/// </summary>
		string InvalidEmail { get; }

		/// <summary>
		///   Looks up a string similar to "Not all booleans are false".
		/// </summary>
		string IsFalse { get; }

		/// <summary>
		///   Looks up a string similar to "Not all booleans are true".
		/// </summary>
		string IsTrue { get; }

		/// <summary>
		///   Looks up a localized string similar to "Negative numbers have been passed.".
		/// </summary>
		string NegativeNumbers { get; }

		/// <summary>
		///   Looks up a localized string similar to "Negative numbers or zero have been passed.".
		/// </summary>
		string NegativeNumbersOrZero { get; }

		/// <summary>
		///   Looks up a localized string similar to "A type was passed that wasn't serializable".
		/// </summary>
		string IsSerializable { get; }

		/// <summary>
		///   Looks up a string similar to "No arguments".
		/// </summary>
		string NoArguments { get; }

		/// <summary>
		///   Looks up a string similar to "Not all passed dates are in the future".
		/// </summary>
		string NotAllDatesAreInTheFuture { get; }

		/// <summary>
		///   Looks up a string similar to "Not all passed dates are in the past".
		/// </summary>
		string NotAllDatesAreInThePast { get; }

		///<summary>
		/// Looks up a string similar to "Some of the passed arguments aren't compliant with the given expression".
		///</summary>
		string NotCompliant { get; }

		/// <summary>
		///   Looks up a string similar to "NULL, empty or whitespace has been passed".
		/// </summary>
		string NullEmptyOrWhiteSpaceHasBeenPassed { get; }

		/// <summary>
		///   Looks up a string similar to "NULL has been passed".
		/// </summary>
		string NullHasBeenPassed { get; }

		/// <summary>
		///   Looks up a string similar to "NULL".
		/// </summary>
		string NullValue { get; }

		/// <summary>
		///   Looks up a localized string similar to "Positive numbers have been passed.".
		/// </summary>
		string PositiveNumbers { get; }

		/// <summary>
		///   Looks up a localized string similar to Positive numbers or zero have been passed..
		/// </summary>
		string PostiveNumbersOrZero { get; }

		/// <summary>
		///   Looks up a string similar to ": ".
		/// </summary>
		string SentenceSeparator { get; }

		/// <summary>
		///   Looks up a string similar to "The start datetime is after or equal to end datetime".
		/// </summary>
		string StartBeforeEndDate { get; }

		/// <summary>
		///   Looks up a string similar to "Type {0} can not be assigned to {1}".
		/// </summary>
		string TypeCannotBeAssigned { get; }

		/// <summary>
		///   Looks up a string similar to "Type {0} can not be assigned to {1}: interface is not implemented".
		/// </summary>
		string TypeCannotBeAssignedInterface { get; }

		/// <summary>
		///   Looks up a string similar to "An exception of the wrong type was thrown.\n\nExpected: {0}\nActual: {1}".
		/// </summary>
		string WrongException { get; }

		/// <summary>
		///   Looks up a string similar to "The exception messages are not equal.\n\nExpected: {0}\nActual: {1}".
		/// </summary>
		string WrongMessage { get; }
	}
}
