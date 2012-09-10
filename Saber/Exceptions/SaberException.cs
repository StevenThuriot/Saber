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
using System;
using System.Runtime.Serialization;
using Saber.Extensions;

namespace Saber.Exceptions
{
	///<summary>
	/// The base exception class for every exception the Saber Framework will throw.
	///</summary>
	[Serializable]
	public abstract class SaberException : Exception
	{
		/// <summary>
        /// Initializes a new instance of the <see cref="SaberException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="parameters">The parameters.</param>
		protected SaberException(string message, params String[] parameters)
                : base(message.FormatWith(parameters))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SaberException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        protected SaberException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SaberException"/> class.
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
        protected SaberException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SaberException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        protected SaberException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SaberException"/> class.
        /// </summary>
        protected SaberException()
			: base("An error has occurred in the Saber Framework.")
        {
        }
	}
}
