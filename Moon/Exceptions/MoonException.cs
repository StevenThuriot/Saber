using System;
using System.Runtime.Serialization;
using Moon.Extensions;

namespace Moon.Exceptions
{
	///<summary>
	/// The base exception class for every exception the Moon Framework will throw.
	///</summary>
	[Serializable]
	public abstract class MoonException : Exception
	{
		/// <summary>
        /// Initializes a new instance of the <see cref="MoonException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="parameters">The parameters.</param>
		protected MoonException(string message, params String[] parameters)
                : base(message.FormatWith(parameters))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MoonException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        protected MoonException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MoonException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// The <paramref name="info"/> parameter is null.
        ///   </exception>
        ///   
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">
        /// The class name is null or <see cref="P:System.Exception.HResult"/> is zero (0).
        ///   </exception>
        protected MoonException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MoonException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        protected MoonException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MoonException"/> class.
        /// </summary>
        protected MoonException()
			: base("An error has occurred in the Moon Framework.")
        {
        }
	}
}
