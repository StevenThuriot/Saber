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
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Saber.Extensions
{
	/// <summary>
	/// Extensions on ICloneable objects.
	/// </summary>
	public static class CloneableExtensions
	{
		/// <summary>
		/// Clones an ICloneable object and casts it to the type you specified.
		/// </summary>
		/// <param name="value">The instance to clone.</param>
		/// <typeparam name="T">The type to cast the clone to.</typeparam>
		/// <returns>A clone of the specified instance, casted to the wanted type.</returns>
		public static T CloneAs<T>(this ICloneable value)
		{
			return (T)value.Clone();
		}

		/// <summary>
		/// Clones an instance of type T. 
		/// If ICloneable is implemented, the implemented Clone() method will be called.
		/// If not, a deep clone (by using serialization) will be made.
		/// </summary>
		/// <typeparam name="T">The type of object being cloned.</typeparam>
		/// <param name="value">The object instance to clone.</param>
		/// <exception cref="ArgumentException">Throws an exception in case the type is not serializable.</exception>
		/// <returns>The cloned object.</returns>
		public static T BruteClone<T>(this T value)
		{
			var cloneable = value as ICloneable;
			return cloneable != null ? cloneable.CloneAs<T>() : value.DeepClone();
		}

		/// <summary>
		/// Perform a deep clone of the passed instance.
		/// </summary>
		/// <typeparam name="T">The type of object being cloned.</typeparam>
		/// <param name="value">The object instance to clone.</param>
		/// <exception cref="ArgumentException">Throws an exception in case the type is not serializable.</exception>
		/// <returns>The cloned object.</returns>
		public static T DeepClone<T>(this T value)
		{
			if (ReferenceEquals(value, null))
			{
				return default(T);
			}

			var type = typeof (T);
			if (!type.IsSerializable)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "The type \"{0}\" must be serializable.", type), "value");
			}

			var formatter = new BinaryFormatter();
			using (var stream = new MemoryStream())
			{
				formatter.Serialize(stream, value);
				stream.Seek(0, SeekOrigin.Begin);
				return (T)formatter.Deserialize(stream);
			}
		}
	}

}
