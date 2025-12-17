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
    public class ExpenseSharesController : CustomBaseController
    {
        private readonly IExpenseShareService _expenseShareService;
        private readonly IMapper _mapper;

        public ExpenseSharesController(IExpenseShareService expenseShareService, IMapper mapper)
        {
            _expenseShareService = expenseShareService;
            _mapper = mapper;
        }

        // Kullanıcının borçları
        [HttpGet("MyDebts")]
        public async Task<IActionResult> MyDebts()
        {
            int userId = 1;
            var debts = _expenseShareService.GetExpenseSharesByUser(userId);
            var dtos = _mapper.Map<List<ExpenseShareDto>>(debts);
            return CreateActionResult(CustomResponseDto<List<ExpenseShareDto>>.Success(200, dtos));
        }


        [HttpGet]
        public async Task<IActionResult> All()
        {
            var expenseShares = _expenseShareService.GetAll();
            var dtos = _mapper.Map<List<ExpenseShareDto>>(expenseShares).ToList();

            return CreateActionResult(CustomResponseDto<List<ExpenseShareDto>>.Success(200, dtos));
        }


        // pagination

        // böyle bir id var mı diye ilk soruyor yoksa direkt 404 döndürüyor 
        [ServiceFilter(typeof(NotFoundFilter<ExpenseShare>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var expenseShare = await _expenseShareService.GetByIdAsync(id);
            var expenseShareDto = _mapper.Map<ExpenseShareDto>(expenseShare);
            return CreateActionResult(CustomResponseDto<ExpenseShareDto>.Success(200, expenseShareDto));
        }

        [ServiceFilter(typeof(NotFoundFilter<ExpenseShare>))]
        [HttpGet("[action]")]
        public async Task<IActionResult> Remove(int id)
        {
            // get expenseShare from token
            int expenseShareId = 1;

            var expenseShare = await _expenseShareService.GetByIdAsync(id);
            expenseShare.UpdateBy = expenseShareId;

            _expenseShareService.ChangeStatus(expenseShare);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpPost]
        public async Task<IActionResult> Save(ExpenseShareDto expenseShareDto)
        {
            int expenseShareId = 1;

            var processedEntity = _mapper.Map<ExpenseShare>(expenseShareDto);

            processedEntity.UpdateBy = expenseShareId;
            processedEntity.CreateBy = expenseShareId;

            var expenseShare = await _expenseShareService.AddAsync(processedEntity);

            var expenseShareResponseDto = _mapper.Map<ExpenseShareDto>(expenseShare);

            return CreateActionResult(CustomResponseDto<ExpenseShareDto>.Success(201, expenseShareResponseDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(ExpenseShareUpdateDto expenseShareDto)
        {
            int expenseShareId = 1;
            var currentExpenseShare = await _expenseShareService.GetByIdAsync(expenseShareDto.Id);

            currentExpenseShare.UpdateBy = expenseShareId;
            currentExpenseShare.ShareAmount = expenseShareDto.ShareAmount;

            _expenseShareService.Update(currentExpenseShare);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
