using System.Text;

namespace Saber.Extensions
{
	/// <summary>
	/// StringBuilder extension methods
	/// </summary>
	public static class StringBuilderExtensions
	{
		/// <summary>
		/// Clears the current StringBuilder instance.
		/// </summary>
		/// <param name="builder">The StringBuilder</param>
		public static StringBuilder Clear(this StringBuilder builder)
		{
			builder.Length = 0;
			return builder;
		}

		/// <summary>
		/// Prepends the passed value.
		/// </summary>
		/// <param name="builder">The StringBuilder</param>
		/// <param name="value">The value to prepend.</param>
		public static StringBuilder Prepend(this StringBuilder builder, string value)
		{
			return builder.Insert(0, value);
		}
	}
}
