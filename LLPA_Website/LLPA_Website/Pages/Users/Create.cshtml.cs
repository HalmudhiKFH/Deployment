using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LLPA_Website.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using LLPA_Website.Helpers;
using Microsoft.AspNetCore.Authorization;
using LLPA_Website.ModelsE;

namespace LLPA_Website.Pages.Users
{
    public class CreateModel : PageModel
    {
        public IActionResult OnGet()
        {
            ViewData["UserTypes"] = new SelectList(new List<SelectListItem> { new SelectListItem { Text = "Admin", Value = "1" }, new SelectListItem { Text = "Client", Value = "2" } }, "Value", "Text");
            return Page();
        }

        [BindProperty]
        public ModelsE.User user { get; set; }


        private readonly HttpClient _httpClient;
        public CreateModel(IHttpClientFactory clientFactory)
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
                    return Page();


                }

            }
            catch
            {
                return RedirectToPage("./Index");

            }
        }
    }
}
