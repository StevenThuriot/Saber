using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Saber.WPF.Extensions
{
	/// <summary>
	/// Extension methods for Dependency Objects.
	/// </summary>
	public static class DependencyObjectExtensions
	{
		/// <summary>
		/// Disposes the visual tree.
		/// </summary>
		/// <param name="dependencyObject">The dependency object.</param>
		public static void DisposeVisualTree(this DependencyObject dependencyObject)
		{
			foreach (var disposable in dependencyObject.IterateVisualTreeRecursive()
													   .OfType<IDisposable>())
			{
				disposable.Dispose();
			}
		}
		/// <summary>
		/// Disposes the logical tree.
		/// </summary>
		/// <param name="dependencyObject">The dependency object.</param>
		public static void DisposeLogicalTree(this DependencyObject dependencyObject)
		{
			foreach (var disposable in dependencyObject.IterateLogicalTreeRecursive()
													   .OfType<IDisposable>())
			{
				disposable.Dispose();
			}
		}

		/// <summary>
		/// Iterates the visual tree. (One level)
		/// </summary>
		/// <param name="dependencyObject">The dependency object.</param>
		/// <returns></returns>
		public static IEnumerable<DependencyObject> IterateVisualTree(this DependencyObject dependencyObject)
		{
			var childrenCount = VisualTreeHelper.GetChildrenCount(dependencyObject);

			for (var i = 0; i < childrenCount; i++)
			{
				var child = VisualTreeHelper.GetChild(dependencyObject, i);

				if (child != null)
					yield return child;
			}
		}

		/// <summary>
		/// Iterates the logical tree. (One level)
		/// </summary>
		/// <param name="dependencyObject">The dependency object.</param>
		/// <returns></returns>
		public static IEnumerable<DependencyObject> IterateLogicalTree(this DependencyObject dependencyObject)
		{
			return LogicalTreeHelper.GetChildren(dependencyObject)
									.OfType<DependencyObject>()
									.Where(x => x != null);
		}

		/// <summary>
		/// Iterates the visual tree recursively. (All levels)
		/// </summary>
		/// <param name="dependencyObject">The dependency object.</param>
		/// <returns></returns>
		public static IEnumerable<DependencyObject> IterateVisualTreeRecursive(this DependencyObject dependencyObject)
		{
			foreach (var child in dependencyObject.IterateVisualTree())
			{
				foreach (var subChild in child.IterateVisualTreeRecursive())
				{
					yield return subChild;
				}

				yield return child;
			}
		}

		/// <summary>
		/// Iterates the logical tree recursively. (All levels)
		/// </summary>
		/// <param name="dependencyObject">The dependency object.</param>
		/// <returns></returns>
		public static IEnumerable<DependencyObject> IterateLogicalTreeRecursive(this DependencyObject dependencyObject)
		{
			foreach (var child in dependencyObject.IterateLogicalTree())
			{
				foreach (var subChild in child.IterateLogicalTreeRecursive())
				{
					yield return subChild;
				}

				yield return child;
			}
		}
	}
}
