using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCD2.Models;

namespace MVCD2.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult ReceiveData()
        {
            string courseName = "Default";
            if (TempData.ContainsKey("CourseName")) {
                courseName = TempData["CourseName"].ToString();
                TempData.Keep();
            }
            //ViewBag.CourseName = courseName;
            //return View();
            return Content($"Course Name from TempData: {courseName}");

        }
    }
}
