using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricalShop.Models
{
    public class InvoiceDetail
    {
        public string InvoiceId { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int Price { get; set; }

        public short Quantity { get; set; }
    }
}
