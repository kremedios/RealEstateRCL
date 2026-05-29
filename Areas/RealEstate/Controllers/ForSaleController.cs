using Microsoft.AspNetCore.Mvc;

namespace RealEstateRCL.Areas.RealEstate.Controllers
{
    [Area("RealEstate")]
    [Route("RealEstate/[controller]")]
    public class ForSaleController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Keswick")]
        public IActionResult Keswick()
        {
            return View();
        }
    }
}