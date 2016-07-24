using Newtonsoft.Json;
using Ninject.Extensions.Interception;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace NLogAsyncAspectDemo.Logging
{
    public class LogInterceptor : AsyncInterceptor
    {
        protected Logger _logger;
        protected LogLevel _level;

        public LogInterceptor(string loggerName, LogLevel level)
        {
            if (string.IsNullOrWhiteSpace(loggerName)) _logger = LogManager.GetCurrentClassLogger();
            else _logger = LogManager.GetLogger(loggerName);
            _level = level;
        }

        protected override void BeforeInvoke(IInvocation invocation)
        {
            //check if log should actually be written with current level
            //- if not, there is no need to do any expensive serialization of data
            if (!_logger.IsEnabled(_level)) return;

            string methodName = invocation.Request.Method.Name;

            try
            {
                StringBuilder _logBuilder = new StringBuilder();
                object[] parameterValues = invocation.Request.Arguments;

                if (parameterValues == null || parameterValues.Length == 0) //method with no parameters
                {
                    _logBuilder.AppendFormat("Method {0} was called", methodName);
                }
                else
                {
                    List<string> parameterNames = invocation.Request.Method.GetParameters().Select(x => x.Name).ToList();

                    _logBuilder.AppendFormat("Method {0} called with parameters: ", methodName);
                    
                    for (int i = 0; i < parameterValues.Length; i++)
                    {
                        _logBuilder.AppendLine();

                        object paramValue = parameterValues[i];

                        //if object implementes ILoggable, write GetLogMessage() result to log,
                        //otherwise serialize the object using Newtonsoft.Json
                        var loggable = paramValue as ILoggable;

                        if (loggable != null) _logBuilder.AppendFormat("Name: {0}; Value: {1}", parameterNames[i], loggable.GetLogMessage());
                        else _logBuilder.AppendFormat("Name: {0}; Value: {1}", parameterNames[i], JsonConvert.SerializeObject(paramValue));
                        
                    }
                }

                //save log
                _logger.Log(_level, _logBuilder.ToString());
            }
            catch { }

        }

        protected override void AfterInvoke(IInvocation invocation)
        {
            //check if log should actually be written with current level
            //- if not, there is no need to do any expensive serialization of data
            if (!_logger.IsEnabled(_level)) return;

            //no need to log anything if method does not return any data
            if (invocation.Request.Method.ReturnType == typeof(void)
                || invocation.Request.Method.ReturnType == typeof(Task)) return;

            string methodName = invocation.Request.Method.Name;

            try
            {
                StringBuilder _logBuilder = new StringBuilder();
                _logBuilder.AppendFormat("Method {0} returned: ", methodName);
                _logBuilder.AppendLine();

                //if object implementes ILoggable, write GetLogMessage() result to log,
                //otherwise serialize the object using Newtonsoft.Json
                var loggable = invocation.ReturnValue as ILoggable;

                if (loggable != null) _logBuilder.Append(loggable.GetLogMessage());
                else _logBuilder.Append(JsonConvert.SerializeObject(invocation.ReturnValue));

                //save log
                _logger.Log(_level, _logBuilder.ToString());
            }
            catch { }

        }

    }
}