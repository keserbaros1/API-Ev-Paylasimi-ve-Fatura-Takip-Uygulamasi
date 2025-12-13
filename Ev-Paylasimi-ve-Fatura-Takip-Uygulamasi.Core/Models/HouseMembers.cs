using System;
using System.Collections.Generic;
using System.Text;

namespace Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Models
{
    public class HouseMembers:BaseEntity
    {
        public required int HouseID { get; set; }
        public required int UserID { get; set; }
        public required string Role { get; set; }
    }
}
