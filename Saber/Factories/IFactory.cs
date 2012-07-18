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
