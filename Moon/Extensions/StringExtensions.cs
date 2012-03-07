﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using Moon.Helpers;

namespace Moon.Extensions
{
	/// <summary>
	/// Extension methods on strings
	/// </summary>
	public static class StringExtensions
	{
		/// <summary>
		/// Replaces the format item in a specified System.String with the text equivalent of the value of a corresponding System.Object instance in a specified array,
		/// using the Moon Framework Culture Info (Moon.Culture).
		/// </summary>
		/// <param name="value">The string to format.</param>
		/// <param name="arguments">The arguments to insert into the string.</param>
		/// <returns>The formatted string.</returns>
		public static string FormatWith(this string value, params string[] arguments)
		{
			return string.Format(CultureInfo.CurrentCulture, value, arguments);
		}

		/// <summary>
		/// Gets the value of the string.
		/// In case the value is null, empty or whitespace, it returns null.
		/// If not, it returns the actual value.
		/// </summary>
		/// <param name="value">The value you need.</param>
		/// <returns>The value or null</returns>
		public static string GetValueOrDefault(this string value)
		{
			return value.GetValueOrDefault(null);
		}

		/// <summary>
		/// Gets the value of the string.
		/// In case the value is null, empty or whitespace, it returns "defaultValue".
		/// If not, it returns the actual value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="defaultValue">The value to return in case of null, empty or whitespace.</param>
		/// <returns>The value or defaultValue</returns>
		public static string GetValueOrDefault(this string value, string defaultValue)
		{
			return value.IsNullOrWhiteSpace() ? defaultValue : value;
		}

		/// <summary>
		/// Checks a string for null or white space.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>True if null, empty or whitespace. False if not.</returns>
		public static bool IsNullOrWhiteSpace(this string value)
		{
			return value == null || value.All(char.IsWhiteSpace);
		}

		///<summary>
		/// Checks if the string is a valid e-mail address.
		///</summary>
		///<param name="value">The e-mail address.</param>
		///<returns>True if valid</returns>
		public static bool IsValidEmailAddress(this string value)
		{
			return Guard.ValidEmailRegex.IsMatch(value);
		}

		/// <summary>
		/// Takes characters from the left side of the string, with a maximum of string.length.
		/// </summary>
		/// <param name="value">The string to take characters from.</param>
		/// <param name="length">The amount of characters to take.</param>
		/// <returns>A new string with the specified length.</returns>
		public static string Left(this string value, int length)
		{
			Guard.StrictPositive(length);
			return value == null ? null : value.Substring(0, Math.Min(length, value.Length));
		}

		/// <summary>
		/// Takes characters from the right side of the string.
		/// </summary>
		/// <param name="value">The string to take characters from.</param>
		/// <param name="length">The amount of characters to take.</param>
		/// <returns>A new string with the specified length.</returns>
		public static string Right(this string value, int length)
		{
			Guard.StrictPositive(length);
			return value == null ? null : value.Substring(Math.Max(value.Length - length, 0));
		}

		///<summary>
		/// Reverses the given string.
		///</summary>
		///<param name="value">The string to reverse.</param>
		///<returns>The reversed string.</returns>
		public static string Reverse(this string value)
		{
			char[] chars = value.ToCharArray();
			Array.Reverse(chars);
			return new String(chars);
		}

		///<summary>
		/// Turns a string into a title cased one.
		///</summary>
		///<param name="value">The string that needs to be title cased.</param>
		///<returns>The title cased string</returns>
		[SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
		public static string ToTitleCase(this string value)
		{
			return value.IsNullOrWhiteSpace()
			       	? value
			       	: CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLowerInvariant());
		}

		/// <summary>
		/// Convert the string to an enum of the given type
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="target">The target.</param>
		/// <param name="defaultValue">The default value.</param>
		/// <returns>An enumeration of type T</returns>
		public static T ToEnum<T>(this string target, T defaultValue) where T : IComparable, IFormattable
		{
			T convertedValue = defaultValue;

			if (!string.IsNullOrEmpty(target))
			{
				try
				{
					convertedValue = (T) Enum.Parse(typeof (T), target.Trim(), true);
				}
				catch (ArgumentException)
				{
				}
			}

			return convertedValue;
		}

		/// <summary>
		/// Gets the trimmed value of the string.
		/// In case this string is null, empty or whitespace, it will return null.
		/// If not, it will return the trimmed value.
		/// </summary>
		/// <param name="value">The string to trim safely.</param>
		/// <returns>The trimmed string or null.</returns>
		public static string TrimSafe(this string value)
		{
			return value.TrimSafe(' ');
		}

		/// <summary>
		/// Gets the trimmed value of the string.
		/// In case this string is null, empty or whitespace, it will return null.
		/// If not, it will return the trimmed value.
		/// </summary>
		/// <param name="value">The string to trim safely.</param>
		/// <param name="trimCharacter">The character to trim</param>
		/// <returns>The trimmed string or null.</returns>
		public static string TrimSafe(this string value, char trimCharacter)
		{
			return value.TrimSafe(null, trimCharacter);
		}

		/// <summary>
		/// Gets the trimmed value of the string.
		/// In case this string is null, empty or whitespace, it will return "defaultValue".
		/// If not, it will return the trimmed value.
		/// </summary>
		/// <param name="value">The string to trim safely.</param>
		/// <param name="defaultValue">The default value to return</param>
		/// <returns>The trimmed string or null.</returns>
		public static string TrimSafe(this string value, string defaultValue)
		{
			return value.TrimSafe(defaultValue, ' ');
		}

		/// <summary>
		/// Gets the trimmed value of the string.
		/// In case this string is null, empty or whitespace, it will return "defaultValue".
		/// If not, it will return the trimmed value.
		/// </summary>
		/// <param name="value">The string to trim safely.</param>
		/// <param name="defaultValue">The default value to return</param>
		/// <param name="trimCharacter">The character to trim</param>
		/// <returns>The trimmed string or null.</returns>
		public static string TrimSafe(this string value, string defaultValue, char trimCharacter)
		{
			return value == null ? defaultValue : value.Trim(trimCharacter);
		}
	}
}