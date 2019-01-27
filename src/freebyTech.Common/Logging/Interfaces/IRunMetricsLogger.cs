using System;

namespace freebyTech.Common.Logging.Interfaces
{
    public interface IRunMetricsLogger : IBasicLogger
    {
        /// <summary>
        /// Will Log all Application run statistics into an instrumentation message.
        /// </summary>
        void LogRunMetrics(bool fileTypeLogging = false);

        void ResetSummaryData();
    }
}