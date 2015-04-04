using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Saber.Extensions;

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