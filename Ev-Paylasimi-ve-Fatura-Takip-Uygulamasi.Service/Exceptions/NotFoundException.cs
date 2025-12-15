using System;
using System.Collections.Generic;
using System.Text;

namespace Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Service.Exceptions
{
    public class NotFoundException(string message) : Exception(message)
    {
    }
}
