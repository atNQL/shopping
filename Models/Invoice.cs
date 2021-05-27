using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricalShop.Models
{
    public class Invoice
    {

        public string Id { get; set; }

        public string MemberId { get; set; }

        public string MemberName { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public int PayMethod { get; set; }

        public List<InvoiceDetail> InvoiceDetail { get; set; }

    }
}
