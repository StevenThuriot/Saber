using System.Collections.Generic;
using System.Linq;
using Moon.Extensions;

namespace Moon.Factories
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
