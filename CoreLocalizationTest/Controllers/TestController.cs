using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;

namespace CoreLocalizationTest.Controllers
{
    public class TestController : Controller
    {
        private readonly IStringLocalizer<SharedResource> _localizer;

        public TestController(IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
        }

        [HttpGet("/{culture:regex(en)}/test/about")]
        [HttpGet("/{culture:regex(fr)}/tester/apropos")]
        public IActionResult About()
        {
            var q = _localizer["Hello"];

            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        //public override void OnActionExecuting(ActionExecutingContext context)
        //{
        //    var culture = context.RouteData.Values["culture"].ToString();

        //    switch (culture.ToLower())
        //    {
        //        case "eng":
        //            {
        //                SetCulture("en-CA");

        //                break;
        //            }
        //        case "fra":
        //            {
        //                SetCulture("fr-CA");

        //                break;
        //            }
        //        default:
        //            {
        //                SetCulture("en-CA");

        //                break;
        //            }
        //    }

        //    base.OnActionExecuting(context);
        //}

        //private void SetCulture(string lang)
        //{
        //    CultureInfo.CurrentCulture = new CultureInfo(lang);
        //    CultureInfo.CurrentUICulture = new CultureInfo(lang);
        //}
    }
}