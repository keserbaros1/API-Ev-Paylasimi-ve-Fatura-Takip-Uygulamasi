using AutoMapper;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.API.Filters;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.DTOs;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.DTOs.UpdateDTOs;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Models;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Services;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HouseMembersController : CustomBaseController
    {
        private readonly IHouseMemberService _houseMemberService;
        private readonly IMapper _mapper;

        public HouseMembersController(IHouseMemberService houseMemberService, IMapper mapper)
        {
            _houseMemberService = houseMemberService;
            _mapper = mapper;
        }

        // Kullanıcının üye olduğu evler
        [HttpGet("MyHouses")]
        public async Task<IActionResult> MyHouses()
        {
            int userId = 1; // token'dan gelecek
            var houses = _houseMemberService.GetHousesByUser(userId);
            var dtos = _mapper.Map<List<HouseDto>>(houses);
            return CreateActionResult(CustomResponseDto<List<HouseDto>>.Success(200, dtos));
        }

        // Belirli evin üyeleri
        [HttpGet("ByHouse/{houseId}")]
        public async Task<IActionResult> Members(int houseId)
        {
            var members = _houseMemberService.GetMembersByHouse(houseId);
            var dtos = _mapper.Map<List<HouseMemberDto>>(members);
            return CreateActionResult(CustomResponseDto<List<HouseMemberDto>>.Success(200, dtos));
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var houseMembers = _houseMemberService.GetAll();
            var dtos = _mapper.Map<List<HouseMemberDto>>(houseMembers).ToList();

            return CreateActionResult(CustomResponseDto<List<HouseMemberDto>>.Success(200, dtos));
        }
        // pagination

        // böyle bir id var mı diye ilk soruyor yoksa direkt 404 döndürüyor 
        [ServiceFilter(typeof(NotFoundFilter<HouseMember>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var houseMember = await _houseMemberService.GetByIdAsync(id);
            var houseMemberDto = _mapper.Map<HouseMemberDto>(houseMember);
            return CreateActionResult(CustomResponseDto<HouseMemberDto>.Success(200, houseMemberDto));
        }

        [ServiceFilter(typeof(NotFoundFilter<HouseMember>))]
        [HttpGet("[action]")]
        public async Task<IActionResult> Remove(int id)
        {
            // get houseMember from token
            int houseMemberId = 1;

            var houseMember = await _houseMemberService.GetByIdAsync(id);
            houseMember.UpdateBy = houseMemberId;

            _houseMemberService.ChangeStatus(houseMember);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpPost]
        public async Task<IActionResult> Save(HouseMemberDto houseMemberDto)
        {
            int houseMemberId = 1;

            var processedEntity = _mapper.Map<HouseMember>(houseMemberDto);

            processedEntity.UpdateBy = houseMemberId;
            processedEntity.CreateBy = houseMemberId;

            var houseMember = await _houseMemberService.AddAsync(processedEntity);

            var houseMemberResponseDto = _mapper.Map<HouseMemberDto>(houseMember);

            return CreateActionResult(CustomResponseDto<HouseMemberDto>.Success(201, houseMemberResponseDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(HouseMemberUpdateDto houseMemberDto)
        {
            int houseMemberId = 1;
            var currentHouseMember = await _houseMemberService.GetByIdAsync(houseMemberDto.Id);

            currentHouseMember.UpdateBy = houseMemberId;
            currentHouseMember.Role = houseMemberDto.Role;

            _houseMemberService.Update(currentHouseMember);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
