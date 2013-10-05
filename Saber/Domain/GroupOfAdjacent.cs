using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Saber.Domain
{
    /// <summary>
    /// Group of adjacent
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    internal class GroupOfAdjacent<TSource, TKey> : IGrouping<TKey, TSource>
    {
        private readonly List<TSource> _groupList;
        private readonly TKey _key;

        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public TKey Key
        {
            get { return _key; }
        }

        /// <summary>
        /// Initializes a new instance of the GroupOfAdjacent class.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="key">The key.</param>
        public GroupOfAdjacent(List<TSource> source, TKey key)
        {
            _groupList = source;
            _key = key;
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<TSource>)this).GetEnumerator();
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns></returns>
        IEnumerator<TSource> IEnumerable<TSource>.GetEnumerator()
        {
            return ((IEnumerable<TSource>)_groupList).GetEnumerator();
        }
    }
}
