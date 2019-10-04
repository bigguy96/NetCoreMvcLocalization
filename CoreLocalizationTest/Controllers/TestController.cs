using Microsoft.AspNetCore.Mvc;

namespace CoreLocalizationTest.Controllers
{
    public class TestController : BaseController
    {
        [HttpGet("/{culture:regex(en)}/test/about")]
        [HttpGet("/{culture:regex(fr)}/tester/apropos")]
        public IActionResult About()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}

//https://ssl-templates.services.gc.ca/app/cls/WET/gcintranet/v4_0_31/cdts/appTop/apptop_nomenu-en.shtml