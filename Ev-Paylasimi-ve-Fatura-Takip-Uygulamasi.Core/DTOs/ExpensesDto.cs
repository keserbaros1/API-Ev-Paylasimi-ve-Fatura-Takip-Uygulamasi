using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.DTOs
{
    public class ExpensesDto: BaseDto
    {
        public required int HouseId { get; set; }
        public required int UserId { get; set; }
        public required string Category { get; set; }
        public required double Amount { get; set; }
        public HouseDto House { get; set; }
        public UserDto User { get; set; }
        public ExpenseShareDto ExpenseShare { get; set; }
    }
}
