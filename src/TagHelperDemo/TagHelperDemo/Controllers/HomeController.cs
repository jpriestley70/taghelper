using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TagHelperDemo.Models;

namespace TagHelperDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Home
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Privacy
        /// </summary>
        /// <returns></returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Email TagHelper
        /// </summary>
        /// <returns></returns>
        public IActionResult TagEmail()
        {
            return View();
        }

        /// <summary>
        /// Web Site Tag Helper
        /// </summary>
        /// <returns></returns>
        public IActionResult TagWebSite()
        {
            return View();
        }

        /// <summary>
        /// Telephone TagHelper
        /// </summary>
        /// <returns></returns>
        public IActionResult TagTelephone()
        {
            return View();
        }

        /// <summary>
        /// Initialise ChildSection
        /// </summary>
        /// <returns></returns>
        public IActionResult TagChildSection()
        {
            return View();
        }

        /// <summary>
        /// Render ChildSection
        /// </summary>
        /// <returns></returns>
        public IActionResult TagRenderChildSection()
        {
            return View();
        }

        /// <summary>
        /// Initialise and render child sections in different order
        /// </summary>
        /// <returns></returns>
        public IActionResult TagChildSection2()
        {
            return View();
        }

        /// <summary>
        /// Error
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
