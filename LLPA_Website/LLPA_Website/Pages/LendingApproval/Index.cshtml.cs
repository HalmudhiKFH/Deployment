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

namespace LLPA_Website.Pages.OutgoingGoods
{
    public class PendingLends
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PizzaId { get; set; }
        public decimal? Bill { get; set; }
        public int? Quantity { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PizzaName { get; set; }
        public string Status { get; set; }

    }

    [Authorize]
    public class IndexModel : PageModel
    {
        public IList<ModelsE.Order> PendingLends { get; set; }
        public IList<PendingLends> PendingLendList { get; set; }

        private readonly HttpClient _httpClient;
        public IndexModel(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient("ApiClient");
        }

        public async Task OnGetAsync()
        {
            using (var _context = new PizzaAppContext())
            {
                PendingLendList = await _context.Orders.Where(l => l.Status == "Pending").Select(x => new PendingLends{
                    Id = x.Id,
                    Status = x.Status,
                    UserName = x.User.Username,
                    Email = x.User.Email,
                    Quantity = x.Quantity,
                    Bill = x.Bill,
                    PizzaName = x.Pizza.Type
                }).ToListAsync();
            }

        }
    }
}
