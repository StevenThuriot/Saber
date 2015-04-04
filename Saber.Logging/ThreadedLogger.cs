using System;
using System.Threading;
using System.Threading.Tasks.Dataflow;
using Saber.Helpers;

namespace Saber.Logging
{
    /// <summary>
    /// Threaded Logging Class
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", 
        Justification = "Disposing CancellationTokenSources don't actually change anything. They still get GC'd if not disposed, so under the moto of keeping it simple, I have decided not to implement IDisposable on this class.")]
    public class ThreadedLogger
    {
        private readonly ActionBlock<string> _ActionBlock;
        private readonly CancellationTokenSource _CancellationTokenSource;

        /// <summary>
        /// Initializes a new instance of the <see cref="ThreadedLogger" /> class.
        /// </summary>
        /// <param name="log">The log.</param>
        public ThreadedLogger(Action<string> log)
        {
            Guard.NotNull(log);

            _CancellationTokenSource = new CancellationTokenSource();
            var options = new ExecutionDataflowBlockOptions
            {
                CancellationToken = _CancellationTokenSource.Token,
                MaxDegreeOfParallelism = 1
            };

            _ActionBlock = new ActionBlock<string>(log, options);
        }

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Log(string message)
        {
            _ActionBlock.Post(message);
        }

        /// <summary>
        /// Logs the specified message with a newline at the end.
        /// </summary>
        /// <param name="message">The message.</param>
        public void LogLine(string message)
        {
            var newLineMessage = message + Environment.NewLine;
            Log(newLineMessage);
        }

        /// <summary>
        /// Cancels the logging.
        /// </summary>
        public void Cancel()
        {
            if (!_ActionBlock.Completion.IsCompleted)
            {
                _CancellationTokenSource.Cancel();
            }
        }

        /// <summary>
        /// Completes the logging.
        /// New log messages will no longer be accepted.
        /// </summary>
        public void Complete()
        {
            _ActionBlock.Complete();
        }
    }
}
