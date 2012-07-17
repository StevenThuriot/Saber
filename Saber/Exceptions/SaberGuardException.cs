using System;
using System.Diagnostics.CodeAnalysis;

namespace Saber.Exceptions
{
	///<summary>
	/// The Guard has found an error.
	///</summary>
	[Serializable]
	public class SaberGuardException : SaberException
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SaberGuardException"/> class.
		/// </summary>
		/// <param name="message">The message.</param>
		internal SaberGuardException(string message) : base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SaberGuardException"/> class.
		/// </summary>
		internal SaberGuardException()
			: base("The Guard has found an error.")
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SaberGuardException"/> class.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="innerException">The inner exception.</param>
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		internal SaberGuardException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
