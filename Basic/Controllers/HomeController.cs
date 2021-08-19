using Basics.CustomPolicyProvider;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Basic.Controllers
{
    public class HomeController : Controller
    {


        public IActionResult Index()
        {
            return View();
        }
        //page access through authorization // authorize attribute will ask question like are you allowed to come here
        //guard an action and we want to authorize someone like this secret page we have
        [Authorize] 
        // authorize tag/attribute contains information how we want to authorize.
        // 
        public IActionResult Secret()
        {
            return View();
        }

        [Authorize(Policy = "Claim.DoB")]//endpoint that will authorize the policy
        public IActionResult SecretPolicy()
        {
            return View("Secret");
        }

        [Authorize(Roles = "Admin")] // role is another claim that look at the orginal claim
        public IActionResult SecretRole()
        {
            return View("Secret");
        }

        [SecurityLevel(5)]
        public IActionResult SecretLevel()
        {
            return View("Secret");
        }

        [SecurityLevel(10)]
        public IActionResult SecretHigherLevel()
        {
            return View("Secret");
        }

        [AllowAnonymous]
        public IActionResult Authenticate() // creating the user
        {
            var grandmaClaims = new List<Claim>() // claim is an abstract concept
            {
                new Claim(ClaimTypes.Name, "Samrat"),
                new Claim(ClaimTypes.Email, "samrat@hotmail.com"),
                new Claim(ClaimTypes.DateOfBirth, "30/12/2000"),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.Role, "AdminTwo"),
               // new Claim(DynamicPolicies.SecurityLevel, "7"),
                new Claim("Grandma.Says", "Very nice boy."),
            };

            var licenseClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "Samrat Thapa"),
                new Claim("DrivingLicense", "A+"),
            };

            var grandmaIdentity = new ClaimsIdentity(grandmaClaims, "Grandma Identity");
            var licenseIdentity = new ClaimsIdentity(licenseClaims, "Government");
            //will accept an array of claims identity.. identity can be many once we have been authenticated
            var userPrincipal = new ClaimsPrincipal(new[] { grandmaIdentity, licenseIdentity });

            HttpContext.SignInAsync(userPrincipal);
            return RedirectToAction("Index"); // when everything is done then the user is redirected to index
        }

        public async Task<IActionResult> DoStuff(
            [FromServices] IAuthorizationService authorizationService)
        {
            // we are doing stuff here

            var builder = new AuthorizationPolicyBuilder("Schema");
            var customPolicy = builder.RequireClaim("Hello").Build();

            var authResult = await authorizationService.AuthorizeAsync(User, customPolicy);

            if (authResult.Succeeded)
            {
                return View("Index");
            }

            return View("Index");
        }
    }
}
