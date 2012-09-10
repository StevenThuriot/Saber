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