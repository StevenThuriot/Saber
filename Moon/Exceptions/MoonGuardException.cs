using System;
using System.Diagnostics.CodeAnalysis;

namespace Moon.Exceptions
{
	///<summary>
	/// The Guard has found an error.
	///</summary>
	[Serializable]
	public class MoonGuardException : MoonException
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MoonGuardException"/> class.
		/// </summary>
		/// <param name="message">The message.</param>
		internal MoonGuardException(string message) : base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MoonGuardException"/> class.
		/// </summary>
		internal MoonGuardException()
			: base("The Guard has found an error.")
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MoonGuardException"/> class.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="innerException">The inner exception.</param>
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		internal MoonGuardException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
