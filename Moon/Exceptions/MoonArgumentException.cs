using System;
using System.Linq.Expressions;
using System.Diagnostics.CodeAnalysis;

using Moon.Extensions;

namespace Moon.Exceptions
{
	///<summary>
	/// A faulty argument was passed to the Moon Framework.
	///</summary>
	[Serializable]
	public class MoonArgumentException : MoonException
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MoonException"/> class.
		/// </summary>
		/// <param name="expression">The passed variable that caused the exception.</param>
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		internal MoonArgumentException(Expression<Func<object>> expression)
			: base("A faulty argument was passed to the Moon Framework.", new ArgumentException(expression.GetPropertyName()))
		{
		}
	}
}
