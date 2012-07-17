using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Saber.WPF.Converters
{
	///<summary>
	/// Converter used to append multiple strings into one.
	///</summary>
	[ValueConversion(typeof(string), typeof(string))]
	public class AppendConverter : IValueConverter, IMultiValueConverter
	{
		///<summary>
		/// Ctor
		///</summary>
		public AppendConverter()
		{
			AppendBefore = false;
			StripSpaces = false;
			Delimiter = string.Empty;
		}

		/// <summary>
		/// If true, the converter will append the parameter value before the binded object. If false, it will be after.
		/// </summary>
		public bool AppendBefore { get; set; }

		/// <summary>
		/// If true, the string will be stripped of spaces before being returned.
		/// </summary>
		public bool StripSpaces { get; set; }

		/// <summary>
		/// The delimiter used when merging the strings.
		/// </summary>
		public string Delimiter { get; set; }

		private static string Strip(string value)
		{
			return value.Replace(" ", string.Empty);
		}

		/// <summary>
		/// Converts a value. 
		/// </summary>
		/// <returns>
		/// A converted value. If the method returns null, the valid null value is used.
		/// </returns>
		/// <param name="value">The value produced by the binding source.
		///                 </param><param name="targetType">The type of the binding target property.
		///                 </param><param name="parameter">The converter parameter to use.
		///                 </param><param name="culture">The culture to use in the converter.
		///                 </param>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string valueAsString = System.Convert.ToString(value, culture);
			string parameterAsString = System.Convert.ToString(parameter, culture);

			if (StripSpaces)
			{
				valueAsString = Strip(valueAsString);
				parameterAsString = Strip(parameterAsString);
			}

			var returnValue = AppendBefore
								  ? string.Concat(parameterAsString, Delimiter, valueAsString)
								  : string.Concat(valueAsString, Delimiter, parameterAsString);

			return returnValue;
		}

		/// <summary>
		/// Converts a value. 
		/// </summary>
		/// <returns>
		/// A converted value. If the method returns null, the valid null value is used.
		/// </returns>
		/// <param name="value">The value that is produced by the binding target.
		///                 </param><param name="targetType">The type to convert to.
		///                 </param><param name="parameter">The converter parameter to use.
		///                 </param><param name="culture">The culture to use in the converter.
		///                 </param>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return Binding.DoNothing;
		}

		/// <summary>
		/// Converts source values to a value for the binding target. The data binding engine calls this method when it propagates the values from source bindings to the binding target.
		/// </summary>
		/// <returns>
		/// A converted value.
		///                     If the method returns null, the valid null value is used.
		///                     A return value of <see cref="T:System.Windows.DependencyProperty"/>.<see cref="F:System.Windows.DependencyProperty.UnsetValue"/> indicates that the converter did not produce a value, and that the binding will use the <see cref="P:System.Windows.Data.BindingBase.FallbackValue"/> if it is available, or else will use the default value.
		///                     A return value of <see cref="T:System.Windows.Data.Binding"/>.<see cref="F:System.Windows.Data.Binding.DoNothing"/> indicates that the binding does not transfer the value or use the <see cref="P:System.Windows.Data.BindingBase.FallbackValue"/> or the default value.
		/// </returns>
		/// <param name="values">The array of values that the source bindings in the <see cref="T:System.Windows.Data.MultiBinding"/> produces. The value <see cref="F:System.Windows.DependencyProperty.UnsetValue"/> indicates that the source binding has no value to provide for conversion.
		///                 </param><param name="targetType">The type of the binding target property.
		///                 </param><param name="parameter">The converter parameter to use.
		///                 </param><param name="culture">The culture to use in the converter.
		///                 </param>
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			if (values == null || values.Length == 0)
				return string.Empty;

			var enumerator =
				values.Where(x => x != null)
					  .Select(System.Convert.ToString);

			if (StripSpaces)
			{
				enumerator = enumerator.Select(Strip);
			}

			if (AppendBefore)
			{
				enumerator = enumerator.Reverse();
			}

			var returnValue = string.Join(Delimiter, enumerator.ToArray());

			return returnValue;
		}

		/// <summary>
		/// Converts a binding target value to the source binding values.
		/// </summary>
		/// <returns>
		/// An array of values that have been converted from the target value back to the source values.
		/// </returns>
		/// <param name="value">The value that the binding target produces.
		///                 </param><param name="targetTypes">The array of types to convert to. The array length indicates the number and types of values that are suggested for the method to return.
		///                 </param><param name="parameter">The converter parameter to use.
		///                 </param><param name="culture">The culture to use in the converter.
		///                 </param>
		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			return new[] { Binding.DoNothing };
		}
	}
}
