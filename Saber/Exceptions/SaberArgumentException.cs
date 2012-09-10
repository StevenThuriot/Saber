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
using System.Linq.Expressions;
using System.Diagnostics.CodeAnalysis;

using Saber.Extensions;

namespace Saber.Exceptions
{
	///<summary>
	/// A faulty argument was passed to the Saber Framework.
	///</summary>
	[Serializable]
	public class SaberArgumentException : SaberException
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SaberException"/> class.
		/// </summary>
		/// <param name="expression">The passed variable that caused the exception.</param>
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		internal SaberArgumentException(Expression<Func<object>> expression)
			: base("A faulty argument was passed to the Saber Framework.", new ArgumentException(expression.GetPropertyName()))
		{
		}
	}
}
