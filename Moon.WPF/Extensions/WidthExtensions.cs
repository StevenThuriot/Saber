using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Moon.WPF.Extensions
{
	/// <summary>
	/// Extension method for calculating width for controls.
	/// </summary>
	public static class WidthExtensions
	{
		/// <summary>
		/// Calculates the desired width for the text in the given control.
		/// </summary>
		/// <param name="textbox">The control.</param>
		/// <returns>A double representing the desired width for the given text.</returns>
		public static double CalculateWidth(this TextBox textbox)
		{
			return CalculateWidth(textbox.Text, textbox.FontSize, textbox.FontFamily, textbox.FontWeight, textbox.FontStyle, textbox.FontStretch, textbox.FlowDirection, textbox.Foreground);
		}

		/// <summary>
		/// Calculates the desired width for the text in the given control.
		/// </summary>
		/// <param name="textblock">The control.</param>
		/// <returns>A double representing the desired width for the given text.</returns>
		public static double CalculateWidth(this TextBlock textblock)
		{
			return CalculateWidth(textblock.Text, textblock.FontSize, textblock.FontFamily, textblock.FontWeight, textblock.FontStyle, textblock.FontStretch, textblock.FlowDirection, textblock.Foreground);
		}

		/// <summary>
		/// Calculates the desired width for the text in the given control.
		/// </summary>
		/// <param name="selector">The control.</param>
		/// <returns>A double representing the desired width for the given text.</returns>
		public static double CalculateWidth(this Selector selector)
		{
			var text = Convert.ToString(selector.SelectedValue);
			return CalculateWidth(text, selector.FontSize, selector.FontFamily, selector.FontWeight, selector.FontStyle, selector.FontStretch, selector.FlowDirection, selector.Foreground);
		}


		/// <summary>
		/// Calculates the desired width for the given text.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <param name="fontSize">The used fontsize.</param>
		/// <param name="fontFamily">The used font family.</param>
		/// <param name="fontWeight">The used font weight.</param>
		/// <param name="fontStyle">The used font style.</param>
		/// <param name="fontStretch">The used font stretch.</param>
		/// <param name="flowDirection">The used flow direction.</param>
		/// <param name="foreground">The used foreground brush.</param>
		/// <returns>A double representing the desired width for the given text.</returns>
		public static double CalculateWidth(this string text, double fontSize, FontFamily fontFamily, FontWeight fontWeight, FontStyle fontStyle, FontStretch fontStretch, FlowDirection flowDirection, Brush foreground)
		{
			var typeface = new Typeface(fontFamily, fontStyle, fontWeight, fontStretch);

			var formattedText = new FormattedText
									(
										text,
										Thread.CurrentThread.CurrentUICulture,
										flowDirection,
										typeface,
										fontSize,
										foreground
									)
			{
				Trimming = TextTrimming.CharacterEllipsis
			};


			return formattedText.Width;
		}
	}
}
