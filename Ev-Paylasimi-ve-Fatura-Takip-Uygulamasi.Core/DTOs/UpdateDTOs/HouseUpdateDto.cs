using System;
using System.Collections.Generic;
using System.Text;

namespace Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.DTOs.UpdateDTOs
{
    public class HouseUpdateDto
    {
        public int Id { get; set; }
        public string HouseName { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
    }
}
