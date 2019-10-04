using Microsoft.AspNetCore.Mvc;

namespace CoreLocalizationTest.Controllers
{
    public class ServiceRequestsController : BaseController
    {
        // GET
        [HttpGet("/{culture:regex(en)}/dashboard/servicerequests")]
        [HttpGet("/{culture:regex(fr)}/tableaudebord/demandesdeservice")]
        public IActionResult Index()
        {
            return View();
        }
    }
}