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
    public class PaymentsController : CustomBaseController
    {
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;

        public PaymentsController(IPaymentService paymentService, IMapper mapper)
        {
            _paymentService = paymentService;
            _mapper = mapper;
        }

        // Belirli bir evin ödemeleri
        [HttpGet("ByHouse/{houseId}")]
        public async Task<IActionResult> ByHouse(int houseId)
        {
            var payments = _paymentService.GetPaymentsByHouse(houseId);
            var dtos = _mapper.Map<List<PaymentDto>>(payments);
            return CreateActionResult(CustomResponseDto<List<PaymentDto>>.Success(200, dtos));
        }

        // Kullanıcının yaptığı ödemeler
        [HttpGet("MyPayments")]
        public async Task<IActionResult> MyPayments()
        {
            int userId = 1; // token'dan gelecek
            var payments = _paymentService.GetPaymentsByUser(userId);
            var dtos = _mapper.Map<List<PaymentDto>>(payments);
            return CreateActionResult(CustomResponseDto<List<PaymentDto>>.Success(200, dtos));
        }



        [HttpGet]
        public async Task<IActionResult> All()
        {
            var payments = _paymentService.GetAll();
            var dtos = _mapper.Map<List<PaymentDto>>(payments).ToList();

            return CreateActionResult(CustomResponseDto<List<PaymentDto>>.Success(200, dtos));
        }
        // pagination

        // böyle bir id var mı diye ilk soruyor yoksa direkt 404 döndürüyor 
        [ServiceFilter(typeof(NotFoundFilter<Payment>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var payment = await _paymentService.GetByIdAsync(id);
            var paymentDto = _mapper.Map<PaymentDto>(payment);
            return CreateActionResult(CustomResponseDto<PaymentDto>.Success(200, paymentDto));
        }

        [ServiceFilter(typeof(NotFoundFilter<Payment>))]
        [HttpGet("[action]")]
        public async Task<IActionResult> Remove(int id)
        {
            // get payment from token
            int paymentId = 1;

            var payment = await _paymentService.GetByIdAsync(id);
            payment.UpdateBy = paymentId;

            _paymentService.ChangeStatus(payment);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpPost]
        public async Task<IActionResult> Save(PaymentDto paymentDto)
        {
            int paymentId = 1;

            var processedEntity = _mapper.Map<Payment>(paymentDto);

            processedEntity.UpdateBy = paymentId;
            processedEntity.CreateBy = paymentId;

            var payment = await _paymentService.AddAsync(processedEntity);

            var paymentResponseDto = _mapper.Map<PaymentDto>(payment);

            return CreateActionResult(CustomResponseDto<PaymentDto>.Success(201, paymentResponseDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(PaymentUpdateDto paymentDto)
        {
            int paymentId = 1;
            var currentPayment = await _paymentService.GetByIdAsync(paymentDto.Id);

            currentPayment.UpdateBy = paymentId;
            currentPayment.Amount = paymentDto.Amount;

            _paymentService.Update(currentPayment);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
