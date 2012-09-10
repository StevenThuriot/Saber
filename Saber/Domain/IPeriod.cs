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
using System;

namespace Saber.Domain
{
	/// <summary>
	/// A period in time with a beginning and an end.
	/// </summary>
	public interface IPeriod
	{
		/// <summary>
		/// Gets the start datetime.
		/// </summary>
		DateTime StartDate { get; }

		/// <summary>
		/// Gets the end datetime.
		/// </summary>
		DateTime EndDate { get; }

		///<summary>
		/// The duration of this period in time.
		///</summary>
		TimeSpan Duration { get; }

		/// <summary>
		/// Determines whether this period contains the given period.
		/// </summary>
		/// <param name="period">The period.</param>
		/// <returns>
		/// 	<c>true</c> if this period contains the given period; otherwise, <c>false</c>.
		/// </returns>
		Boolean Contains(IPeriod period);

		/// <summary>
		/// Determines whether the specified period overlaps with this period.
		/// </summary>
		/// <param name="period">The period.</param>
		/// <returns>
		/// 	<c>true</c> if the specified period overlaps with this one; otherwise, <c>false</c>.
		/// </returns>
		Boolean HasOverlap(IPeriod period);

		/// <summary>
		/// Sets the end datetime.
		/// </summary>
		/// <param name="endDate">The end datetime.</param>
		/// <exception cref="ArgumentException">The start datetime is after or equal to end datetime.</exception>
		void SetEnd(DateTime endDate);

		/// <summary>
		/// Sets the period in time.
		/// </summary>
		/// <param name="period">The period in time.</param>
		/// <exception cref="ArgumentException">The start datetime is after or equal to end datetime.</exception>
		void SetPeriod(IPeriod period);

		/// <summary>
		/// Sets the period in time.
		/// </summary>
		/// <param name="startDate">The start datetime.</param>
		/// <param name="endDate">The end datetime.</param>
		/// <exception cref="ArgumentException">The start datetime is after or equal to end datetime.</exception>
		void SetPeriod(DateTime startDate, DateTime endDate);

		/// <summary>
		/// Sets the start datetime.
		/// </summary>
		/// <param name="start">The start datetime.</param>
		/// <exception cref="ArgumentException">The start datetime is after or equal to end datetime.</exception>
		void SetStart(DateTime start);

		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		string ToString();
	}
}