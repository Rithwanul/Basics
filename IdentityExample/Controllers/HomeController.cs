using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Basics.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        public IActionResult Authenticate()
        {

            var grandmaClaims = new List<Claim>()
            {
                
                new Claim(ClaimTypes.Name, "Bob"),
                new Claim(ClaimTypes.Email, "bob@fmail.com"),
                new Claim("Gramdma.Says", "Very nice boy")
            };

            var licenceClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "Dico"),
                new Claim("DrivingLicence", "A+")
            };

            var grandmaIdentity = new ClaimsIdentity(grandmaClaims, "Grandma Identity");
            var licenceIdentity = new ClaimsIdentity(licenceClaims, "Government");

            var userPrincipal = new ClaimsPrincipal(new[] { grandmaIdentity, licenceIdentity});

            HttpContext.SignInAsync(userPrincipal);

            return RedirectToAction(nameof(Index));
        }
    }
}
