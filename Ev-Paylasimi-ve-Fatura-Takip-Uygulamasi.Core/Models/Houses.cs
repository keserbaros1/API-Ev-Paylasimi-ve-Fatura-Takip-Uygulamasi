using System;
using System.Collections.Generic;
using System.Text;

namespace Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Models
{
    public class Houses:BaseEntity
    {
        public required string HouseName { get; set; }
        public string? Description { get; set; }
        public required string Address { get; set; }
    }
}
