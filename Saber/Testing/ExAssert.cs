using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Saber.Exceptions;

namespace Saber.Testing
{
	/// <summary>
	/// Helper to make exception specific testing in unit tests easier.
	/// </summary>
	[DebuggerDisplay("Saber's Testing helper.")]
	public static class ExAssert
	{
		/// <summary>
		/// Checks if the action throws the specified Exception. 
		/// It also checks if the messages are the same.
		/// </summary>
		/// <typeparam name="T">The type of exception to be thrown.</typeparam>
		/// <param name="action">The action to be performed.</param>
		/// <param name="expectedMessage">The expected message.</param>
		/// <exception cref="SaberAssertException">The assertion failed.</exception>
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter"),
		 SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		public static void Throws<T>(Action action, string expectedMessage)
				where T : Exception
		{
			var failed = false;

			if (action != null)
			{
				try
				{
					action();
					failed = true;
				}
				catch (Exception exc)
				{
					AreEqual(typeof(T), exc.GetType());
					AreEqual(expectedMessage, exc.Message);
				}
			}

			if (failed)
				Fail(typeof(T).Name);
		}

		private static void AreEqual(string expected, string actual)
		{
			if (expected != actual)
			{
				throw new SaberAssertException(
						Settings.Saber.Language.WrongException,
						expected,
						actual);
			}
		}

		private static void AreEqual(Type expected, Type actual)
		{
			if (expected != actual)
			{
				throw new SaberAssertException(
						Settings.Saber.Language.WrongMessage,
						expected.Name,
						actual.Name);
			}
		}

		private static void Fail(string message)
		{
			throw new SaberAssertException
					(
						Settings.Saber.Language.ExceptionNotThrown,
						message
					);
		}
	}
}
