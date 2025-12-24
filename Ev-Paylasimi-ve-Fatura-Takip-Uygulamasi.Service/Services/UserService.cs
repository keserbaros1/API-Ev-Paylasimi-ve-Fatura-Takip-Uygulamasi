using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.DTOs;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Models;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Repositories;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Services;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.UnitOfWorks;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Service.Hashing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Service.Services
{
    public class UserService(IGenericRepository<User> repository, IUnitOfWorks unitOfWorks, IUserRepository userRepository, ITokenHandler tokenHandler) : Service<User>(repository, unitOfWorks), IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ITokenHandler _tokenHandler = tokenHandler;

        public User GetByEmail(string email)
        {
            User user = _userRepository.Where(u => u.Email == email).FirstOrDefault();

            return user ?? user;
        }

        public async Task<Token> Login(UserLoginDto userLoginDto)
        {
            Token token = new Token();
            var user = GetByEmail(userLoginDto.Email);
            if (user == null)
            {
                return null;
            }

            var result = false;

            result = HashingHelper.VerifyPasswordHash(userLoginDto.Password, user.PasswordHash, user.PasswordSalt);

            //List<HouseMember> roles = new List<HouseMember>();

            // get roles todo

            if (result)
            {
                token = _tokenHandler.CreateToken(user);
                //token = _tokenHandler.CreateToken(user, roles);
                return token;
            }
            return null;


        }
    }
}
