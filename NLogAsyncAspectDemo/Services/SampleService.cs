using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using NLogAsyncAspectDemo.Logging;
using NLogAsyncAspectDemo.Models;
using NLog;

namespace NLogAsyncAspectDemo.Services
{
    public class SampleService : ISampleService
    {
        [Log(Level = NLogLevel.Debug, LoggerName = "NLogAsyncAspectDemo.Services.SampleService")]
        public void DoSomething(SampleILoggable loggableObj)
        {
            //do something
        }

        [Log(Level = NLogLevel.Debug, LoggerName = "NLogAsyncAspectDemo.Services.SampleService")]
        public async Task<SamplePOCO> DoSomethingAsync1(SampleILoggable loggableObj)
        {
            //do some asynchronous work
            float value = await Task.Run(() => { return 5.0f * loggableObj.Value; });

            List<string> items = loggableObj.Items.Select(x => x + "_POCO").ToList();

            SamplePOCO poco = new SamplePOCO()
            {
                Id = Guid.NewGuid(),
                Items = items,
                Text = loggableObj.Text + "_POCO",
                Value = value
            };

            return poco;
        }

        [Log(Level = NLogLevel.Debug, LoggerName = "NLogAsyncAspectDemo.Services.SampleService")]
        public async Task DoSomethingAsync2(string text, Guid id, float value, SamplePOCO poco, SampleILoggable loggableObj)
        {
            //do some asynchronous work
            float newValue = await Task.Run(() => { return value * loggableObj.Value; });

            List<string> items = loggableObj.Items.Select(x => x + "_POCO").ToList();

            poco.Id = id;
            poco.Items = items;
            poco.Text = loggableObj.Text + text;
            poco.Value = newValue;
        }
    }
}