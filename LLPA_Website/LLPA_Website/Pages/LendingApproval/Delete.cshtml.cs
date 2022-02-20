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
using System.Net.Mail;
using System.Net;

namespace LLPA_Website.Pages.OutgoingGoods
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        [BindProperty]
        public ModelsE.Order outgoingGood { get; set; }


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

            using(var _context = new PizzaAppContext())
            {
                var lend = await _context.Orders.Where(l => l.Id == id).FirstOrDefaultAsync();
                var userEmail = await _context.Orders.Where(l => l.Id == id).Select(lu => lu.User.Email).FirstOrDefaultAsync();
                if (lend == null)
                {
                    return Redirect("~/Error");

                }
                lend.Status = "Approved";
                _context.SaveChanges();
                DoSendEmail(userEmail, lend.Bill);
            }


            return Page();
        }
        protected void DoSendEmail(string email, decimal? lend)
        {
            var mail = new MailMessage
            {
                From = new MailAddress("hamzakhan.beetech@gmail.com"),
                To = { email },
                Subject = "Our Lending PlatForm",
                IsBodyHtml = true,
                Body = "Thanks for requesting for lend. We have reviewed your biodata <br> and Allotted you lend of 15000 "+lend
            };

            var smtp = new SmtpClient("smtp.gmail.com", 587);

            smtp.Credentials = new NetworkCredential("hamzakhan@gmail.com", "asdf");
            smtp.EnableSsl = true;

            smtp.Send(mail);
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var response = await _httpClient.GetAsync("api/outgoinggoods/" + id);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                outgoingGood = JsonConvert.DeserializeObject<ModelsE.Order>(result);
                if (outgoingGood != null)
                {
                    await _httpClient.DeleteAsync("api/outgoinggoods/" + id);
                    return RedirectToPage("./Index");
                }
            }
            else
            {
                return Redirect("~/Error");
            }

            return Page();
        }
    }
}
