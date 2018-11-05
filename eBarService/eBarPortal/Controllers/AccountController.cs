using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using eBarPortal.Models.Account;
using eBarPortal.APIHelpers;
using eBarPortal.Extensions;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace eBarPortal.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        [TempData]
        public string ErrorMessage { get; set; }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                UserSessionApiHelper sessionApiHelper = new UserSessionApiHelper();

                var sessionResponse = await sessionApiHelper.Login(model.Email, model.Password);

                if (sessionResponse != null)
                {
                    var claims = new List<Claim>
                    {
                       new Claim(ClaimTypes.Name, sessionResponse.UserDetails.Name),
                       new Claim(ClaimTypes.Role, sessionResponse.UserDetails.UserTypeId.ToString()),
                       new Claim(ClaimTypes.NameIdentifier, sessionResponse.UserDetails.Username),
                       new Claim(ClaimTypes.Email, sessionResponse.UserDetails.Email),
                   
                       new Claim("HasRestaurantConfigured", sessionResponse.UserDetails.HasRestaurantConfigured.HasValue ? sessionResponse.UserDetails.HasRestaurantConfigured.Value.ToString() : string.Empty),
                       new Claim("SessionKey", sessionResponse.SessionKey),
                    };
                    var props = new AuthenticationProperties
                    {
                        IsPersistent = false,
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(30)
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await AuthenticationHttpContextExtensions.SignInAsync(HttpContext, new ClaimsPrincipal(identity), props);

                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.SetUserObject("LoggedUser", null);
            HttpContext.Session.SetString("SessionKey", null);
            return RedirectToAction(nameof(AccountController.Login), "Account");
        }
    }
}