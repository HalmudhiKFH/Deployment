using System;
using System.Collections.Generic;

#nullable disable

namespace LLPA_Website.ModelsE
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Type { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string MobileNo { get; set; }
        public string Address { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
