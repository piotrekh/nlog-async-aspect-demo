using NLogAsyncAspectDemo.Logging;
using NLogAsyncAspectDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLogAsyncAspectDemo.Services
{
    public interface ISampleService
    {
        void DoSomething(SampleILoggable loggableObj);

        Task<SamplePOCO> DoSomethingAsync1(SampleILoggable loggableObj);

        Task DoSomethingAsync2(string text, Guid id, float value, SamplePOCO poco, SampleILoggable loggableObj);
    }
}
