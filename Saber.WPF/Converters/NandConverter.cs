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
using System.Windows.Data;

namespace Saber.WPF.Converters
{
	/// <summary>
	/// Converter used to perform a NOT AND operation on several booleans.
	/// </summary>
	[ValueConversion(typeof(bool), typeof(bool))]
	public class NandConverter : IMultiValueConverter
	{
		private readonly AndConverter _AndConverter;

		/// <summary>
		/// Initializes a new instance of the <see cref="NandConverter"/> class.
		/// </summary>
		public NandConverter()
		{
			_AndConverter = new AndConverter();
		}

		/// <summary>
		/// Converts source values to a value for the binding target. The data binding engine calls this method when it propagates the values from source bindings to the binding target.
		/// </summary>
		/// <param name="values">The array of values that the source bindings in the <see cref="T:System.Windows.Data.MultiBinding"/> produces. The value <see cref="F:System.Windows.DependencyProperty.UnsetValue"/> indicates that the source binding has no value to provide for conversion.</param>
		/// <param name="targetType">The type of the binding target property.</param>
		/// <param name="parameter">The converter parameter to use.</param>
		/// <param name="culture">The culture to use in the converter.</param>
		/// <returns>
		/// A converted value.
		/// If the method returns null, the valid null value is used.
		/// A return value of <see cref="T:System.Windows.DependencyProperty"/>.<see cref="F:System.Windows.DependencyProperty.UnsetValue"/> indicates that the converter did not produce a value, and that the binding will use the <see cref="P:System.Windows.Data.BindingBase.FallbackValue"/> if it is available, or else will use the default value.
		/// A return value of <see cref="T:System.Windows.Data.Binding"/>.<see cref="F:System.Windows.Data.Binding.DoNothing"/> indicates that the binding does not transfer the value or use the <see cref="P:System.Windows.Data.BindingBase.FallbackValue"/> or the default value.
		/// </returns>
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			var convertedValue = _AndConverter.Convert(values, targetType, parameter, culture);
			var convertedBool = convertedValue is bool && (bool) convertedValue;

			return !convertedBool;
		}

		/// <summary>
		/// Converts a binding target value to the source binding values.
		/// </summary>
		/// <param name="value">The value that the binding target produces.</param>
		/// <param name="targetTypes">The array of types to convert to. The array length indicates the number and types of values that are suggested for the method to return.</param>
		/// <param name="parameter">The converter parameter to use.</param>
		/// <param name="culture">The culture to use in the converter.</param>
		/// <returns>
		/// An array of values that have been converted from the target value back to the source values.
		/// </returns>
		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			return new[] {Binding.DoNothing};
		}
	}
}
