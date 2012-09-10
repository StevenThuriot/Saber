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
using System.Windows;
using System.Windows.Input;

namespace Saber.WPF.Extensions
{
	/// <summary>
	/// Extensions for UI Elements.
	/// </summary>
	public static class UIElementExtensions
	{
		/// <summary>
		/// Creates the input gesture.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <param name="key">The key to listen to.</param>
		/// <param name="modifierKeys">The modifier keys to listen to.</param>
		/// <param name="execute">The command to execute.</param>
		/// <param name="canExecute">The command to check if the main command can execute.</param>
		public static void CreateInputGesture(this UIElement element, Key key, ModifierKeys modifierKeys, ExecutedRoutedEventHandler execute, CanExecuteRoutedEventHandler canExecute)
		{
			var command = new RoutedUICommand();
			var binding = new CommandBinding(command, execute, canExecute);

			var gesture = new KeyGesture(key, modifierKeys);
			var keyBinding = new KeyBinding(command, gesture);

			element.CommandBindings.Add(binding);
			element.InputBindings.Add(keyBinding);
		}

		/// <summary>
		/// Translates a point with coordinates (0,0) relative to this element to coordinates that are relative to the specified element.
		/// </summary>
		/// <param name="element">The point value, as relative to this element.</param>
		/// <param name="relativeTo">The element to translate the point into.</param>
		/// <returns>A point value, now relative to the target element rather than this source element.</returns>
		public static Point TranslatePoint(this UIElement element, UIElement relativeTo)
		{
			var point = new Point();
			return element.TranslatePoint(point, relativeTo);
		}
	}
}
