using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace Saber.Helpers
{
	/// <summary>
	/// Helper to make serializing objects easier.
	/// </summary>
	[Serializable]
	public static class Serializer
	{
		/// <summary>
		/// Serializes the passed value.
		/// </summary>
		/// <typeparam name="T">The type to serialize.</typeparam>
		/// <param name="value">The value to serialize.</param>
		/// <param name="file">Where to write the serialized instance.</param>
		public static void Serialize<T>(T value, FileInfo file)
		{
			Guard.NotNull(value, file);
			Guard.Serializable(typeof(T));

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
		/// <param name="file">The file to deserialize.</param>
		/// <returns>The deserialized instance.</returns>
		public static T Deserialize<T>(FileInfo file)
		{
			Guard.NotNull(file);
			Guard.Serializable(typeof(T));

			object deserializedObject = null;

			if (file.Exists)
			{
				using (var stream = File.OpenRead(file.FullName))
				{
					var formatter = new BinaryFormatter();
					deserializedObject = formatter.Deserialize(stream);
				}
			}

			return deserializedObject is T ? (T) deserializedObject : default(T);
		}

		/// <summary>
		/// Serializes the passed value to XML.
		/// </summary>
		/// <typeparam name="T">The type to serialize.</typeparam>
		/// <param name="value">The value to serialize.</param>
		/// <param name="file">Where to write the serialized instance.</param>
		public static void SerializeXml<T>(T value, FileInfo file)
		{
			Guard.NotNull(value, file);

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
		/// <param name="file">The file to deserialize.</param>
		/// <returns>The deserialized instance.</returns>
		public static T DeserializeXML<T>(FileInfo file)
		{
			Guard.NotNull(file);

			object deserializedObject = null;

			if (file.Exists)
			{
				var serializer = new XmlSerializer(typeof (T));
				using (var stream = File.OpenRead(file.FullName))
				{
					deserializedObject = serializer.Deserialize(stream);
				}
			}

			return deserializedObject is T ? (T) deserializedObject : default(T);
		}
	}
}
