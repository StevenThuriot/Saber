using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Saber.Domain
{
    /// <summary>
    /// Grouped adjacent enumerable
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    internal class GroupedAdjacentEnumerable<TSource, TKey> : IEnumerable<IGrouping<TKey, TSource>>
    {
        readonly IEnumerable<TSource> _source;
        readonly Func<TSource, TKey> _keySelector;
        readonly IEqualityComparer<TKey> _comparer;

        /// <summary>
        /// Initializes a new instance of the GroupedAdjacentEnumerable class.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="keySelector">The key selector.</param>
        /// <param name="comparer">The comparer.</param>
        /// <exception cref="System.ArgumentNullException">source</exception>
        public GroupedAdjacentEnumerable(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (keySelector == null) throw new ArgumentNullException("keySelector");

            _source = source;
            _keySelector = keySelector;
            _comparer = comparer ?? EqualityComparer<TKey>.Default;
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<IGrouping<TKey, TSource>> GetEnumerator()
        {
            var lastKey = default(TKey);
            List<TSource> group = null;

            foreach (var item in _source)
            {
                var key = _keySelector(item);

                if (group != null && _comparer.Equals(key, lastKey))
                {
                    group.Add(item);
                }
                else
                {
                    if (group != null)
                        yield return new GroupOfAdjacent<TSource, TKey>(group, lastKey);

                    group = new List<TSource> { item };
                }

                lastKey = key;
            }

            if (group != null)
                yield return new GroupOfAdjacent<TSource, TKey>(group, lastKey);
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
