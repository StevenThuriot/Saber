using System;
using System.Diagnostics.CodeAnalysis;

namespace Moon.Exceptions
{
    /// <summary>
    /// This exception will be used by the Assertion class.
    /// Failing to assert, the Assertion helper will throw this exception with some extra information what went wrong stored into the message.
	/// </summary>
	[Serializable]
    public class MoonAssertException : MoonException
    {
    	/// <summary>
		/// Initializes a new instance of the <see cref="MoonAssertException"/> class.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="parameters">The parameters.</param>
    	internal MoonAssertException(string message, params String[] parameters)
			: base(message, parameters)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MoonAssertException"/> class.
		/// </summary>
		internal MoonAssertException()
			: base("Assertion has failed.")
		{
		}

    	/// <summary>
		/// Initializes a new instance of the <see cref="MoonAssertException"/> class.
    	/// </summary>
    	/// <param name="message">The message.</param>
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		internal MoonAssertException(string message) : base(message)
    	{
    	}

		/// <summary>
		/// Initializes a new instance of the <see cref="MoonAssertException"/> class.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="innerException">The inner exception.</param>
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		internal MoonAssertException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
    }
}