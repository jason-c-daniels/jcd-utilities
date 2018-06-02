using System;

namespace Jcd.Utilities.Logging
{
    /// <summary>
    /// A base interface to identify a data-type as providing additional log information. 
    /// </summary>
    public interface ILoggingContext {
    }

    /// <summary>
    /// A base interface to identify a data-type as providing additional log information. 
    /// </summary>
    public interface ILoggingContext<T> : ILoggingContext {
    }
}
