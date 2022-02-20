using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LLPA_Website.Models;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using LLPA_Website.ModelsE;

namespace LLPA_Website.Pages.Users
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        [BindProperty]
        public Models.User user { get; set; }


        private readonly HttpClient _httpClient;
        public DeleteModel(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient("ApiClient");
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var response = await _httpClient.GetAsync("api/users/" + id);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                user = JsonConvert.DeserializeObject<Models.User>(result);
                if (user == null)
                {
                    return NotFound();
                }
            }
            else
            {
                return Redirect("~/Error");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                using (var _context = new PizzaAppContext())
                {
                    var user = await _context.Users.FindAsync(id);
                    if (user == null)
                    {
                        return NotFound();
                    }

                    _context.Users.Remove(user);
                    await _context.SaveChangesAsync();
                    await _httpClient.DeleteAsync("api/users/" + id);
                    return RedirectToPage("./Index");

                }

            }
            catch
            {
                return Redirect("~/Error");

            }

            var response = await _httpClient.GetAsync("api/users/" + id);

            return Page();
        }
    }
}
