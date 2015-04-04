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
