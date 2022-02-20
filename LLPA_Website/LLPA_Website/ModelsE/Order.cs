using System;
using System.Collections.Generic;

#nullable disable

namespace LLPA_Website.ModelsE
{
    public partial class Order
    {
        public int Id { get; set; }
        public int PizzaId { get; set; }
        public int UserId { get; set; }
        public int? Quantity { get; set; }
        public decimal? Bill { get; set; }
        public string Status { get; set; }

        public virtual Pizza Pizza { get; set; }
        public virtual User User { get; set; }
    }
}
