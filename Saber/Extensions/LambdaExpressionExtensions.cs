using System.Linq.Expressions;
using System.Diagnostics.CodeAnalysis;

namespace Saber.Extensions
{
	/// <summary>
	/// Extension methods for Lamba expressions.
	/// </summary>
	public static class LambdaExpressionExtensions
	{
		///<summary>
		/// Gets the name of the member of a passed expression.
		///</summary>
		///<param name="expression">The member whose name you want as an expression.</param>
		///<returns>The name of the member of the passed expression as a string.</returns>
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public static string GetPropertyName(this LambdaExpression expression)
		{
			var memberExpression = expression.Body as MemberExpression ??
								   ((UnaryExpression)expression.Body).Operand as MemberExpression;

			return memberExpression == null ? string.Empty : memberExpression.Member.Name;
		}
	}
}
