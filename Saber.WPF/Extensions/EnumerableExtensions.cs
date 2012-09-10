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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace Saber.WPF.Extensions
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
