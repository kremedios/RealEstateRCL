using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

using AppContractsSCO.Services.Logging;
using AppContractsSCO.Models.Logging;
using AppContractsSCO.Services.RealEstate;
using AppContractsSCO.Models.RealEstate.ForSale;

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
            _pageHitService.BlockAccess();//test this--block


            //Load the model with existing values read from JSON file
            //var model = _settingsService.Load();
            string webpageName = "Keswick";
            string settingsFilename = "KeswickForSaleSettings.json";
            //The "type" of model is a "ForSaleSettings"
            var model = _settingsService.Load(webpageName, settingsFilename);


            //Use service to get current PageHitCount 
            await _pageHitService.LogPageHitAsync("realEstate", "Keswick");
            //Update values in the model for this web page
            model.CountModel.PageHitCount = _pageHitService.GetPageHitCount("realEstate", "Keswick");
                
            
            //You can now use these values in the following View
            return View(model);
        }




        [Authorize(Roles = "Admin:keswick")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet("KeswickAdmin")]
        public IActionResult KeswickAdmin()
        {
            
            var model = _settingsService.Load(
            "Keswick",
            "KeswickForSaleSettings.json");

            return View(model);
        }




        [Authorize(Roles = "Admin:keswick")]
        [HttpPost("KeswickAdmin")]
        [ValidateAntiForgeryToken]
        public IActionResult KeswickAdmin(ForSaleSettings model)
        {
            _settingsService.Save(
                "Keswick",
                "KeswickForSaleSettings.json",
                model);

            return RedirectToAction(nameof(Keswick));
        }




    }
}