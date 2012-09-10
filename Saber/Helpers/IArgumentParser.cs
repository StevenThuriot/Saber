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
using System.Diagnostics.CodeAnalysis;

namespace Saber.Helpers
{
	/// <summary>
	/// A parser class to easily retreive start up arguments.
	/// </summary>
	public interface IArgumentParser
	{
		/// <summary>
		/// The string a command has to start with. Default is set to "/"
		/// </summary>
		string CommandStarter { get; set; }

		/// <summary>
		/// A parse method that parses the Start Up arguments into a dictionary.
		/// You want to use this method when every command has exactly one value.
		/// Commands will still be added as a key if they do not have a value.
		/// </summary>
		/// <param name="arguments">The Start Up arguments.</param>
		/// <returns>A dictionary of commands and values.</returns>
		IDictionary<string, string> Parse(string[] arguments);

		/// <summary>
		/// A parse method that parses the Start Up arguments into a dictionary.
		/// You want to use this method when every command has one or more values.
		/// Commands will still be added as a key if they do not have a value.
		/// </summary>
		/// <param name="arguments">The Start Up arguments.</param>
		/// <returns>A dictionary of commands and values.</returns>
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		IDictionary<string, IList<string>> ParseMultiValues(string[] arguments);
	}
}