using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricalShop.Models
{
    public class Member
    {
        public string Id { get; set; }
        public string Username { get; set; }

        public string Password { get; set; }
        public string Email { get; set; }

        public bool Gender { get; set; }

        public string PhoneNumber { get; set; }

        public List<Role> Roles { get; set; }
    }
}
