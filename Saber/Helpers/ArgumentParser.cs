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
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;

namespace Saber.Helpers
{
	/// <summary>
	/// A parser class to easily retreive start up arguments.
	/// </summary>
	[DebuggerDisplay("Saber's Argument Parser")]
	public class ArgumentParser : IArgumentParser
	{
		private string _CommandStarter = "/";

		/// <summary>
		/// The string a command has to start with. Default is set to "/"
		/// </summary>
		public string CommandStarter
		{
			get { return _CommandStarter; }
			set { _CommandStarter = value; }
		}

		/// <summary>
		/// A parse method that parses the Start Up arguments into a dictionary.
		/// You want to use this method when every command has exactly one value.
		/// Commands will still be added as a key if they do not have a value.
		/// </summary>
		/// <param name="arguments">The Start Up arguments.</param>
		/// <returns>A dictionary of commands and values.</returns>
		public IDictionary<string, string> Parse(string[] arguments)
		{
			var dictionary = new Dictionary<string, string>();

			string key = string.Empty;
			var builder = new StringBuilder();

			for (int i = 0; i < arguments.Length; i++)
			{
				var argument = arguments[i];

				if (argument.StartsWith(CommandStarter, true, CultureInfo.CurrentCulture))
				{
					var strippedArgument = argument.Substring(1).ToUpperInvariant();

					if (i == 0)
					{
						key = strippedArgument;
					}
					else
					{
						dictionary.Add(key, builder.ToString());

						key = strippedArgument;
						builder.Length = 0;
					}
				}
				else
				{
					builder.Append(" ");
					builder.Append(argument);
				}
			}

			dictionary.Add(key, builder.ToString());

			return dictionary;
		}

		/// <summary>
		/// A parse method that parses the Start Up arguments into a dictionary.
		/// You want to use this method when every command has one or more values.
		/// Commands will still be added as a key if they do not have a value.
		/// </summary>
		/// <param name="arguments">The Start Up arguments.</param>
		/// <returns>A dictionary of commands and values.</returns>
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public IDictionary<string, IList<string>> ParseMultiValues(string[] arguments)
		{
			var dictionary = new Dictionary<string, IList<string>>();

			string key = string.Empty;
			var values = new List<string>();

			for (int i = 0; i < arguments.Length; i++)
			{
				var argument = arguments[i];

				if (argument.StartsWith(CommandStarter, true, CultureInfo.CurrentCulture))
				{
					var strippedArgument = argument.Substring(1).ToUpperInvariant();

					if (i == 0)
					{
						key = strippedArgument;
					}
					else
					{
						dictionary.Add(key, values);

						key = strippedArgument;
						values = new List<string>();
					}
				}
				else
				{
					values.Add(argument);
				}
			}

			dictionary.Add(key, values);

			return dictionary;
		}
	}
}
