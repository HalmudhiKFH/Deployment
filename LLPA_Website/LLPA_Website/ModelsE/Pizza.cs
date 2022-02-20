using System;
using System.Collections.Generic;

#nullable disable

namespace LLPA_Website.ModelsE
{
    public partial class Pizza
    {
        public Pizza()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public string Type { get; set; }
        public int? Quantity { get; set; }
        public int? Price { get; set; }
        public int? Sold { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
