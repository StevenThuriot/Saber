using System;

namespace Saber.Comparers
{
    /// <summary>
    /// Compare nullable structs.
    /// </summary>
    /// <typeparam name="T">A nullable type</typeparam>
    public class NullableComparer<T> : INullableComparer<T> where T : struct, IComparable<T>
    {
        /// <summary>
        /// Compares the specified values.
        /// </summary>
        /// <param name="x">The first value.</param>
        /// <param name="y">The second value.</param>
        /// <returns></returns>
        public int Compare(T? x, T? y)
        {
            if (!x.HasValue && !y.HasValue)
                return 0;

            if (x.HasValue && !y.HasValue)
                return -1;

            if (!x.HasValue)
                return 1;

            return x.Value.CompareTo(y.Value);
        }
    }
}