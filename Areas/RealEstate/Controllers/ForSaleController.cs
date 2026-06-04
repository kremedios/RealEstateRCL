using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using AppContractsSCO.Services.Logging;
using AppContractsSCO.Models.Logging;
using AppContractsSCO.Services.RealEstate;
//using AppContractsSCO.Models.RealEstate;

using RealEstateRCL.Areas.RealEstate.Controllers;


namespace RealEstateRCL.Areas.RealEstate.Controllers
{
    [Area("RealEstate")]
    [Route("RealEstate/[controller]")]
    public class ForSaleController : Controller
    {
        //Interface defined in AppContractsSCO
        private readonly IPageHitService _pageHitService;

        private readonly IForSaleSettingsService _settingsService;
    
        public ForSaleController(IPageHitService pageHitService,
                                 IForSaleSettingsService forSaleSettingsService)
                            
        {
            _pageHitService = pageHitService;//Implemented in host app
            _settingsService = forSaleSettingsService;//Implemented in host app
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Keswick")]
        public async Task<IActionResult> Keswick()
        {
            //await _pageHitService.LogPageHitAsync("realEstate", "Keswick");

            /*
            var price = _settingsService.Price;
            Console.WriteLine($"price: {price}");

            var model = new CountModel
            {
                PageHitCount = _pageHitService.GetPageHitCount("realEstate", "Keswick")
            };
            return View(model);
            */

            //Load the model with existing values
            var model = _settingsService.Load();

            //Use service to get current PageHitCount 
            await _pageHitService.LogPageHitAsync("realEstate", "Keswick");
            //Update values in the model for this web page
            model.CountModel.PageHitCount = _pageHitService.GetPageHitCount("realEstate", "Keswick");
                
            
            /*.
            var model = new test
            {
                PageHitCount = _pageHitService.GetPageHitCount("realEstate", "Keswick"),
                //Settings = _settingsService.Load()
                //x = "dog"
                KeswickForSaleSettings = _settingsService.Load()
            };
            */
            
            //You can now use these values in the following View
            return View(model);
        }
    }
}