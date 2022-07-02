using SimpleFileLogging.Enums;
using SimpleFileLogging.Interfaces;
using SimpleFileLogging.Logging;

namespace SI.Logging
{
    /// <summary>
    /// Simple Logger class for logging operations.
    /// </summary>
    public static class SimpleCommonLogger
    {
        /// <summary>
        /// Logger with day date format.
        /// </summary>
        public static ISimpleLogger DayLogger
        {
            get
            {
                ISimpleLogger instance = SimpleLoggerStorage.GetSimpleLogger(SimpleLogDateFormats.Day);

#if DEBUG
                instance.Debug("First day instance created");
#endif

                return instance;
            }
        }

        /// <summary>
        /// Logger with hour date format.
        /// </summary>
        public static ISimpleLogger HourLogger
        {
            get
            {
                ISimpleLogger instance = SimpleLoggerStorage.GetSimpleLogger(SimpleLogDateFormats.Hour);
#if DEBUG
                instance.Debug("First hour instance created");
#endif
                return instance;
            }
        }

        /// <summary>
        /// Logger without date format.
        /// </summary>
        public static ISimpleLogger Logger
        {
            get
            {
                ISimpleLogger instance = SimpleLoggerStorage.GetSimpleLogger(SimpleLogDateFormats.None);
#if DEBUG
                instance.Debug("First none instance created");
#endif
                return instance;
            }
        }

        /// <summary>
        /// Logger with month date format.
        /// </summary>
        public static ISimpleLogger MonthLogger
        {
            get
            {
                ISimpleLogger instance = SimpleLoggerStorage.GetSimpleLogger(SimpleLogDateFormats.Month);
#if DEBUG
                instance.Debug("First month instance created");
#endif
                return instance;
            }
        }
    }
}