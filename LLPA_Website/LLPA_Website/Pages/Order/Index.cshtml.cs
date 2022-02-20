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
using Microsoft.AspNetCore.Authorization;
using LLPA_Website.ModelsE;

namespace LLPA_Website.Pages.Products
{
    public class CreatePizzaRequest
    {
        public int PizzaId { get; set; }
        public int Quantity { get; set; }
        public decimal Bill { get; set; }

    }
    [Authorize()]
    public class CreateOrderModel : PageModel
    {
        [BindProperty]
        public CreatePizzaRequest pizzaRequest { get; set; }
        [BindProperty]
        public Pizza pizza { get; set; }

        private readonly HttpClient _httpClient;
        public CreateOrderModel(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient("ApiClient");
        }
        public IActionResult OnGet(int? id)
        {
            ViewData["planTypes"] = new SelectList(new List<SelectListItem> { new SelectListItem { Text = "Monthly", Value = "Monthly" }, new SelectListItem { Text = "Quarterly", Value = "Quarterly" }
            , new SelectListItem { Text = "Yearly", Value = "Yearly" }}, "Value", "Text");
            if (id == null)
            {
                return NotFound();
            }

            using(var _context = new PizzaAppContext())
            {
                pizza = _context.Pizzas.Where(p => p.Id == id).FirstOrDefault();

                if (pizza == null)
                {
                    ModelState.AddModelError("", "Invalid edit attempt.");
                }
            }

            ViewData["UserTypes"] = new SelectList(new List<SelectListItem> { new SelectListItem { Text = "Admin", Value = "1" }, new SelectListItem { Text = "Client", Value = "2" } }, "Value", "Text", pizza.Type);

            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            try
            {
                using (var _context = new PizzaAppContext())
                {
                    var userId = _context.Users.Where(x => x.Email == User.Identity.Name).Select(x => x.Id).FirstOrDefault();
                    var createLend = new ModelsE.Order
                    {
                        PizzaId = pizza.Id,
                        UserId = userId,
                        Quantity = pizza.Quantity,
                        Bill = pizza.Quantity * pizza.Price,
                        Status = "Pending"
                    };
                    _context.Orders.Add(createLend);
                    await _context.SaveChangesAsync();
                    return RedirectToPage("/LendingApproval/Index");
                }
            }
            catch
            {
                ViewData["Message"] = $"Item Is Out Of Stock: {pizza.Type} .";
            }

            return RedirectToPage("/LendingApproval/Index");
        }
    }
}
