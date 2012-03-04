using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace Moon.WPF.Extensions
{
    /// <summary>
    /// Extension methods on lists
    /// </summary>
    public static class EnumerableExtensions
    {
    	/// <summary>
    	/// Turns any list into an observable collection.
    	/// </summary>
    	/// <param name="source">The source list.</param>
    	/// <typeparam name="T">The type of items contained in the list.</typeparam>
    	/// <returns>An observable collection.</returns>
		[SuppressMessage("Microsoft.Portability", "CA1903:UseOnlyApiFromTargetedFramework", 
							MessageId = "System.Collections.ObjectModel.ObservableCollection`1<!!0>.#.ctor(System.Collections.Generic.IEnumerable`1<!!0>)",
							Justification = "This dll will only be used by WPF applications, which know of Observable Collections.")]
		public static ObservableCollection<T> ToObservable<T>(this IEnumerable<T> source)
    	{
    		return new ObservableCollection<T>(source);
    	}
    }
}
