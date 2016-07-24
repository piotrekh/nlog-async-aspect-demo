using Ninject.Extensions.Interception.Attributes;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Extensions.Interception;
using Ninject.Extensions.Interception.Request;

namespace NLogAsyncAspectDemo.Logging
{
    public class LogAttribute : InterceptAttribute
    {
        public string LoggerName { get; set; }

        //Cannot use NLog.LogLevel because it is not a valid attribute type
        public NLogLevel Level { get; set; } = NLogLevel.Info;
        
        public override IInterceptor CreateInterceptor(IProxyRequest request)
        {
            return new LogInterceptor(LoggerName, ConvertLevel());
        }

        private LogLevel ConvertLevel()
        {
            switch(Level)
            {
                case NLogLevel.Trace:
                    return LogLevel.Trace;
                case NLogLevel.Debug:
                    return LogLevel.Debug;
                case NLogLevel.Info:
                    return LogLevel.Info;
                case NLogLevel.Warn:
                    return LogLevel.Warn;
                case NLogLevel.Error:
                    return LogLevel.Error;
                case NLogLevel.Fatal:
                    return LogLevel.Fatal;
                default: return LogLevel.Info;
            }
        }
    }
}