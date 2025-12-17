using System;
using System.Collections.Generic;
using System.Text;

namespace Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Models
{
    public class HouseMember:BaseEntity
    {
        public required int HouseId { get; set; }
        public required int UserId { get; set; }
        public required string Role { get; set; }
        public House House{ get; set; }
        public User User { get; set; }
    }
}
