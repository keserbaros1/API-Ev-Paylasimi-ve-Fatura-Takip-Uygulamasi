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
    public class ExpensesController : CustomBaseController
    {
        private readonly IExpenseService _expenseService;
        private readonly IMapper _mapper;

        public ExpensesController(IExpenseService expenseService, IMapper mapper)
        {
            _expenseService = expenseService;
            _mapper = mapper;
        }

        // Belirli bir evin harcamaları
        [HttpGet("ByHouse/{houseId}")]
        public async Task<IActionResult> ByHouse(int houseId)
        {
            var expenses = _expenseService.GetByHouse(houseId);
            var dtos = _mapper.Map<List<ExpenseDto>>(expenses);
            return CreateActionResult(CustomResponseDto<List<ExpenseDto>>.Success(200, dtos));
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var expenses = _expenseService.GetAll();
            var dtos = _mapper.Map<List<ExpenseDto>>(expenses).ToList();

            return CreateActionResult(CustomResponseDto<List<ExpenseDto>>.Success(200, dtos));
        }
        // pagination

        // böyle bir id var mı diye ilk soruyor yoksa direkt 404 döndürüyor 
        [ServiceFilter(typeof(NotFoundFilter<Expense>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var expense = await _expenseService.GetByIdAsync(id);
            var expenseDto = _mapper.Map<ExpenseDto>(expense);
            return CreateActionResult(CustomResponseDto<ExpenseDto>.Success(200, expenseDto));
        }

        [ServiceFilter(typeof(NotFoundFilter<Expense>))]
        [HttpGet("[action]")]
        public async Task<IActionResult> Remove(int id)
        {
            // get expense from token
            int expenseId = 1;

            var expense = await _expenseService.GetByIdAsync(id);
            expense.UpdateBy = expenseId;

            _expenseService.ChangeStatus(expense);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpPost]
        public async Task<IActionResult> Save(ExpenseDto expenseDto)
        {
            int expenseId = 1;

            var processedEntity = _mapper.Map<Expense>(expenseDto);

            processedEntity.UpdateBy = expenseId;
            processedEntity.CreateBy = expenseId;

            var expense = await _expenseService.AddAsync(processedEntity);

            var expenseResponseDto = _mapper.Map<ExpenseDto>(expense);

            return CreateActionResult(CustomResponseDto<ExpenseDto>.Success(201, expenseResponseDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(ExpenseUpdateDto expenseDto)
        {
            int expenseId = 1;
            var currentExpense = await _expenseService.GetByIdAsync(expenseDto.Id);

            currentExpense.UpdateBy = expenseId;
            currentExpense.Category = expenseDto.Category;
            currentExpense.Amount = expenseDto.Amount;

            _expenseService.Update(currentExpense);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
