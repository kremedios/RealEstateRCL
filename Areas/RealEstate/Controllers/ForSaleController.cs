using AppContractsSCO.Models.Common;
using AppContractsSCO.Models.RealEstate.ForSale;
using AppContractsSCO.Services.Logging;
using AppContractsSCO.Services.RealEstate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace RealEstateRCL.Areas.RealEstate.Controllers
{
    [Area("RealEstate")]
    [Route("RealEstate/[controller]")]
    public class ForSaleController : Controller
    {
        //Interface defined in AppContractsSCO
        private readonly IPageHitService _pageHitService;
        //private readonly IPageHitEvaluationManager _pageHitEvaluationManager;
        private readonly IForSaleSettingsService _settingsService;
    
        public ForSaleController(IPageHitService pageHitService,
                                 IForSaleSettingsService forSaleSettingsService)
                                 //IPageHitEvaluationManager pageHitEvaluationManager)
                            
        {
            _pageHitService = pageHitService;//Implemented in host app
            _settingsService = forSaleSettingsService;//Implemented in host app
            //_pageHitEvaluationManager = pageHitEvaluationManager;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Keswick")]
        public async Task<IActionResult> Keswick()
        {
            //Get PageHit info
            //Write in PageHitService.cs the code for method GetPageHit(.)


            /**
            Replaced the following code with middleware in Program.cs
            //Use service to evaluate webpage hit
            var whatToDo = await _pageHitEvaluationManager.EvaluatePageHit("realEstate", "Keswick");

            switch (whatToDo)
            {
                case PageHitEvaluation.AllowAndDoNotRegister:

                break;

                case PageHitEvaluation.Block:

                break;

                case PageHitEvaluation.AllowAndRegister:
                default:

                break;
            }
            */


            //Load the model with existing values read from JSON file
            //var model = _settingsService.Load();
            string webpageName = "Keswick";
            string settingsFilename = "KeswickForSaleSettings.json";
            //The "type" of model is a "ForSaleSettings"
            var model = _settingsService.Load(webpageName, settingsFilename);


            //Use service to log a page hit. This service will determine
            //whether or not the page request is from a human or a bot and
            //act accordingly.s
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