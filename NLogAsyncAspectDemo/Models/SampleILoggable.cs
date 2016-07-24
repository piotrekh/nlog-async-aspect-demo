using Newtonsoft.Json;
using NLogAsyncAspectDemo.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace NLogAsyncAspectDemo.Models
{
    public class SampleILoggable : ILoggable
    {
        public float Value { get; set; }

        public string Text { get; set; }

        public Guid Id { get; set; }

        public List<string> Items { get; set; }

        public SampleILoggable Sibling { get; set; }

        public string GetLogMessage()
        {
            //prevent circular references by storing SiblingId instead of whole object
            Guid? siblingId = Sibling?.Id;
            string valueFormatted = Value.ToString("0.00", CultureInfo.InvariantCulture);

            var obj = new
            {
                Value = valueFormatted,
                Text = this.Text,
                Id = this.Id,
                Items = this.Items,
                SiblingId = siblingId
            };

            string logMessage = JsonConvert.SerializeObject(obj);
            return logMessage;
        }
    }
}