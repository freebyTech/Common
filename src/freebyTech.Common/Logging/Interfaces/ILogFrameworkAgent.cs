using System;
using System.Collections.Generic;
using System.Text;

namespace freebyTech.Common.Logging.Interfaces
{
    /// <summary>
    /// This interface represents an actual logging agent that takes the logging abstraction and translates it to each framework.
    /// </summary>
    public interface ILogFrameworkAgent
    {
        void Log(string loggerName, GenericLogEventInfo logEvent);
    }
}