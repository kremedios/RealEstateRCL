using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using AppContractsSCO.Services.Logging;
using AppContractsSCO.Models.Logging;

namespace RealEstateRCL.Areas.RealEstate.Controllers
{
    [Area("RealEstate")]
    [Route("RealEstate/[controller]")]
    public class ForSaleController : Controller
    {
        //Interface defined in AppContractsSCO
        private readonly IPageHitService _pageHitService;
    
        public ForSaleController(IPageHitService pageHitService)
        {
            _pageHitService = pageHitService;//Implemented in host app
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Keswick")]
        public async Task<IActionResult> Keswick()
        {
            await _pageHitService.LogPageHitAsync("realEstate", "Keswick");

            var model = new CountModel
            {
                PageHitCount = _pageHitService.GetPageHitCount("realEstate", "Keswick")
            };
            return View(model);
        }
    }
}