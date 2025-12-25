using System;
using System.Collections.Generic;
using System.Text;

namespace Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.DTOs.UpdateDTOs
{
    public class PaymentUpdateDto
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public int? ExpenseShareId { get; set; }

    }
}
