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
using System.Collections.Generic;
using System.Linq;
using Saber.Extensions;

namespace Saber.Factories
{
	/// <summary>
	/// A base factory class, usually to create client-side instances from instances received from the server.
	/// </summary>
	/// <typeparam name="TInput">The type of the input.</typeparam>
	/// <typeparam name="TOutput">The type of the output.</typeparam>
	public abstract class BaseFactory<TInput, TOutput> : IFactory<TInput, TOutput>
	{
		/// <summary>
		/// Creates the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>An instance of type TOutput</returns>
		public abstract TOutput Create(TInput value);

		/// <summary>
		/// Creates the specified values.
		/// </summary>
		/// <param name="values">The values.</param>
		/// <returns>A list of instances of type TOutput</returns>
		public virtual IEnumerable<TOutput> Create(IEnumerable<TInput> values)
		{
			return values.Select(Create).AsReadOnly();
		}
	}
}
