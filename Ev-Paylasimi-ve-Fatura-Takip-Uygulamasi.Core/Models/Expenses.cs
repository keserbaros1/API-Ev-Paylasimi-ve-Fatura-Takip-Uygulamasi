using System;
using System.Collections.Generic;
using System.Text;

namespace Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Models
{
    public class Expenses:BaseEntity
    {
        public required int HouseId { get; set; }
        public required int CreateByUserId { get; set; }
        public required string Category{ get; set; }
        public required double Amount { get; set; }
    }
}
