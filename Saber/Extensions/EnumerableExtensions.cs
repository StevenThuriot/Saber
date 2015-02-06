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
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Saber.Domain;

namespace Saber.Extensions
{
	/// <summary>
	/// Extension methods on lists
	/// </summary>
	public static class EnumerableExtensions
	{
		///<summary>
		/// Returns the passed enumerable as a ReadOnlyCollection.
		///</summary>
		///<param name="enumerable">The enumeration of items.</param>
		///<typeparam name="T">The type of items the enumerable contains.</typeparam>
		///<returns>A ReadOnlyCollection of the passed enumerable.</returns>
		public static ReadOnlyCollection<T> AsReadOnly<T>(this IEnumerable<T> enumerable)
		{
			return enumerable.ToList().AsReadOnly();
		}

		/// <summary>
		/// Adds the item in case it's not already present in the list using the default equality comparer.
		/// </summary>
		/// <typeparam name="T">The type of item to add.</typeparam>
		/// <param name="enumerable">The enumerable to add it to.</param>
		/// <param name="value">The value to add.</param>
		public static void AddUnique<T>(this ICollection<T> enumerable, T value)
		{
			enumerable.AddUnique(value, EqualityComparer<T>.Default);
		}

		/// <summary>
		/// Adds the item in case it's not already present in the list.
		/// </summary>
		/// <typeparam name="T">The type of item to add.</typeparam>
		/// <param name="enumerable">The enumerable to add it to.</param>
		/// <param name="value">The value to add.</param>
		/// <param name="comparer">The comparer to use.</param>
		public static void AddUnique<T>(this ICollection<T> enumerable, T value, IEqualityComparer<T> comparer)
		{
			if (!enumerable.Contains(value, comparer))
			{
				enumerable.Add(value);
			}
		}

		/// <summary>
		/// Adds the passed items to the enumerable.
		/// </summary>
		/// <typeparam name="T">The type of item to add.</typeparam>
		/// <param name="enumerable">The enumerable to add it to.</param>
		/// <param name="values">The values to add.</param>
		public static void AddRange<T>(this ICollection<T> enumerable, IEnumerable<T> values)
		{
			if (values == null)
				return;

			foreach (var item in values)
			{
				enumerable.AddUnique(item);
			}
		}

        /// <summary>
        /// Appends a list to another one without ordering.
        /// </summary>
        /// <remarks>This resembles LINQ's union, except it doesn't distinct.</remarks>
        /// <param name="source">The first list.</param>
        /// <param name="additionals">The list to append.</param>
        /// <returns>An combined version of the two enumerables.</returns>
        public static IEnumerable Append(this IEnumerable source, IEnumerable additionals)
        {
            foreach (var item in source)
                yield return item;

            foreach (var item in additionals)
                yield return item;
        }

        /// <summary>
        /// Appends a list to another one without ordering.
        /// </summary>
        /// <remarks>This resembles LINQ's union, except it doesn't distinct.</remarks>
        /// <param name="source">The first list.</param>
        /// <param name="additionals">The list to append.</param>
        /// <typeparam name="T">The type of items in the list</typeparam>
        /// <returns>An combined version of the two enumerables.</returns>
        public static IEnumerable<T> Append<T>(this IEnumerable<T> source, IEnumerable<T> additionals)
        {
            foreach (var item in source)
                yield return item;

            foreach (var item in additionals)
                yield return item;
        }

        /// <summary>
        /// Flattens an enumerable and its children into a single list.
        /// </summary>
        /// <remarks>This is recursive unlike Delve</remarks>
        /// <param name="source">The parent enumerable.</param>
        /// <param name="selector">The child selector</param>
        /// <typeparam name="T">The type of items in the enumerables.</typeparam>
        /// <returns>A flat list</returns>
        public static IEnumerable<T> AsFlat<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> selector)
        {
            if (source == null) yield break;

            foreach (var entity in source)
            {
                yield return entity;

                foreach (var child in selector(entity).AsFlat(selector))
                    yield return child;
            }
        }

		/// <summary>
		/// Transforms array of structs into an array of objects.
		/// </summary>
		/// <typeparam name="T">The type of struct.</typeparam>
		/// <param name="enumerable">The arguments.</param>
		/// <returns>An array of objects.</returns>
		public static object[] BoxToArray<T>(this IEnumerable<T> enumerable)
			where T : struct
		{
			return enumerable == null
			       	? null
			       	: enumerable.Cast<object>().ToArray();
		}

		///<summary>
		///Determines whether the dictionary (where each value is a list of values) contains a specified value.
		///</summary>
		///<typeparam name="TKey">The type of keys in the dictionary.</typeparam>
		///<typeparam name="TValues">The type of the list of values in the dictionary.</typeparam>
		///<typeparam name="TValue">The type of the searched value.</typeparam>
		///<param name="dictionary">The dictionary.</param>
		///<param name="value">The searced value.</param>
		///<returns>
		///  <c>true</c> if the specified dictionary contains value; otherwise, <c>false</c>.
		///</returns>
		public static bool ContainsValue<TKey, TValues, TValue>(this IDictionary<TKey, TValues> dictionary, TValue value)
			where TValues : IEnumerable<TValue>
		{
			return dictionary.Values.SelectMany(x => x).Contains(value);
		}

		/// <summary>
		/// Completely consumes the given sequence. This method uses immediate execution,
		/// and doesn't store any data during execution.
		/// </summary>
		/// <typeparam name="T">Element type of the sequence</typeparam>
		/// <param name="source">Source to consume</param>
		[SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "element",
			Justification = "The point is to just iterate the list. We don't want to do anything with it.")]
		public static void Consume<T>(this IEnumerable<T> source)
		{
			if (source == null)
				return;

			foreach (var element in source) { }
		}

		/// <summary>
		/// Converts an IEnumerable to a type of choice.
		/// </summary>
		/// <typeparam name="TItem">The type of item.</typeparam>
		/// <typeparam name="TList">The type of list to convert to.</typeparam>
		/// <param name="enumerable">The original values.</param>
		/// <returns>A list of type TList, with all the values from the original list.</returns>
		public static TList ConvertTo<TItem, TList>(this IEnumerable<TItem> enumerable)
			where TList : ICollection<TItem>, new()
		{
			var list = new TList();

			if (enumerable != null)
			{
				foreach (var value in enumerable)
				{
					list.Add(value);
				}
			}

			return list;
		}

        /// <summary>
        /// Distinct, based on a key.
        /// </summary>
        /// <param name="source">The original enumerable</param>
        /// <param name="selector">The key used to distinct.</param>
        /// <typeparam name="T">The type of list.</typeparam>
        /// <typeparam name="TKey">The type of key</typeparam>
        /// <returns>A distinct version of the original source.</returns>
        public static IEnumerable<T> Distinct<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector)
        {
            var set = new HashSet<TKey>();

            return source.Where(x => set.Add(selector(x)))
                          //It's important that we call ToList, since the hash set will be used more than once otherwise, giving a false result.
                         .ToList();
        }

        /// <summary>
        /// Returns a list of all parents and delves into the selected children.
        /// </summary>
        /// <remarks>This is not recursive, unlike AsFlat</remarks>
        /// <param name="source">The original list</param>
        /// <param name="childrenSelector">Child selector</param>
        /// <typeparam name="T">The type of items in the list</typeparam>
        /// <returns>A single level flat list</returns>
        public static IEnumerable<T> Delve<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> childrenSelector)
        {
            if (source == null) yield break;
            foreach (var entity in source)
            {
                yield return entity;

                var children = childrenSelector(entity);
                if (children == null) continue;

                foreach (var child in children)
                    yield return child;
            }
        }

        /// <summary>
        /// Returns a list of the parent and delves into the selected children.
        /// </summary>
        /// <remarks>This is not recursive, unlike AsFlat</remarks>
        /// <param name="entity"></param>
        /// <param name="childrenSelector">Child selector</param>
        /// <typeparam name="T">The type of items in the list</typeparam>
        /// <returns>A single level flat list</returns>
        public static IEnumerable<T> Delve<T>(this T entity, Func<T, IEnumerable<T>> childrenSelector)
        {
            yield return entity;

            var children = childrenSelector(entity);
            if (children == null) yield break;

            foreach (var child in children)
                yield return child;
        }

		///<summary>
		/// Counts a list, keeping in mind the check you are planning to do on it. 
		/// This way you don't have to count every item when you don't need to, resulting in less overhead.
		///</summary>
		///<param name="enumerable">The list with items getting counted.</param>
		///<param name="comparePredicate">The comparison function.</param>
		///<param name="numberOfItems">The number of items that you are going to check for. The same number should be used in the predicate.</param>
		///<param name="wherePredicate">The filter predicate.</param>
		///<typeparam name="T">The type of item getting counted. This is of no relevance, just to keep the method generic.</typeparam>
		///<exception cref="OverflowException">An overflow exception will be thrown when 'numberOfItems' equals int.MaxValue</exception>
		///<returns>The result of the passed predicate.</returns>
		private static bool OptimizedCount<T>(IEnumerable<T> enumerable, int numberOfItems, Func<T, bool> wherePredicate,
		                                      Func<int, bool> comparePredicate)
		{
			if (enumerable == null || numberOfItems < 0) return false;

			if (wherePredicate != null)
			{
				enumerable = enumerable.Where(wherePredicate);
			}

			var numberOfItemsToCount = checked(numberOfItems + 1);
			var countedItems = 0;

			var enumerator = enumerable.GetEnumerator();
			while (enumerator.MoveNext() && countedItems < numberOfItemsToCount)
			{
				countedItems++;
			}

			var returnValue = comparePredicate(countedItems);

			return returnValue;
		}

		///<summary>
		/// Counts a list, keeping in mind the check you are planning to do on it. 
		/// This way you don't have to count every item when you don't need to, resulting in less overhead.
		///</summary>
		///<param name="enumerable">The list with items getting counted.</param>
		///<param name="numberOfItems">The number of items that you are going to check for. The same number should be used in the predicate.</param>
		///<typeparam name="T">The type of item getting counted. This is of no relevance, just to keep the method generic.</typeparam>
		///<exception cref="OverflowException">An overflow exception will be thrown when 'numberOfItems' equals int.MaxValue</exception>
		///<returns>True if the list contains the same amount of items as defined in 'numberOfItems'.</returns>
		public static bool CountEqualTo<T>(this IEnumerable<T> enumerable, int numberOfItems)
		{
			return CountEqualTo(enumerable, numberOfItems, null);
		}

		///<summary>
		/// Counts a list, keeping in mind the check you are planning to do on it. 
		/// This way you don't have to count every item when you don't need to, resulting in less overhead.
		///</summary>
		///<param name="enumerable">The list with items getting counted.</param>
		///<param name="numberOfItems">The number of items that you are going to check for. The same number should be used in the predicate.</param>
		///<param name="wherePredicate">The filter predicate to apply to the list.</param>
		///<typeparam name="T">The type of item getting counted. This is of no relevance, just to keep the method generic.</typeparam>
		///<exception cref="OverflowException">An overflow exception will be thrown when 'numberOfItems' equals int.MaxValue</exception>
		///<returns>True if the list contains the same amount of items as defined in 'numberOfItems'.</returns>
		public static bool CountEqualTo<T>(this IEnumerable<T> enumerable, int numberOfItems, Func<T, bool> wherePredicate)
		{
			return OptimizedCount(enumerable, numberOfItems, wherePredicate, x => x == numberOfItems);
		}

		///<summary>
		/// Counts a list, keeping in mind the check you are planning to do on it. 
		/// This way you don't have to count every item when you don't need to, resulting in less overhead.
		///</summary>
		///<param name="enumerable">The list with items getting counted.</param>
		///<param name="numberOfItems">The number of items that you are going to check for. The same number should be used in the predicate.</param>
		///<typeparam name="T">The type of item getting counted. This is of no relevance, just to keep the method generic.</typeparam>
		///<exception cref="OverflowException">An overflow exception will be thrown when 'numberOfItems' equals int.MaxValue</exception>
		///<returns>True if the list contains less items than defined in 'numberOfItems'.</returns>
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "CountLess")]
		public static bool CountLessThan<T>(this IEnumerable<T> enumerable, int numberOfItems)
		{
			return CountLessThan(enumerable, numberOfItems, null);
		}

		///<summary>
		/// Counts a list, keeping in mind the check you are planning to do on it. 
		/// This way you don't have to count every item when you don't need to, resulting in less overhead.
		///</summary>
		///<param name="enumerable">The list with items getting counted.</param>
		///<param name="numberOfItems">The number of items that you are going to check for. The same number should be used in the predicate.</param>
		///<param name="wherePredicate">The filter predicate to apply to the list.</param>
		///<typeparam name="T">The type of item getting counted. This is of no relevance, just to keep the method generic.</typeparam>
		///<exception cref="OverflowException">An overflow exception will be thrown when 'numberOfItems' equals int.MaxValue</exception>
		///<returns>True if the list contains less items than defined in 'numberOfItems'.</returns>
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "CountLess")]
		public static bool CountLessThan<T>(this IEnumerable<T> enumerable, int numberOfItems, Func<T, bool> wherePredicate)
		{
			return OptimizedCount(enumerable, numberOfItems, wherePredicate, x => x < numberOfItems);
		}

		///<summary>
		/// Counts a list, keeping in mind the check you are planning to do on it. 
		/// This way you don't have to count every item when you don't need to, resulting in less overhead.
		///</summary>
		///<param name="enumerable">The list with items getting counted.</param>
		///<param name="numberOfItems">The number of items that you are going to check for. The same number should be used in the predicate.</param>
		///<typeparam name="T">The type of item getting counted. This is of no relevance, just to keep the method generic.</typeparam>
		///<exception cref="OverflowException">An overflow exception will be thrown when 'numberOfItems' equals int.MaxValue</exception>
		///<returns>True if the list contains more items than defined in 'numberOfItems'.</returns>
		public static bool CountGreaterThan<T>(this IEnumerable<T> enumerable, int numberOfItems)
		{
			return CountGreaterThan(enumerable, numberOfItems, null);
		}

		///<summary>
		/// Counts a list, keeping in mind the check you are planning to do on it. 
		/// This way you don't have to count every item when you don't need to, resulting in less overhead.
		///</summary>
		///<param name="enumerable">The list with items getting counted.</param>
		///<param name="numberOfItems">The number of items that you are going to check for. The same number should be used in the predicate.</param>
		///<param name="wherePredicate">The filter predicate to apply to the list.</param>
		///<typeparam name="T">The type of item getting counted. This is of no relevance, just to keep the method generic.</typeparam>
		///<exception cref="OverflowException">An overflow exception will be thrown when 'numberOfItems' equals int.MaxValue</exception>
		///<returns>True if the list contains more items than defined in 'numberOfItems'.</returns>
		public static bool CountGreaterThan<T>(this IEnumerable<T> enumerable, int numberOfItems, Func<T, bool> wherePredicate)
		{
			return OptimizedCount(enumerable, numberOfItems, wherePredicate, x => x > numberOfItems);
		}

		///<summary>
		/// Counts a list, keeping in mind the check you are planning to do on it. 
		/// This way you don't have to count every item when you don't need to, resulting in less overhead.
		///</summary>
		///<param name="enumerable">The list with items getting counted.</param>
		///<param name="numberOfItems">The number of items that you are going to check for. The same number should be used in the predicate.</param>
		///<typeparam name="T">The type of item getting counted. This is of no relevance, just to keep the method generic.</typeparam>
		///<exception cref="OverflowException">An overflow exception will be thrown when 'numberOfItems' equals int.MaxValue</exception>
		///<returns>True if the list contains less or the same amount of items as defined in 'numberOfItems'.</returns>
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "CountLess")]
		public static bool CountLessOrEqualTo<T>(this IEnumerable<T> enumerable, int numberOfItems)
		{
			return CountLessOrEqualTo(enumerable, numberOfItems, null);
		}

		///<summary>
		/// Counts a list, keeping in mind the check you are planning to do on it. 
		/// This way you don't have to count every item when you don't need to, resulting in less overhead.
		///</summary>
		///<param name="enumerable">The list with items getting counted.</param>
		///<param name="numberOfItems">The number of items that you are going to check for. The same number should be used in the predicate.</param>
		///<param name="wherePredicate">The filter predicate to apply to the list.</param>
		///<typeparam name="T">The type of item getting counted. This is of no relevance, just to keep the method generic.</typeparam>
		///<exception cref="OverflowException">An overflow exception will be thrown when 'numberOfItems' equals int.MaxValue</exception>
		///<returns>True if the list contains less or the same amount of items as defined in 'numberOfItems'.</returns>
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "CountLess")]
		public static bool CountLessOrEqualTo<T>(this IEnumerable<T> enumerable, int numberOfItems,
		                                         Func<T, bool> wherePredicate)
		{
			return OptimizedCount(enumerable, numberOfItems, wherePredicate, x => x <= numberOfItems);
		}

		///<summary>
		/// Counts a list, keeping in mind the check you are planning to do on it. 
		/// This way you don't have to count every item when you don't need to, resulting in less overhead.
		///</summary>
		///<param name="enumerable">The list with items getting counted.</param>
		///<param name="numberOfItems">The number of items that you are going to check for. The same number should be used in the predicate.</param>
		///<typeparam name="T">The type of item getting counted. This is of no relevance, just to keep the method generic.</typeparam>
		///<exception cref="OverflowException">An overflow exception will be thrown when 'numberOfItems' equals int.MaxValue</exception>
		///<returns>True if the list contains more or the same amount of items as defined in 'numberOfItems'.</returns>
		public static bool CountGreaterOrEqualTo<T>(this IEnumerable<T> enumerable, int numberOfItems)
		{
			return CountGreaterOrEqualTo(enumerable, numberOfItems, null);
		}

		///<summary>
		/// Counts a list, keeping in mind the check you are planning to do on it. 
		/// This way you don't have to count every item when you don't need to, resulting in less overhead.
		///</summary>
		///<param name="enumerable">The list with items getting counted.</param>
		///<param name="numberOfItems">The number of items that you are going to check for. The same number should be used in the predicate.</param>
		///<param name="wherePredicate">The filter predicate to apply to the list.</param>
		///<typeparam name="T">The type of item getting counted. This is of no relevance, just to keep the method generic.</typeparam>
		///<exception cref="OverflowException">An overflow exception will be thrown when 'numberOfItems' equals int.MaxValue</exception>
		///<returns>True if the list contains more or the same amount of items as defined in 'numberOfItems'.</returns>
		public static bool CountGreaterOrEqualTo<T>(this IEnumerable<T> enumerable, int numberOfItems,
		                                            Func<T, bool> wherePredicate)
		{
			return OptimizedCount(enumerable, numberOfItems, wherePredicate, x => x >= numberOfItems);
		}

        /// <summary>
        /// Groups the adjacent elements of a sequence according to a specified key selector function.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="keySelector">The key selector.</param>
        /// <returns></returns>
        public static IEnumerable<IGrouping<TKey, TSource>> GroupAdjacent<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return GroupAdjacent(source, keySelector, EqualityComparer<TKey>.Default);
        }

        /// <summary>
        /// Groups the adjacent elements of a sequence according to a specified key selector function.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="keySelector">The key selector.</param>
        /// <param name="comparer">The comparer.</param>
        /// <returns></returns>
        public static IEnumerable<IGrouping<TKey, TSource>> GroupAdjacent<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
        {
            return new GroupedAdjacentEnumerable<TSource, TKey>(source, keySelector, comparer);
        }

		///<summary>
		/// Uses the most optimized way of checking if a list of items is empty or not.
		///</summary>
		///<param name="collection">The collection to check</param>
		///<typeparam name="T">The type of items in the collection</typeparam>
		///<returns>True if not empty</returns>
		public static bool IsNullOrEmpty<T>(this ICollection<T> collection)
		{
			return collection == null || collection.Count == 0;
		}

		/// <summary>
		/// Uses the most optimized way of checking if a list of items is empty or not.
		/// </summary>
		///<param name="array">The array to check</param>
		///<typeparam name="T">The type of items in the array</typeparam>
		///<returns>True if not empty</returns>
		public static bool IsNullOrEmpty<T>(this T[] array)
		{
			return array == null || array.Length == 0;
		}

		/// <summary>
		/// Uses the most optimized way of checking if a list of items is empty or not.
		/// </summary>
		///<param name="enumerable">The enumeration to check</param>
		///<typeparam name="T">The type of items in the enumeration</typeparam>
		///<returns>True if not empty</returns>
		public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
		{
			return enumerable == null || !enumerable.Any();
		}

		/// <summary>
		/// Selects the maximum from a list after applying a selector.
		/// </summary>
		/// <typeparam name="TSource">The source type.</typeparam>
		/// <typeparam name="TResult">The result type.</typeparam>
		/// <param name="enumerable">The original list.</param>
		/// <param name="selector">The selector used to select items from the list.</param>
		/// <returns>The maximum from the selected items.</returns>
		public static TResult Max<TSource, TResult>(this IEnumerable<TSource> enumerable, Func<TSource, TResult> selector)
		{
			return enumerable.Select(selector).Max();
		}

		/// <summary>
		/// Performs the passed action on each item in the enumeration.
		/// </summary>
		/// <typeparam name="T">The type of items the enumeration contains.</typeparam>
		/// <param name="enumerable">The enumeration.</param>
		/// <param name="action">The action.</param>
		/// <returns>The enumeration.</returns>
		public static IEnumerable<T> ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
		{
			if (enumerable != null)
			{
				foreach (var item in enumerable)
				{
					action(item);
				}
			}

			return enumerable;
		}

		/// <summary>
		/// Skips items from the input sequence until the given predicate returns true
		/// when applied to the current source item; that item will be the last skipped.
		/// </summary>
		/// <typeparam name="TSource">Type of the source sequence</typeparam>
		/// <param name="source">Source sequence</param>
		/// <param name="predicate">Predicate used to determine when to stop yielding results from the source.</param>
		/// <returns>Items from the source sequence after the predicate first returns true when applied to the item.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="predicate"/> is null</exception>
		public static IEnumerable<TSource> SkipUntil<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			using (var iterator = source.GetEnumerator())
			{
				while (iterator.MoveNext())
				{
					if (predicate(iterator.Current))
					{
						break;
					}
				}
				while (iterator.MoveNext())
				{
					yield return iterator.Current;
				}
			}
		}

		///<summary>
		/// Strips null values from the enumerable.
		///</summary>
		///<param name="enumerable">The enumeration.</param>
		///<typeparam name="T">The type of items the enumeration contains. This has to be a class.</typeparam>
		///<returns>An enumerable stripped of all null-values.</returns>
		public static IEnumerable<T> StripNulls<T>(this IEnumerable<T> enumerable) where T : class
		{
			return enumerable == null
			       	? null
			       	: enumerable.Where(x => x != null);
		}
		
		/// <summary>
		/// Select all items from several lists.
		/// </summary>
		/// <typeparam name="TSource">The source type.</typeparam>
		/// <typeparam name="TResult">The result type.</typeparam>
		/// <param name="source">The source list.</param>
		/// <returns>An enumerable that contains all items from the passed lists.</returns>
		public static IEnumerable<TResult> SelectMany<TSource, TResult>(this IEnumerable<TSource> source)
		   where TSource : IEnumerable<TResult>
		{
			return source.SelectMany(x => x);
		}

		/// <summary>
		/// Returns every N-th element of a source sequence.
		/// </summary>
		/// <typeparam name="T">Type of the source sequence</typeparam>
		/// <param name="source">Source sequence</param>
		/// <param name="step">Number of elements to bypass before returning the next element.</param>
		/// <returns>
		/// An <see cref="IEnumerable{T}"/> that contains every N-th element in the list.
		/// </returns>
		public static IEnumerable<T> TakeEvery<T>(this IEnumerable<T> source, int step)
		{
			return source == null
			       	? null
			       	: source.Where((e, i) => i%step == 0);
		}

		/// <summary>
		/// Returns a specified number of contiguous elements from the end of 
		/// a sequence.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="source"/>.</typeparam>
		/// <param name="source">The sequence to pad.</param>
		/// <param name="count">The number of elements to return.</param>
		/// <returns>
		/// An <see cref="IEnumerable{T}"/> that contains the specified number of 
		/// elements from the end of the input sequence.
		/// </returns>
		public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> source, int count)
		{
			if (source == null || count <= 0)
				yield break;

			var queue = new Queue<T>(count);

			foreach (var item in source)
			{
				if (queue.Count == count)
					queue.Dequeue();

				queue.Enqueue(item);
			}

			foreach (var item in queue)
				yield return item;
		}

		/// <summary>
		/// Returns items from the input sequence until the given predicate returns true
		/// when applied to the current source item; that item will be the last returned.
		/// </summary>
		/// <typeparam name="T">Type of the source sequence</typeparam>
		/// <param name="source">Source sequence</param>
		/// <param name="predicate">Predicate used to determine when to stop yielding results from the source.</param>
		/// <returns>Items from the source sequence, until the predicate returns true when applied to the item.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="predicate"/> is null</exception>
		public static IEnumerable<T> TakeUntil<T>(this IEnumerable<T> source, Func<T, bool> predicate)
		{
			if (source == null || predicate == null)
				yield break;

			foreach (var item in source)
			{
				yield return item;

				if (predicate(item))
				{
					yield break;
				}
			}
		}


		/// <summary>
		/// Turns an IEnumerable into a string using a certain delimiter.
		/// Returns an empty string incase the enumeration is null or empty.
		/// </summary>
		/// <typeparam name="T">The type of items the enumeration contains.</typeparam>
		/// <param name="enumerable">The enumeration.</param>
		/// <param name="delimiter">The delimiter.</param>
		/// <returns>A string representation of the list.</returns>
		public static string ToString<T>(this IEnumerable<T> enumerable, string delimiter)
		{
			return enumerable == null
			       	? null
			       	: string.Join(delimiter, enumerable.Select(data => data.ToString()).ToArray());
		}


	    /// <summary>
	    /// Produces the union of two sequences by using a certain key.
	    /// </summary>
	    /// 
	    /// <returns>
	    /// An <see cref="T:System.Collections.Generic.IEnumerable`1"/> that contains the elements from both input sequences, excluding duplicates.
	    /// </returns>
	    /// <param name="first">An <see cref="T:System.Collections.Generic.IEnumerable`1"/> whose distinct elements form the first set for the union.</param><param name="second">An <see cref="T:System.Collections.Generic.IEnumerable`1"/> whose distinct elements form the second set for the union.</param>
	    /// <param name="keySelector">The key selector</param>
	    /// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
	    /// <typeparam name="TKey">The type of the key of the input sequences.</typeparam>
	    /// <exception cref="T:System.ArgumentNullException"><paramref name="first"/> or <paramref name="second"/> is null.</exception>
	    public static IEnumerable<TSource> Union<TSource, TKey>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Func<TSource, TKey> keySelector)
        {
            var set = new HashSet<TKey>();

            var firstFiltered = first.Where(element => set.Add(keySelector(element)));
            var secondFiltered = second.Where(element => set.Add(keySelector(element)));

            //It's important that we call ToList, since the hash set will be used more than once otherwise, giving a false result.
            return firstFiltered.Append(secondFiltered).ToList();
        }


        /// <summary>
        /// Produces the union of two sequences by using a certain key.
        /// </summary>
        /// 
        /// <returns>
        /// An <see cref="T:System.Collections.Generic.IEnumerable`1"/> that contains the elements from both input sequences, excluding duplicates.
        /// </returns>
        /// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1"/> whose distinct elements form the first set for the union.</param><param name="additionals">An <see cref="T:System.Collections.Generic.IEnumerable`1"/> whose distinct elements form the second set for the union.</param>
        /// <typeparam name="T">The type of the elements of the input sequences.</typeparam>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> or <paramref name="additionals"/> is null.</exception>
        public static IEnumerable<T> Union<T>(this IEnumerable<T> source, params T[] additionals)
        {
            return Enumerable.Union(source, additionals);
        }


        /// <summary>
        /// Produces the union of two sequences by using a certain key.
        /// </summary>
        /// 
        /// <returns>
        /// An <see cref="T:System.Collections.Generic.IEnumerable`1"/> that contains the elements from both input sequences, excluding duplicates.
        /// </returns>
        /// <param name="first">An <see cref="T:System.Collections.Generic.IEnumerable`1"/> whose distinct elements form the first set for the union.</param><param name="second">An <see cref="T:System.Collections.Generic.IEnumerable`1"/> whose distinct elements form the second set for the union.</param>
        /// <param name="keySelector">The key selector</param>
        /// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
        /// <typeparam name="TKey">The type of the key of the input sequences.</typeparam>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="first"/> or <paramref name="second"/> is null.</exception>
        public static IEnumerable<TSource> Union<TSource, TKey>(this IEnumerable<TSource> first, Func<TSource, TKey> keySelector, params TSource[] second)
        {
            return first.Union(second, keySelector);
        }

	}
}