using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using LLPA_Website.Helpers;
using LLPA_Website.ModelsE;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LLPA_Website.Pages
{
    public class RegisterModel : PageModel
    {
        public IActionResult OnGet()
        {
            ViewData["UserTypes"] = new SelectList(new List<SelectListItem> { new SelectListItem { Text = "Admin", Value = "1" }, new SelectListItem { Text = "Client", Value = "2" } }, "Value", "Text");
            return Page();
        }

        [BindProperty]
        public ModelsE.User user { get; set; }


        private readonly HttpClient _httpClient;
        public RegisterModel(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient("ApiClient");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            user.Password = PasswordHelper.SimpleHashPassword(user.Password);


            try
            {
                using (var _context = new PizzaAppContext())
                {
                    user.CreatedAt = DateTime.UtcNow;
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();

                    ViewData["UserTypes"] = new SelectList(new List<SelectListItem> { new SelectListItem { Text = "Admin", Value = "1" }, new SelectListItem { Text = "Client", Value = "2" } }, "Value", "Text", user.Type);
                    return RedirectToPage("./Index");


                }

            }
            catch
            {
                return RedirectToPage("./Index");

            }
        }
    }
}
