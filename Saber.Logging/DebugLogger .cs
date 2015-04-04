using System.Diagnostics;

namespace Saber.Logging
{
    /// <summary>
    /// An instance of a Threaded Logger that logs using Debug.Write
    /// </summary>
    public class DebugLogger : ThreadedLogger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DebugLogger" /> class.
        /// </summary>
        public DebugLogger()
            : base(x => Debug.Write(x))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DebugLogger" /> class.
        /// </summary>
        /// <param name="category">The category.</param>
        public DebugLogger(string category)
            : base(x => Debug.Write(x, category))
        {
        }
    }
}
