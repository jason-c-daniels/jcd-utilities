using System;
using System.Collections.Generic;
using System.Text;

namespace Jcd.Utilities.Logging
{
    /// <summary>
    /// An IoC type that will create/
    /// </summary>
    public interface ILoggingContextAccessor
    {
        /// <summary>
        /// Accesses the logging context and returns it.
        /// </summary>
        /// <returns>The logging context.</returns>
        ILoggingContext GetLoggingContext();
    }
}
