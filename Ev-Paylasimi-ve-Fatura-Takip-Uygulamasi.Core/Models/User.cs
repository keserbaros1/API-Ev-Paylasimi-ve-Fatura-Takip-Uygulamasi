using System;
using System.Collections.Generic;
using System.Text;

namespace Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Models
{
    public class User:BaseEntity
    {
        public required string Name { get; set; }
        public required string Email { get; set; }

        public required byte[] PasswordSalt { get; set; }
        public required byte[] PasswordHash { get; set; }
    }
}
