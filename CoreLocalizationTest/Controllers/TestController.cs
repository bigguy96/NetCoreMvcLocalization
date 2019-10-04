using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CoreLocalizationTest.Controllers
{
    public class TestController : Controller
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

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var culture = context.RouteData.Values["culture"].ToString();
            var alternateCulture = culture.Equals("en") ? "fr" : "en";
            var path = string.Empty;

            if (context.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
            {
                var actionAttributes = controllerActionDescriptor.MethodInfo.GetCustomAttributes(inherit: true)
                    .Cast<HttpGetAttribute>()
                    .SingleOrDefault(x => !x.Template.Contains(culture));

                var regex = new Regex(@"/{(.*?)}");
                if (actionAttributes != null)
                {
                    path = regex.Replace(actionAttributes.Template, "");
                }
            }

            ViewData["Toggle"] = $"/{alternateCulture}{path}";

            base.OnActionExecuting(context);
        }
    }
}

//https://ssl-templates.services.gc.ca/app/cls/WET/gcintranet/v4_0_31/cdts/appTop/apptop_nomenu-en.shtml