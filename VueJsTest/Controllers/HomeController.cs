using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Npgsql;
using VueJsTest.Models;

namespace VueJsTest.Controllers
{
    public class Group
    {
        public string number { get; set; }
        public Content content { get; set; }
    }
    public class Content
    {
        public Dictionary<int, string> monday { get; set; }
        public Dictionary<int, string> tuesday { get; set; }
        public Dictionary<int, string> wednesday { get; set; }
        public Dictionary<int, string> thursday { get; set; }
        public Dictionary<int, string> friday { get; set; }
    }

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public string SendJson()
        {
            var group1 = new Group {
                number = "121-17-1",
                content = new Content {
                    monday = new Dictionary<int, string> {
                        { 1, "Monday lesson 1" }, { 2, "Monday lesson 2" }
                    },
                    tuesday = new Dictionary<int, string> {
                        { 3, "Tuesday lesson 3" }, { 4, "Tuesday lesson 4" }
                    },
                    wednesday = new Dictionary<int, string> {
                        { 1, "Wednesday lesson 1" }, { 2, "Wednesday lesson 2" }, { 4, "Wednesdaysday lesson 4" }
                    },
                    thursday = new Dictionary<int, string>(),
                    friday = new Dictionary<int, string>()
                }
            };

            string json = JsonConvert.SerializeObject(group1);
            return json;
        }

        [HttpPost]
        public string GetJson([FromBody]Group[] groups)
        {
            return JsonConvert.SerializeObject(groups);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
