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

namespace Saber.Factories
{
	/// <summary>
	/// Base factory interface.
	/// </summary>
	/// <typeparam name="TInput">The type of the input.</typeparam>
	/// <typeparam name="TOutput">The type of the output.</typeparam>
	public interface IFactory<TInput, TOutput>
	{
		/// <summary>
		/// Creates the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>An instance of type TOutput</returns>
		TOutput Create(TInput value);

		/// <summary>
		/// Creates the specified values.
		/// </summary>
		/// <param name="values">The values.</param>
		/// <returns>A list of instances of type TOutput</returns>
		IEnumerable<TOutput> Create(IEnumerable<TInput> values);
	}
}
