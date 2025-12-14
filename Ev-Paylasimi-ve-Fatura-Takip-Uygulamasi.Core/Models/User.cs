using System;
using System.Collections.Generic;
using System.Text;

namespace Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Models
{
    public class User:BaseEntity
    {
        public required string Name { get; set; }
        public required string Email { get; set; }

        public required byte[] PasswordSalt { get; set; }
        public required byte[] PasswordHash { get; set; }
        public ICollection<ExpenseShare> ExpenseShares { get; set; }
        public ICollection<HouseMember> HouseMembers { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }
}
