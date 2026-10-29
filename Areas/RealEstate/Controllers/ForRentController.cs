using System.Threading.Tasks;
using AppContractsSCO.Models.Logging;
using AppContractsSCO.Services.Logging;
using Microsoft.AspNetCore.Mvc;

namespace RealEstateRCL.Areas.RealEstate.Controllers
{
    [Area("RealEstate")]
    [Route("RealEstate/[controller]")]
    public class ForRentController : Controller
    {
        //Interface defined in AppContractsSCO
        private readonly IPageHitService _pageHitService;
    
        public ForRentController(IPageHitService pageHitService)
        {
            _pageHitService = pageHitService;//Implemented in host app
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Keswick_ForRent")]
        public async Task<IActionResult> Keswick_ForRent()
        {
            await _pageHitService.LogPageHitAsync("realEstate", "Keswick_ForRent");

            var model = new CountModel
            {
                PageHitCount = _pageHitService.GetPageHitCount("realEstate", "Keswick_ForRent")
            };
            return View(model);
        }




        [HttpGet("Lenox")]
        public async Task<IActionResult> Lenox()
        {



            //Update values in the model for this web page
            //model.CountModel.PageHitCount = _pageHitService.GetPageHitCount("realEstate", "Keswick");

            //Use service to log a page hit. This service will determine
            //whether or not the page request is from a human or a bot and
            //act accordingly.s
            await _pageHitService.LogPageHitAsync("realEstate", "Lenox");

            var model = new CountModel
            {
                PageHitCount = _pageHitService.GetPageHitCount("realEstate", "Lenox")
            };
            return View(model);
        }





    }
}