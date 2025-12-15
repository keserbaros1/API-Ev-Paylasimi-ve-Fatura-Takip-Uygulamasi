using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Models;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Repositories;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Services;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Service.Services
{
    public class UserService(IGenericRepository<User> repository, IUnitOfWorks unitOfWorks, IUserRepository userRepository) : Service<User>(repository, unitOfWorks), IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
    }
}
