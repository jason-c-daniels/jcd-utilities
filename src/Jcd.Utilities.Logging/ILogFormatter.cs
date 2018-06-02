using System;
using System.Collections.Generic;
using System.Text;

namespace Jcd.Utilities.Logging
{
    public interface ILogFormatter
    {
        /// <summary>
        /// Creates a formatted log entry from the logging context, state information, and exception.
        /// </summary>
        /// <typeparam name="TState">Type of the logging state</typeparam>
        /// <param name="loggingContext">the logging context</param>
        /// <param name="state">the state information</param>
        /// <param name="exception">the associated exception, if applicable</param>
        /// <param name="messageFormatter">the externally provided state and exception message formatting function.</param>
        /// <returns>The full log entry to write to a text-based log destination</returns>
        string Format<TState>(ILoggingContext loggingContext=null, TState state=default(TState), Exception exception=null, Func<TState,Exception,string> messageFormatter=null);
    }
}
