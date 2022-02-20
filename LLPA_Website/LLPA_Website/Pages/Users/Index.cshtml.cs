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
using LLPA_Website.ModelsE;
using Microsoft.EntityFrameworkCore;

namespace LLPA_Website.Pages.Users
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        public IList<ModelsE.User> Users { get; set; }

        public IndexModel(IHttpClientFactory clientFactory)
        {
        }

        public async Task OnGetAsync()
        {
            using (var _context = new PizzaAppContext())
            {
                Users = await _context.Users.ToListAsync();

            }
        }
    }
}
