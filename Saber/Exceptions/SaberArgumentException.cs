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
