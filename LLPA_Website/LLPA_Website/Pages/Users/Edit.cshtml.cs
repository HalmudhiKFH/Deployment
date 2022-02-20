using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LLPA_Website.Models;
using System.Net.Http;
using System.Net.Http.Json;
using Newtonsoft.Json;
using System.Text;
using LLPA_Website.Helpers;
using Microsoft.AspNetCore.Authorization;
using LLPA_Website.Models.ViewModels;

namespace LLPA_Website.Pages.Users
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        [BindProperty]
        public UserVM user { get; set; }

        private readonly HttpClient _httpClient;
        public EditModel(IHttpClientFactory clientFactory)
        {
            user = new UserVM();
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
                var usr = JsonConvert.DeserializeObject<User>(result);
                if (usr == null)
                {
                    return NotFound();
                }

                user.Id = usr.Id;
                user.Email = usr.Email;
                user.Username = usr.Username;
                user.Type = usr.Type;
                user.PasswordHash = usr.Password;
                user.CreatedAt = usr.CreatedAt;
                user.ModifiedAt = usr.ModifiedAt;
            }
            else
            {
                ModelState.AddModelError("", "Invalid edit attempt.");
            }

            ViewData["UserTypes"] = new SelectList(new List<SelectListItem> { new SelectListItem { Text = "Admin", Value = "1" }, new SelectListItem { Text = "Client", Value = "2" } }, "Value", "Text", user.Type);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            User usr = new User
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username,
                Type = user.Type,
                CreatedAt = user.CreatedAt
            };

            if (!string.IsNullOrEmpty(user.Password) && !string.IsNullOrWhiteSpace(user.Password))
            {
                usr.Password = PasswordHelper.SimpleHashPassword(user.Password);
            }
            else
            {
                usr.Password = user.PasswordHash;
            }

            var result = await _httpClient.PutAsync("api/users/" + usr.Id, new StringContent(JsonConvert.SerializeObject(usr), Encoding.UTF8, "application/json"));

            if (result.IsSuccessStatusCode)
            {
                return RedirectToPage("./Index");
            }

            ViewData["UserTypes"] = new SelectList(new List<SelectListItem> { new SelectListItem { Text = "Admin", Value = "1" }, new SelectListItem { Text = "Client", Value = "2" } }, "Value", "Text", user.Type);

            return Page();
        }
    }
}
