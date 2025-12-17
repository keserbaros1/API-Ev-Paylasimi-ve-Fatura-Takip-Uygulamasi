using System;
using System.Collections.Generic;
using System.Text;

namespace Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.DTOs.UpdateDTOs
{
    public class ExpenseUpdateDto
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public double Amount { get; set; }
    }
}
