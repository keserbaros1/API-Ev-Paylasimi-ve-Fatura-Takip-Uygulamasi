using AutoMapper;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.DTOs;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Service.Mappings
{
    public class MapProfile : Profile
    {
        protected MapProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Expense, ExpenseDto>().ReverseMap();
            CreateMap<ExpenseShare, ExpenseShareDto>().ReverseMap();
            CreateMap<House, HouseDto>().ReverseMap();
            CreateMap<HouseMember, HouseMemberDto>().ReverseMap();
            CreateMap<Payment, PaymentDto>().ReverseMap();
        }
    }
}
