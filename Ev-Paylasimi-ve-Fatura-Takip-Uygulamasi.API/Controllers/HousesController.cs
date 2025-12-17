using AutoMapper;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.API.Filters;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.DTOs;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.DTOs.UpdateDTOs;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Models;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HousesController : CustomBaseController
    {
        private readonly IHouseService _houseService;
        private readonly IMapper _mapper;

        public HousesController(IHouseService houseService, IMapper mapper)
        {
            _houseService = houseService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var houses = _houseService.GetAll();
            var dtos = _mapper.Map<List<HouseDto>>(houses).ToList();

            return CreateActionResult(CustomResponseDto<List<HouseDto>>.Success(200, dtos));
        }
        // pagination

        // böyle bir id var mı diye ilk soruyor yoksa direkt 404 döndürüyor 
        [ServiceFilter(typeof(NotFoundFilter<House>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var house = await _houseService.GetByIdAsync(id);
            var houseDto = _mapper.Map<HouseDto>(house);
            return CreateActionResult(CustomResponseDto<HouseDto>.Success(200, houseDto));
        }

        [ServiceFilter(typeof(NotFoundFilter<House>))]
        [HttpGet("[action]")]
        public async Task<IActionResult> Remove(int id)
        {
            // get house from token
            int houseId = 1;

            var house = await _houseService.GetByIdAsync(id);
            house.UpdateBy = houseId;

            _houseService.ChangeStatus(house);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpPost]
        public async Task<IActionResult> Save(HouseDto houseDto)
        {
            int houseId = 1;

            var processedEntity = _mapper.Map<House>(houseDto);

            processedEntity.UpdateBy = houseId;
            processedEntity.CreateBy = houseId;

            var house = await _houseService.AddAsync(processedEntity);

            var houseResponseDto = _mapper.Map<HouseDto>(house);

            return CreateActionResult(CustomResponseDto<HouseDto>.Success(201, houseResponseDto));
        }

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
