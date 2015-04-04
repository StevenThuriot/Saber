using System;
using System.Collections.Generic;

namespace Saber.Comparers
{
    /// <summary>
    /// Compare nullable structs.
    /// </summary>
    /// <typeparam name="T">A nullable type</typeparam>
    public interface INullableComparer<T> : IComparer<T?>  where T : struct, IComparable<T>
    {
    }
}