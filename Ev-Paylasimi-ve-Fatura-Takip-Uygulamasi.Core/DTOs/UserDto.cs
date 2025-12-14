using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.DTOs
{
    public class UserDto: BaseDto
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public List<ExpenseShare> ExpenseShares { get; set; }
        public List<HouseMember> HouseMembers { get; set; }
        public List<Payment> Payments { get; set; }
    }
}
