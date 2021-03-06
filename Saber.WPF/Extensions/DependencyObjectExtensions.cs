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

		/// <summary>
		/// Gets the child of a given type.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static T GetChild<T>(this DependencyObject value)
			where T : DependencyObject
		{
			if (value != null)
			{
				var castedValue = value as T;
				if (castedValue != null) return castedValue;

				var childrenCount = VisualTreeHelper.GetChildrenCount(value);

				for (int i = 0; i < childrenCount; i++)
				{
					var child = VisualTreeHelper.GetChild(value, i);
					var result = GetChild<T>(child);
					if (result != null)
					{
						return result;
					}
				}
			}

			return null;
		}

		/// <summary>
		/// Gets the child with the given name.
		/// </summary>
		/// <typeparam name="T">The type to return.</typeparam>
		/// <param name="value">The value.</param>
		/// <param name="name">The name.</param>
		/// <returns></returns>
		public static T GetChild<T>(DependencyObject value, string name)
			where T : DependencyObject
		{
			if (value != null)
			{
				var objectName = value.GetValue(FrameworkElement.NameProperty) as string;
				if (objectName == name) return value as T;

				var childrenCount = VisualTreeHelper.GetChildrenCount(value);

				for (int i = 0; i < childrenCount; i++)
				{
					var child = VisualTreeHelper.GetChild(value, i);
					var result = GetChild<T>(child, name);
					if (result != null)
					{
						return result;
					}
				}
			}

			return null;
		}

		/// <summary>
		/// Gets the children with the given interfaces.
		/// </summary>
		/// <param name="parent">The parent.</param>
		/// <returns></returns>
		public static List<T> GetChildrenWithInterfaces<T>(DependencyObject parent)
		{
			return GetChildrenWithInterfaces(parent, typeof(T)).OfType<T>().ToList();
		}

		/// <summary>
		/// Gets the children with the given interfaces.
		/// </summary>
		/// <param name="parent">The parent.</param>
		/// <param name="types">The types.</param>
		/// <returns></returns>
		public static List<DependencyObject> GetChildrenWithInterfaces(DependencyObject parent, params Type[] types)
		{
			var returnValues = new List<DependencyObject>();
			int childCount = VisualTreeHelper.GetChildrenCount(parent);

			for (int i = 0; i < childCount; i++)
			{
				var child = VisualTreeHelper.GetChild(parent, i);
				var interfaces = child.GetType().GetInterfaces();

				if (types.Any(interfaces.Contains))
				{
					returnValues.Add(child);
				}

				var childrenWithInterface = GetChildrenWithInterfaces(child, types);
				returnValues.AddRange(childrenWithInterface);
			}

			return returnValues;
		}

		/// <summary>
		/// Gets the parent.
		/// </summary>
		/// <param name="d">The d.</param>
		/// <param name="t">The t.</param>
		/// <returns></returns>
		public static DependencyObject GetParent(DependencyObject d, Type t)
		{
			while (d != null)
			{
				if (t.IsInstanceOfType(d))
				{
					break;
				}

				d = VisualTreeHelper.GetParent(d);
			}

			return d;
		}

		/// <summary>
		/// Gets the parent.
		/// </summary>
		/// <param name="d">The d.</param>
		/// <param name="maxLevel">The max level.</param>
		/// <param name="types">The types.</param>
		/// <returns></returns>
		public static DependencyObject GetParent(DependencyObject d, int maxLevel, params Type[] types)
		{
			var loop = 0;

			while (d != null && loop < maxLevel)
			{
				var result = from type in types
							 where type.IsInstanceOfType(d)
							 select type;

				if (result.Count() != 0)
				{
					break;
				}

				d = VisualTreeHelper.GetParent(d);

				loop++;
			}

			return (loop >= maxLevel) ? null : d;
		}
	}
}
