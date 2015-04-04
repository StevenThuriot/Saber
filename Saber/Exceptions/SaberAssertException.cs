using System;
using System.Diagnostics.CodeAnalysis;

namespace Saber.Exceptions
{
    /// <summary>
    /// This exception will be used by the Assertion class.
    /// Failing to assert, the Assertion helper will throw this exception with some extra information what went wrong stored into the message.
	/// </summary>
	[Serializable]
    public class SaberAssertException : SaberException
    {
    	/// <summary>
		/// Initializes a new instance of the <see cref="SaberAssertException"/> class.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="parameters">The parameters.</param>
    	internal SaberAssertException(string message, params String[] parameters)
			: base(message, parameters)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SaberAssertException"/> class.
		/// </summary>
		internal SaberAssertException()
			: base("Assertion has failed.")
		{
		}

    	/// <summary>
		/// Initializes a new instance of the <see cref="SaberAssertException"/> class.
    	/// </summary>
    	/// <param name="message">The message.</param>
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		internal SaberAssertException(string message) : base(message)
    	{
    	}

		/// <summary>
		/// Initializes a new instance of the <see cref="SaberAssertException"/> class.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="innerException">The inner exception.</param>
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		internal SaberAssertException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
    }
}