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

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace Saber.Logging
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITraceBlock : IDisposable
    {
        /// <summary>
        /// Traces the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="arguments">The arguments.</param>
        void Trace(string message, params object[] arguments);
    }

    /// <summary>
    /// 
    /// </summary>
    public static class Tracing
    {
        private static readonly ThreadLocal<Stack<ITraceBlock>> _TraceStack = new ThreadLocal<Stack<ITraceBlock>>(() => new Stack<ITraceBlock>());
        
        /// <summary>
        /// Gets the trace stack.
        /// </summary>
        /// <value>
        /// The trace stack.
        /// </value>
        private static Stack<ITraceBlock> TraceStack
        {
            get { return _TraceStack.Value; }
        }

        /// <summary>
        /// Gets the current trace block.
        /// </summary>
        /// <value>
        /// The current trace block.
        /// </value>
        public static ITraceBlock CurrentTraceBlock
        {
            get
            {
                return TraceStack.Count > 0
                           ? TraceStack.Peek()
                           : null;
            }
        }

        /// <summary>
        /// Traces the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="arguments">The arguments.</param>
        public static void Trace(string message, params object[] arguments)
        {
            var current = CurrentTraceBlock;

            if (current == null)
            {
                //No block in current scope, create temporary new one.
                using (current = TraceBlock.New())
                {
                    current.Trace(message, arguments);
                }
            }
            else
            {
                //Trace to block in scope.
                current.Trace(message, arguments);
            }
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <returns></returns>
        public static ITraceBlock CreateBlock()
        {
            return TraceBlock.New();
        }

        /// <summary>
        /// Trace block.
        /// </summary>
        private class TraceBlock : ITraceBlock
        {
            private bool _Disposed;

            /// <summary>
            /// Prevents a default instance of the <see cref="TraceBlock" /> class from being created.
            /// </summary>
            private TraceBlock()
            {
                TraceStack.Push(this);
            }

            /// <summary>
            /// Creates a new instance.
            /// </summary>
            /// <returns></returns>
            public static ITraceBlock New()
            {
                return new TraceBlock();
            }

            /// <summary>
            /// Finalizes an instance of the <see cref="TraceBlock" /> class.
            /// </summary>
            ~TraceBlock()
            {
                Dispose(false);
            }

            /// <summary>
            /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
            /// </summary>
            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            /// <summary>
            /// Traces the specified message.
            /// </summary>
            /// <param name="message">The message.</param>
            /// <param name="arguments">The arguments.</param>
            void ITraceBlock.Trace(string message, params object[] arguments)
            {
                var formattedMessage = String.Format(CultureInfo.CurrentCulture, message, arguments);
                //Trace(formattedMessage);
            }

            /// <summary>
            /// Releases unmanaged and - optionally - managed resources.
            /// </summary>
            /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
            private void Dispose(bool disposing)
            {
                if (_Disposed) return;

                if (disposing)
                {
                    TraceStack.Pop();
                }

                _Disposed = true;
            }
        }
    }
}