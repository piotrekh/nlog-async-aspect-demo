using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NLogAsyncAspectDemo.Models
{
    public class SamplePOCO
    {
        public float Value { get; set; }

        public string Text { get; set; }

        public Guid Id { get; set; }

        public List<string> Items { get; set; }
    }
}