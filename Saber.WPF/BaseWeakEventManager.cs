﻿#region License
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
using System.Windows;
using System.Windows.Threading;

/// <summary>
/// A base weak event manager to easily and safely subscribe and unsubscribe to events.
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
/// <typeparam name="TSelf">The type of the inheriting class.</typeparam>
public abstract class BaseWeakEventManager<TEntity, TSelf> : WeakEventManager
	where TSelf : BaseWeakEventManager<TEntity, TSelf>, new()
{
	/// <summary>
	/// Adds the listener.
	/// </summary>
	/// <param name="source">The source.</param>
	/// <param name="listener">The listener.</param>
	public static void AddListener(TEntity source, IWeakEventListener listener)
	{
		CurrentManager.ProtectedAddListener(source, listener);
	}

	/// <summary>
	/// Removes the listener.
	/// </summary>
	/// <param name="source">The source.</param>
	/// <param name="listener">The listener.</param>
	public static void RemoveListener(TEntity source, IWeakEventListener listener)
	{
		CurrentManager.ProtectedRemoveListener(source, listener);
	}

	protected sealed override void StartListening(object source)
	{
		var entity = (TEntity)source;
		if (Dispatcher.CheckAccess())
		{
			Subscribe(entity);
		}
		else
		{
			Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => Subscribe(entity)));
		}
	}

	protected sealed override void StopListening(object source)
	{
		var entity = (TEntity)source;
		if (Dispatcher.CheckAccess())
		{
			Unsubscribe(entity);
		}
		else
		{
			Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => Unsubscribe(entity)));
		}
	}

	private static TSelf CurrentManager
	{
		get
		{
			Type managerType = typeof(TSelf);
			var manager = (TSelf)GetCurrentManager(managerType);
			if (manager == null)
			{
				manager = new TSelf();
				SetCurrentManager(managerType, manager);
			}
			return manager;
		}
	}

	/// <summary>
	/// Safely subscribes the specified entity.
	/// </summary>
	/// <param name="entity">The entity.</param>
	protected abstract void Subscribe(TEntity entity);

	/// <summary>
	/// Safely unsubscribes the specified entity.
	/// </summary>
	/// <param name="entity">The entity.</param>
	protected abstract void Unsubscribe(TEntity entity);
}