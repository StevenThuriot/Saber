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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace Saber.Extensions
{
	/// <summary>
	/// Serializing extensions to make it easier.
	/// </summary>
	[Serializable]
	public static class SerializableExtensions
	{
		/// <summary>
		/// Serializes the passed value.
		/// </summary>
		/// <typeparam name="T">The type to serialize.</typeparam>
		/// <param name="value">The value to serialize.</param>
		/// <param name="file">Where to write the serialized instance.</param>
		public static void Serialize<T>(this T value, FileSystemInfo file)
		{
			using (var stream = File.OpenWrite(file.FullName))
			{
				var formatter = new BinaryFormatter();
				formatter.Serialize(stream, value);

				stream.Flush();
			}
		}

		/// <summary>
		/// Deserializes the requested file.
		/// </summary>
		/// <typeparam name="T">The type to deserialize.</typeparam>
		/// <param name="value">Used to retreive the type to deserialize.</param>
		/// <param name="file">The file to deserialize.</param>
		/// <returns>The deserialized instance.</returns>
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "value")]
		public static T Deserialize<T>(this T value, FileSystemInfo file)
		{
			return file.Deserialize<T>();
		}

		/// <summary>
		/// Deserializes the requested file.
		/// </summary>
		/// <typeparam name="T">The type to deserialize.</typeparam>
		/// <param name="file">The file to deserialize.</param>
		/// <returns>The deserialized instance.</returns>
		public static T Deserialize<T>(this FileSystemInfo file)
		{
			object deserializedObject = null;

			if (file.Exists)
			{
				using (var stream = File.OpenRead(file.FullName))
				{
					var formatter = new BinaryFormatter();
					deserializedObject = formatter.Deserialize(stream);
				}
			}

			return deserializedObject is T ? (T)deserializedObject : default(T);
		}

		/// <summary>
		/// Serializes the passed value to XML.
		/// </summary>
		/// <typeparam name="T">The type to serialize.</typeparam>
		/// <param name="value">The value to serialize.</param>
		/// <param name="file">Where to write the serialized instance.</param>
		public static void SerializeXml<T>(this T value, FileSystemInfo file)
		{
			var serializer = new XmlSerializer(typeof(T));
			using (var stream = File.OpenWrite(file.FullName))
			{
				serializer.Serialize(stream, value);
			}
		}

		/// <summary>
		/// Deserializes the requested file to XML.
		/// </summary>
		/// <typeparam name="T">The type to deserialize.</typeparam>
		/// <param name="value">Used to retreive the type to deserialize.</param>
		/// <param name="file">The file to deserialize.</param>
		/// <returns>The deserialized instance.</returns>
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "value")]
		public static T DeserializeXML<T>(this T value, FileSystemInfo file)
		{
			return file.DeserializeXML<T>();
		}

		/// <summary>
		/// Deserializes the requested file to XML.
		/// </summary>
		/// <typeparam name="T">The type to deserialize.</typeparam>
		/// <param name="file">The file to deserialize.</param>
		/// <returns>The deserialized instance.</returns>
		public static T DeserializeXML<T>(this FileSystemInfo file)
		{
			object deserializedObject = null;

			if (file.Exists)
			{
				var serializer = new XmlSerializer(typeof(T));
				using (var stream = File.OpenRead(file.FullName))
				{
					deserializedObject = serializer.Deserialize(stream);
				}
			}

			return deserializedObject is T ? (T)deserializedObject : default(T);
		}
	}
}
