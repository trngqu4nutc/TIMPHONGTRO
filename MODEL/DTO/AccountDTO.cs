using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.DTO
{
    public class AccountDTO
    {
        public int AccountId { get; set; }
        public string Fullname { get; set; }
        public string PhoneNum { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public decimal Money { get; set; }
    }
}
