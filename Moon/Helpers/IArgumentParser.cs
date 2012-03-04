using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Moon.Helpers
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