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
