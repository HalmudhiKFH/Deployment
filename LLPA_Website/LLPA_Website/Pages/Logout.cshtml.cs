using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace LLPA_Website.Pages
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        public async Task OnGet()
        {
            if (!string.IsNullOrWhiteSpace(User.Identity.Name))
            {
                await HttpContext.SignOutAsync();
            }
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            await HttpContext.SignOutAsync();
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToPage();
            }
        }
    }
}
