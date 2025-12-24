using AutoMapper;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.API.Filters;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.DTOs;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.DTOs.UpdateDTOs;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Models;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Core.Services;
using Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.Service.Hashing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ev_Paylasimi_ve_Fatura_Takip_Uygulamasi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : CustomBaseController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        //[HttpGet("Dashboard")]
        //public async Task<IActionResult> Dashboard()
        //{
        //    int userId = 1;
        //    var dashboard = await _dashboardService.GetDashboardAsync(userId);
        //    return CreateActionResult(CustomResponseDto<UserDashboardDto>.Success(200, dashboard));
        //}

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            Token token = await _userService.Login(userLoginDto);

            if (token == null)
            {
               return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(401, "Eposta veya parola hatalı"));
            }

             return CreateActionResult(CustomResponseDto<Token>.Success(200, token));
        }


        [HttpGet]
        public async Task<IActionResult> All()
        {
            var users = _userService.GetAll();
            var dtos = _mapper.Map<List<UserDto>>(users).ToList();

            return CreateActionResult(CustomResponseDto<List<UserDto>>.Success(200, dtos));
        }
        // pagination

        // böyle bir id var mı diye ilk soruyor yoksa direkt 404 döndürüyor 
        [ServiceFilter(typeof(NotFoundFilter<User>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            var userDto = _mapper.Map<UserDto>(user);
            return CreateActionResult(CustomResponseDto<UserDto>.Success(200, userDto));
        }

        [ServiceFilter(typeof(NotFoundFilter<User>))]
        [HttpGet("[action]")]
        public async Task<IActionResult> Remove(int id)
        {
            // get user from token
            int userId = 1;

            var user = await _userService.GetByIdAsync(id);
            user.UpdateBy = userId;

            _userService.ChangeStatus(user);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpPost]
        public async Task<IActionResult> Save(UserDto userDto)
        {
            int userId = GetUserFromToken();

            var processedEntity = _mapper.Map<User>(userDto);

            processedEntity.UpdateBy = userId;
            processedEntity.CreateBy = userId;

            byte[] passwordHash, passwordSalt;
            
            HashingHelper.CreatePasswordHash(userDto.Password, out passwordHash, out passwordSalt);

            processedEntity.PasswordHash = passwordHash;
            processedEntity.PasswordSalt = passwordSalt;

            var user = await _userService.AddAsync(processedEntity);

            var userResponseDto = _mapper.Map<UserDto>(user);

            return CreateActionResult(CustomResponseDto<UserDto>.Success(201, userResponseDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UserUpdateDto userDto)
        {
            int userId = 1;
            var currentUser = await _userService.GetByIdAsync(userDto.Id);

            currentUser.UpdateBy = userId;
            currentUser.Name = userDto.Name;

            _userService.Update(currentUser);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
