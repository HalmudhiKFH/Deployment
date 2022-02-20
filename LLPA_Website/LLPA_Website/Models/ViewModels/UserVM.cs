using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LLPA_Website.Models.ViewModels
{
    public class UserVM
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Type { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
