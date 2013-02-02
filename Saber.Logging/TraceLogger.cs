#region License

// 
//  Copyright 2013 Steven Thuriot
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

using System.Diagnostics;
using Saber.Helpers;

namespace Saber.Logging
{
    /// <summary>
    /// An instance of a Threaded Logger that logs using Trace.Write
    /// </summary>
    public class TraceLogger : ThreadedLogger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TraceLogger" /> class.
        /// </summary>
        public TraceLogger()
            : base(x => Trace.Write(x))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TraceLogger" /> class.
        /// </summary>
        /// <param name="category">The category.</param>
        public TraceLogger(string category)
            : base(x => Trace.Write(x, category))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TraceLogger" /> class.
        /// </summary>
        /// <param name="source">The source.</param>
        public TraceLogger(TraceSource source)
            : base(x => source.TraceInformation(x))
        {
            Guard.NotNull(source);
        }
    }
}
