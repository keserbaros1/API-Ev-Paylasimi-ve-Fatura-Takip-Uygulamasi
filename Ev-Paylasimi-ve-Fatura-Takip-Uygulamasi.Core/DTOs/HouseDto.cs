using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.DTOs
{
    public class HouseDto:BaseDto
    {
        public required string HouseName { get; set; }
        public string? Description { get; set; }
        public required string Address { get; set; }

        public List<Expenses> Expenses { get; set; }
        public List<HouseMember> HouseMembers { get; set; }
        public List<Payment> Payments { get; set; }
    }
}
