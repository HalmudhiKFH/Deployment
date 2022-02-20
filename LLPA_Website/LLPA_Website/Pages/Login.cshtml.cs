using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using LLPA_Website.Helpers;
using LLPA_Website.Models;
using System.Net.Http;
using System.Net.Http.Json;
using Newtonsoft.Json;
using LLPA_Website.ModelsE;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace LLPA_Website.Pages
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        private readonly HttpClient _httpClient;
        public LoginModel(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient("ApiClient");
        }


        public IActionResult OnGet(string returnUrl = null)
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/Index");
            }
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }
            ReturnUrl = returnUrl;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/Index");

            if (ModelState.IsValid)
            {
                var pass = PasswordHelper.SimpleHashPassword(Input.Password);
                using(var _context = new PizzaAppContext())
                {
                    var user = await _context.Users.FirstOrDefaultAsync(m => m.Email == Input.Email && m.Password == pass);

                    if (user == null)
                    {
                        return NotFound();
                    }

                    if (user != null)
                    {
                        var role = Roles.RoleNames[user.Type];

                        var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Name, Input.Email),
                        new Claim(ClaimTypes.Role, role)
                    };
                        var claimsIdentity = new ClaimsIdentity(
                            claims,
                            CookieAuthenticationDefaults.AuthenticationScheme);

                        var properties = new AuthenticationProperties()
                        {
                            IsPersistent = Input.RememberMe,
                            RedirectUri = "/Index",
                        };

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);

                        return LocalRedirect(returnUrl);
                    }

                }
            }
            ModelState.AddModelError("", "Invalid login attempt.");
            return Page();

        }
    }
}
