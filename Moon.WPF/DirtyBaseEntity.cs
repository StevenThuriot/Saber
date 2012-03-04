using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Moon.WPF
{
	///<summary>
	/// Base class for domain objects that will be used in WPF projects.
	/// This expands the normal BaseEntity with dirty tracking.
	///</summary>
	[Serializable]
	public abstract class DirtyBaseEntity : BaseEntity, ISerializable
	{
		#region IgnoreDirtyScope

		/// <summary>
		/// Class to temporarily stop dirty tracking.
		/// Should be used with in a using (new IgnoreDirtyScope(Model)) statement.
		/// </summary>
		private class IgnoreDirtyScope : IDisposable
		{
			private bool _Disposed;
			private DirtyBaseEntity _Model;
			private readonly bool _IsDirty;

			/// <summary>
			/// Default ctor used to temporarily stop dirty tracking.
			/// </summary>
			/// <param name="model">The model to pause the tracking on.</param>
			public IgnoreDirtyScope(DirtyBaseEntity model)
			{
				_Disposed = false;

				_Model = model;

				_IsDirty = _Model._IsDirty;
				_Model.StopDirtyTracking();
			}

			~IgnoreDirtyScope()
			{
				Dispose(false);
			}

			public void Dispose()
			{
				Dispose(true);
				GC.SuppressFinalize(this);
			}

			private void Dispose(bool disposing)
			{
				if (!_Disposed) return;

				if (disposing)
				{
					_Model.ResetDirtyTracking();
					_Model._IsDirty = _IsDirty;

					_Model = null;
				}

				_Disposed = true;
			}
		}

		#endregion IgnoreDirtyScope

		private bool _IsDirty;

        /// <summary>
        /// Default ctor.
        /// Enables dirty tracking from the start.
        /// </summary>
		protected DirtyBaseEntity()
		{
			_IsDirty = false;
			PropertyChanged += DirtyHandler;
		}

		private void DirtyHandler(object sender, PropertyChangedEventArgs e)
		{
			_IsDirty = true;
			PropertyChanged -= DirtyHandler;
		}

		/// <summary>
		/// Indicates if the model is dirty.
		/// </summary>
		/// <returns>True in case the model is dirty.</returns>
		public bool HasChanges()
		{
			return _IsDirty;
		}

		/// <summary>
		/// Stops dirty tracking alltogether.
		/// </summary>
		public void StopDirtyTracking()
		{
			_IsDirty = false;
			PropertyChanged -= DirtyHandler;
		}

		/// <summary>
		/// Pauses dirty tracking. 
		/// Should be used in a using statement: using (Model.PauseDirtyTracking()) { /* Changes */ }
		/// </summary>
		/// <returns>An IDisposible to use in a using statement.</returns>
		public IDisposable PauseDirtyTracking()
		{
			return new IgnoreDirtyScope(this);
		}

		/// <summary>
		/// Resets dirty tracking.
		/// </summary>
		public void ResetDirtyTracking()
		{
			_IsDirty = false;

			PropertyChanged -= DirtyHandler;
			PropertyChanged += DirtyHandler;
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="DirtyBaseEntity"/> class.
		/// </summary>
		/// <param name="info">The info.</param>
		/// <param name="context">The context.</param>
		protected DirtyBaseEntity(SerializationInfo info, StreamingContext context)
		{
			_IsDirty = info.GetBoolean("IsDirty");

			if (!_IsDirty)
			{
				PropertyChanged += DirtyHandler;
			}
		}

		/// <summary>
		/// Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo"/> with the data needed to serialize the target object.
		/// </summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> to populate with data.</param>
		/// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext"/>) for this serialization.</param>
		/// <exception cref="T:System.Security.SecurityException">
		/// The caller does not have the required permission.
		///   </exception>
		[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("IsDirty", _IsDirty);
		}
	}
}
