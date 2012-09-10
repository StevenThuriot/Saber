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
using System.Diagnostics;
using Saber.Helpers;

namespace Saber.Domain
{
	/// <summary>
	/// A period in time with a beginning and an end.
	/// </summary>
	[DebuggerDisplay("Start date: {StartDate}, End date: {EndDate}")]
	public class Period : IPeriod
	{
		/// <summary>
		/// Gets the start datetime.
		/// </summary>
		public DateTime StartDate { get; private set; }

		/// <summary>
		/// Gets the end datetime.
		/// </summary>
		public DateTime EndDate { get; private set; }

		///<summary>
		/// The duration of this period in time.
		///</summary>
		public TimeSpan Duration { get; private set; }
		
		/// <summary>
		/// Initializes a new instance of the <see cref="Period"/> class.
		/// </summary>
		/// <param name="startDateTime">The start.</param>
		/// <param name="endDateTime">The end.</param>
		/// <exception cref="ArgumentException">The start datetime is after or equal to end datetime.</exception>
        public Period(DateTime startDateTime, DateTime endDateTime)
		{
			SetPeriod(startDateTime, endDateTime);
		}

		/// <summary>
		/// Sets the end datetime.
		/// </summary>
		/// <param name="endDate">The end datetime.</param>
		/// <exception cref="ArgumentException">The start datetime is after or equal to end datetime.</exception>
		public void SetEnd(DateTime endDate)
		{
			Guard.StartBeforeEnd(StartDate, endDate);

			EndDate = endDate;
			Duration = EndDate - StartDate;
		}

		/// <summary>
		/// Sets the period in time.
		/// </summary>
		/// <param name="period">The period in time.</param>
		/// <exception cref="ArgumentException">The start datetime is after or equal to end datetime.</exception>
		public void SetPeriod(IPeriod period)
		{
			Guard.NotNull(period);

			StartDate = period.StartDate;
			EndDate = period.EndDate;

			Duration = EndDate - StartDate;
		}

		/// <summary>
		/// Sets the period in time.
		/// </summary>
		/// <param name="startDate">The start datetime.</param>
		/// <param name="endDate">The end datetime.</param>
		/// <exception cref="ArgumentException">The start datetime is after or equal to end datetime.</exception>
		public void SetPeriod(DateTime startDate, DateTime endDate)
		{
			Guard.StartBeforeEnd(startDate, endDate);

			StartDate = startDate;
			EndDate = endDate;

			Duration = EndDate - StartDate;
		}

		/// <summary>
		/// Sets the start datetime.
		/// </summary>
		/// <param name="start">The start datetime.</param>
		/// <exception cref="ArgumentException">The start datetime is after or equal to end datetime.</exception>
		public void SetStart(DateTime start)
		{
			Guard.StartBeforeEnd(start, EndDate);

			StartDate = start;

			Duration = EndDate - StartDate;
		}

		/// <summary>
		/// Determines whether the first period contains the second.
		/// </summary>
		/// <param name="firstPeriod">The first period.</param>
		/// <param name="secondPeriod">The second period.</param>
		/// <returns>
		///     <c>true</c> if the first period contains the second; otherwise, <c>false</c>.
		/// </returns>
		public static Boolean Contains(IPeriod firstPeriod, IPeriod secondPeriod)
		{
			return firstPeriod != null && secondPeriod != null &&
				   secondPeriod.StartDate >= firstPeriod.StartDate && secondPeriod.EndDate <= firstPeriod.EndDate;
		}

		/// <summary>
		/// Determines whether the specified periods overlap.
		/// </summary>
		/// <param name="firstPeriod">The first period.</param>
		/// <param name="secondPeriod">The second period.</param>
		/// <returns>
		///     <c>true</c> if the specified periods overlap; otherwise, <c>false</c>.
		/// </returns>
		public static Boolean HasOverlap(IPeriod firstPeriod, IPeriod secondPeriod)
		{
			return firstPeriod != null && secondPeriod != null &&
				   secondPeriod.StartDate < firstPeriod.EndDate && secondPeriod.EndDate > firstPeriod.StartDate;
		}

		/// <summary>
		/// Determines whether the first period contains the second.
		/// </summary>
		/// <param name="period">The period.</param>
		/// <returns>
		///     <c>true</c> if the first period contains the second; otherwise, <c>false</c>.
		/// </returns>
		public Boolean Contains(IPeriod period)
		{
			return Contains(this, period);
		}

		/// <summary>
		/// Determines whether the specified periods overlap.
		/// </summary>
		/// <param name="period">The period.</param>
		/// <returns>
		///     <c>true</c> if the specified periods overlap; otherwise, <c>false</c>.
		/// </returns>
		public Boolean HasOverlap(IPeriod period)
		{
			return HasOverlap(this, period);
		}

		/// <summary>
		/// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
		/// </summary>
		/// <returns>
		/// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
		/// </returns>
		/// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>. 
		///                 </param><exception cref="T:System.NullReferenceException">The <paramref name="obj"/> parameter is null.
		///                 </exception><filterpriority>2</filterpriority>
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof (Period)) return false;
			return Equals((Period) obj);
		}

		/// <summary>
		/// Determines whether the specified <see cref="T:Saber.Domain.Period"/> is equal to the current <see cref="T:Saber.Domain.Period"/>.
		/// </summary>
		/// <returns>
		/// true if the specified <see cref="T:Saber.Domain.Period"/> is equal to the current <see cref="T:Saber.Domain.Period"/>; otherwise, false.
		/// </returns>
		/// <param name="other">The <see cref="T:Saber.Domain.Period"/> to compare with the current <see cref="T:Saber.Domain.Period"/>. 
		///                 </param><exception cref="T:System.NullReferenceException">The <paramref name="other"/> parameter is null.
		///                 </exception><filterpriority>2</filterpriority>
		public bool Equals(Period other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.StartDate.Equals(StartDate) && other.EndDate.Equals(EndDate);
		}

		/// <summary>
		/// Serves as a hash function for a particular type. 
		/// </summary>
		/// <returns>
		/// A hash code for the current <see cref="T:System.Object"/>.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override int GetHashCode()
		{
			unchecked
			{
				return (StartDate.GetHashCode()*397) ^ EndDate.GetHashCode();
			}
		}

		/// <summary>
		/// Determines wether the left period equals the right period.
		/// </summary>
		/// <param name="left">The first period.</param>
		/// <param name="right">The other period.</param>
		/// <returns>True if both periods are equal.</returns>
		public static bool operator ==(Period left, Period right)
		{
			return Equals(left, right);
		}

		/// <summary>
		/// Determines wether the left period does not equal the right period.
		/// </summary>
		/// <param name="left">The first period.</param>
		/// <param name="right">The other period.</param>
		/// <returns>True if the supplied periods are different.</returns>
		public static bool operator !=(Period left, Period right)
		{
			return !Equals(left, right);
		}
	}
}