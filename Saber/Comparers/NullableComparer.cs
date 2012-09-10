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