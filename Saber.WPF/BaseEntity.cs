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
using System.Diagnostics.CodeAnalysis;
using Saber.Extensions;
using System.Linq.Expressions;
using System.ComponentModel;

namespace Saber.WPF
{
	///<summary>
	/// Base class for domain objects that will be used in WPF projects.
	///</summary>
	[Serializable]
	public abstract class BaseEntity : INotifyPropertyChanged
	{
		///<summary>
		/// Notifies clients that a property value has changed.
		///</summary>
		[field:NonSerialized]
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Notifies clients that propertyName has changed.
		/// </summary>
		/// <param name="propertyName">The property that changed.</param>
		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChanged.InvokeEvent(this, new PropertyChangedEventArgs(propertyName));
		}

		/// <summary>
		/// Notifies clients that propertyName has changed.
		/// </summary>
		/// <param name="propertyExpression">The property that changed.</param>
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures"),
		 SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		protected void OnPropertyChanged(Expression<Func<object>> propertyExpression)
		{
			var name = propertyExpression.GetPropertyName();
			OnPropertyChanged(name);
		}

		/// <summary>
		/// Checks if the private member changed. If it did, it will set the new value and call the PropertyChanged event handler
		/// </summary>
		/// <typeparam name="T">The type of the member.</typeparam>
		/// <param name="privateMember">The member that is being set.</param>
		/// <param name="value">The new value.</param>
		/// <param name="propertyName">The property that changed.</param>
		protected void SetValue<T>(ref T privateMember, T value, string propertyName)
		{
			if (!Equals(privateMember, value))
			{
				privateMember = value;
				OnPropertyChanged(propertyName);
			}
		}

		/// <summary>
		/// Checks if the private member changed. If it did, it will set the new value and call the PropertyChanged event handler
		/// </summary>
		/// <typeparam name="T">The type of the member.</typeparam>
		/// <param name="privateMember">The member that is being set.</param>
		/// <param name="value">The new value.</param>
		/// <param name="propertyExpression">The property that changed.</param>
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures"),
		 SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		protected void SetValue<T>(ref T privateMember, T value, Expression<Func<object>> propertyExpression)
		{
			SetValue(ref privateMember, value, propertyExpression.GetPropertyName());
		}

	}
}