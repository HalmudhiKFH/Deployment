using LLPA_Website.ModelsE;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LLPA_Website.Pages
{
    public class IndexModel : PageModel
    {
        public List<Pizza> PizzaList { get; set; }
        public int? UserCounts { get; set; }
        public int? ProductCounts { get; set; }

        public IndexModel()
        {
        }

        public async Task OnGetAsync()
        {
            using (var _context = new PizzaAppContext())
            {
                PizzaList = _context.Pizzas.ToList();
                UserCounts = _context.Pizzas.Select(l => l.Quantity).FirstOrDefault();
                ProductCounts = _context.Pizzas.Select(l => l.Quantity).FirstOrDefault();
                ProductCounts = ProductCounts == null ? ProductCounts = 0 : ProductCounts;
            }
        }
    }
}
