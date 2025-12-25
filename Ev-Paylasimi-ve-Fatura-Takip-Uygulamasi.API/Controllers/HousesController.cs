using AutoMapper;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.API.Filters;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.DTOs;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.DTOs.UpdateDTOs;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Models;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HousesController : CustomBaseController
    {
        private readonly IHouseService _houseService;
        private readonly IHouseMemberService _houseMemberService;
        private readonly IMapper _mapper;

        public HousesController(IHouseService houseService, IMapper mapper, IHouseMemberService houseMemberService)
        {
            _houseService = houseService;
            _mapper = mapper;
            _houseMemberService = houseMemberService;
        }

        // Tüm evleri listeler
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var houses = _houseService.GetAll();
            var dtos = _mapper.Map<List<HouseDto>>(houses).ToList();

            return CreateActionResult(CustomResponseDto<List<HouseDto>>.Success(200, dtos));
        }
        // pagination

        // Id'ye göre ev detayını getirir
        [ServiceFilter(typeof(NotFoundFilter<House>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var house = await _houseService.GetByIdAsync(id);
            var houseDto = _mapper.Map<HouseDto>(house);
            return CreateActionResult(CustomResponseDto<HouseDto>.Success(200, houseDto));
        }

        // Belirtilen evi siler (Soft delete)
        [ServiceFilter(typeof(NotFoundFilter<House>))]
        [HttpGet("[action]")]
        public async Task<IActionResult> Remove(int id)
        {
            int userId = GetUserFromToken();

            var house = await _houseService.GetByIdAsync(id);
            house.UpdateBy = userId;

            _houseService.ChangeStatus(house);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        // Yeni bir ev kaydı oluşturur
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Save(HouseDto houseDto)
        {
            int userId = GetUserFromToken();

            var processedEntity = _mapper.Map<House>(houseDto);

            processedEntity.UpdateBy = userId;
            processedEntity.CreateBy = userId;


            var house = await _houseService.AddAsync(processedEntity);

            // Yaratılan eve kullanıcıyı admin olarak ekler
            var houseMember = new HouseMember
            {
                UserId = userId,
                HouseId = house.Id,
                Role = "Admin",
                CreateBy = userId,
                UpdateBy = userId
            };

            await _houseMemberService.AddAsync(houseMember);

            var houseResponseDto = _mapper.Map<HouseDto>(house);

            return CreateActionResult(CustomResponseDto<HouseDto>.Success(201, houseResponseDto));
        }

        // Mevcut bir evi günceller
        [HttpPut]
        public async Task<IActionResult> Update(HouseUpdateDto houseDto)
        {
            int houseId = 1;
            var currentHouse = await _houseService.GetByIdAsync(houseDto.Id);

            currentHouse.UpdateBy = houseId;
            currentHouse.HouseName = houseDto.HouseName;
            currentHouse.Description = houseDto.Description;
            currentHouse.Address = houseDto.Address;

            _houseService.Update(currentHouse);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
