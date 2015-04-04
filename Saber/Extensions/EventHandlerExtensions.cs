using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

namespace Saber.Extensions
{
	///<summary>
	/// Extension methods for EventHandlers.
	///</summary>
	public static class EventHandlerExtensions
	{
		/// <summary>
		/// Safe way to invoke the event (Only invokes if it has any event handlers).
		/// </summary>
		/// <typeparam name="T">Type of event arguments</typeparam>
		/// <param name="eventHandler">The event handler.</param>
		/// <param name="sender">The sender.</param>
		/// <param name="eventArgs">The event arguments.</param>
		/// <returns>The event handler.</returns>
		public static EventHandler<T> InvokeEvent<T>(this EventHandler<T> eventHandler, object sender, T eventArgs)
				where T : EventArgs
		{
			if (eventHandler != null)
			{
				eventHandler(sender, eventArgs);
			}

			return eventHandler;
		}
		
		/// <summary>
		/// Safe way to invoke the event one time for each passed event argument. (Only invokes if it has any event handlers).
		/// </summary>
		/// <param name="eventHandler">The event handler.</param>
		/// <param name="sender">The sender.</param>
		/// <param name="eventArgs">The event arguments.</param>
		/// <returns>The event handler.</returns>
		public static PropertyChangedEventHandler InvokeEvent(this PropertyChangedEventHandler eventHandler, object sender, PropertyChangedEventArgs eventArgs)
		{
			if (eventHandler != null)
			{
				eventHandler(sender, eventArgs);
			}

			return eventHandler;
		}

		/// <summary>
		/// Safe way to invoke the event one time for each passed event argument. (Only invokes if it has any event handlers).
		/// </summary>
		/// <typeparam name="T">Type of event arguments</typeparam>
		/// <param name="eventHandler">The event handler.</param>
		/// <param name="sender">The sender.</param>
		/// <param name="eventArgs">The event arguments.</param>
		/// <returns>The event handler.</returns>
		public static EventHandler<T> InvokeEvent<T>(this EventHandler<T> eventHandler, object sender, params T[] eventArgs)
				where T : EventArgs
		{
			eventArgs.ForEach(x => eventHandler.InvokeEvent(sender, x));

			return eventHandler;
		}

		/// <summary>
		/// Safe way to invoke the event one time for each passed event argument. (Only invokes if it has any event handlers).
		/// </summary>
		/// <param name="eventHandler">The event handler.</param>
		/// <param name="sender">The sender.</param>
		/// <param name="eventArgs">The event arguments.</param>
		/// <returns>The event handler.</returns>
		public static PropertyChangedEventHandler InvokeEvent(this PropertyChangedEventHandler eventHandler, object sender, params PropertyChangedEventArgs[] eventArgs)
		{
			eventArgs.ForEach(x => eventHandler.InvokeEvent(sender, x));

			return eventHandler;
		}

		/// <summary>
		/// Safe way to invoke the event one time for each passed argument. (Only invokes if it has any event handlers).
		/// </summary>
		/// <param name="eventHandler">The event handler.</param>
		/// <param name="sender">The sender.</param>
		/// <param name="eventArgs">The event arguments.</param>
		/// <returns>The event handler.</returns>
		public static PropertyChangedEventHandler InvokeEvent(this PropertyChangedEventHandler eventHandler, object sender, params string[] eventArgs)
		{
			var propertyChangedEventArgs = new List<PropertyChangedEventArgs>();
			
			eventArgs.ForEach(x => propertyChangedEventArgs.Add(new PropertyChangedEventArgs(x)));

			return eventHandler.InvokeEvent(sender, propertyChangedEventArgs.ToArray());
		}

		/// <summary>
		/// Safe way to invoke the event one time for each passed property. (Only invokes if it has any event handlers).
		/// </summary>
		/// <param name="eventHandler">The event handler.</param>
		/// <param name="sender">The sender.</param>
		/// <param name="expressions">The properties triggering the expression.</param>
		/// <returns>The event handler.</returns>
		public static PropertyChangedEventHandler InvokeEvent(this PropertyChangedEventHandler eventHandler, object sender, params Expression<Func<object>>[] expressions)
		{
			expressions.ForEach(x => eventHandler.InvokeEvent(sender, new PropertyChangedEventArgs(x.GetPropertyName())));

			return eventHandler;
		}

		///<summary>
		/// Adds all passed invocations to the event handler so they will be triggered when invoced.
		/// This method is not declared as an extension method, because the event handler needs to be passed as ref for this to work.
		///</summary>
		///<param name="eventHandler">The event handler.</param>
		///<param name="invocations">The list of invocations to add</param>
		///<typeparam name="T"></typeparam>
		///<returns>The event handler.</returns>
		public static EventHandler<T> AddInvocations<T>(ref EventHandler<T> eventHandler, params Action<object, T>[] invocations)
			where T : EventArgs
		{
			return eventHandler = invocations.Aggregate(eventHandler, (current, invocation) => current + new EventHandler<T>(invocation));
		}

		///<summary>
		/// Removes all all occurrences of the invocation list.
		/// This method is not declared as an extension method, because the event handler needs to be passed as ref for this to work.
		///</summary>
		///<param name="eventHandler">The event.</param>
		///<typeparam name="T">The event arguments.</typeparam>
		///<returns>The event handler.</returns>
		public static EventHandler<T> RemoveAllInvocations<T>(ref EventHandler<T> eventHandler)
				where T : EventArgs
		{
			if (eventHandler != null)
			{
				var d = eventHandler as Delegate;
				eventHandler = Delegate.RemoveAll(d, d) as EventHandler<T>;
			}

			return eventHandler;
		}
	}
}
