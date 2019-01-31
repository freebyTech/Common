using System;
using System.Collections.Generic;

namespace freebyTech.Common.Logging.Core
{
    /// <summary>
    /// A class defining the current duration of a logging operation as defined either by the time the logger class was created or the last time ResetDuration() was run.
    /// </summary>
    public class LoggerDuration
    {
        public long Ms { get; set; }
        public double Minutes { get; set; }
    }
}