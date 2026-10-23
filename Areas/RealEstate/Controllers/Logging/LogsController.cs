using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AppContractsSCO.Services.Logging;
using Microsoft.AspNetCore.Mvc;


namespace RealEstateRCL.Areas.RealEstate.Controllers.Logging
{
    [Area("realEstate")]
    public class LogsController : Controller
    {
        /**
        This controller (residing in this RCL):
            - validates input
            - calls service
            - returns view

        The host supplies data via interface.
        */

        //This value is specific to this RCL
        private const string AREA = "realEstate";

        //Interface defined in AppContractsSCO
        private readonly IPageHitService _pageHitService;


        public LogsController(IPageHitService pageHitService)
        {
            _pageHitService = pageHitService;
        }


        public IActionResult PageHits(string pageName)
        {            
            if (string.IsNullOrEmpty(pageName))
                return BadRequest("pageName is required.");
          

            var entries = _pageHitService
                .GetPageHitsAsync(AREA, pageName)
                .Result
                .ToList();


            ViewData["Area"] = AREA;
            ViewData["PageName"] = pageName;


            if (entries.Count == 0)
                return Content("Log file exists but no entries found.");
    
            return View("PageHits", entries); // pass List<string[]> to the view
        }
    }
}


