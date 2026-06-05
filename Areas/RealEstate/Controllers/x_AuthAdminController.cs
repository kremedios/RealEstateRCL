using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

using AppContractsSCO.Services.Security;

namespace RealEstateRCL.Areas.RealEstate.Controllers
{
    [Area("RealEstate")]
    [Route("RealEstate/[controller]/[action]")]
    [AllowAnonymous]
    public class AdminAuthController : Controller
    {
        private readonly IConfiguration _config;
        private readonly ISecureConfig _secureConfig;

        public AdminAuthController(
            IConfiguration config,
            ISecureConfig secureConfig)
        {
            _config = config;
            _secureConfig = secureConfig;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(
            string username,
            string password)
        {
            var encUser =
                _config["AdminAuth:keswick:Username"];

            var encPass =
                _config["AdminAuth:keswick:Password"];

            var adminUser =
                _secureConfig.Decrypt(encUser);

            var adminPass =
                _secureConfig.Decrypt(encPass);

            if (username != adminUser ||
                password != adminPass)
            {
                ViewBag.Error = "Invalid credentials";
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),

                new Claim(
                    ClaimTypes.Role,
                    "Admin:keswick")
            };

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme,
                ClaimTypes.Name,
                ClaimTypes.Role);

            var principal =
                new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                "Cookies",
                principal);

            return RedirectToAction(
                "KeswickAdmin",
                "ForSale",
                new { area = "RealEstate" });
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return Content("Access denied.");
        }
    }
}