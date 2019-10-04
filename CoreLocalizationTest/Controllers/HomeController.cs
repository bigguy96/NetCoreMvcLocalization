using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CoreLocalizationTest.Models;

namespace CoreLocalizationTest.Controllers
{
    public class HomeController : BaseController
    {
        [HttpGet("/{culture:regex(en)}/home")]
        [HttpGet("/{culture:regex(fr)}/accueil")]
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
