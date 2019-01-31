namespace freebyTech.Common.Logging.Interfaces
{
    public class PushLogItem
    {
        public PushLogItem(GenericLogLevel logLevel, string key, string value, long durationMs) {
            LogLevel = logLevel;
            Key = key;
            Value = value;
            DurationMs = durationMs;
        }

        public PushLogItem(GenericLogLevel logLevel, string line, long durationMs) {
            LogLevel = logLevel;
            Line = line;
            DurationMs = durationMs;
        }

        public GenericLogLevel LogLevel { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Line { get; set; }
        public long DurationMs { get; set;}
    }
}