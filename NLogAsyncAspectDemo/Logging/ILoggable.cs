using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLogAsyncAspectDemo.Logging
{
    public interface ILoggable
    {
        string GetLogMessage();
    }
}
