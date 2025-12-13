using System;
using System.Collections.Generic;
using System.Text;

namespace Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Models
{
    public class ExpenseShares:BaseEntity
    {
        public required int ExpenseId { get; set; }
        public required int UserId { get; set; }
        public required double ShareAmount{ get; set; }
    }
}
