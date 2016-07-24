using NLogAsyncAspectDemo.Models;
using NLogAsyncAspectDemo.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace NLogAsyncAspectDemo.Controllers
{
    public class HomeController : Controller
    {
        private ISampleService _sampleService;

        public HomeController(ISampleService sampleService)
        {
            _sampleService = sampleService;
        }

        // GET: Home
        public async Task<ActionResult> Index()
        {
            await InsertLogInViewBag();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(int LogOption)
        {
            Random r = new Random();
            SamplePOCO poco = new SamplePOCO()
            {
                Id = Guid.NewGuid(),
                Items = new List<string> { "poco_item_1", "poco_item_2", "poco_item_3" },
                Text = "poco",
                Value = (float)r.Next(10000) / 100
            };
            SampleILoggable loggableObj1 = new SampleILoggable()
            {
                Id = Guid.NewGuid(),
                Items = new List<string> { "obj1_item_1", "obj1_item_2", "obj1_item_3" },
                Sibling = null,
                Text = "obj1",
                Value = (float)r.Next(10000) / 100
            };
            SampleILoggable loggableObj2 = new SampleILoggable()
            {
                Id = Guid.NewGuid(),
                Items = new List<string> { "obj2_item_1", "obj2_item_2" },
                Sibling = null,
                Text = "obj2",
                Value = (float)r.Next(10000) / 100
            };
            loggableObj1.Sibling = loggableObj2;
            loggableObj2.Sibling = loggableObj1;

            if (LogOption == 0)
            {
                _sampleService.DoSomething(loggableObj1);
            }
            else if(LogOption == 1)
            {
                var result = await _sampleService.DoSomethingAsync1(loggableObj2);
            }
            else if(LogOption == 2)
            {
                await _sampleService.DoSomethingAsync2("sample text", Guid.NewGuid(), 20, poco, loggableObj1);
            }

            await InsertLogInViewBag();
            return View();
        }

        public async Task InsertLogInViewBag()
        {
            string filePath = HostingEnvironment.MapPath("/NLogAsyncAspectDemo.log");
            if (System.IO.File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    var log = await sr.ReadToEndAsync();
                    ViewBag.Log = log;
                }
            }
        }
    }
}