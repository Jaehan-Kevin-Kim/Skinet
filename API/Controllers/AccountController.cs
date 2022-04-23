using System;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, IMapper mapper)
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _signInManager = signInManager;
            _userManager = userManager;
        }



        // 아래는 token 만들때 생성했던 claim 을 이용해서 user를 찾는 API.
        // 아래와 같이 Authorize가 추가 된 경우는 headers에 authorization 항목에 value(값)을 추가 해 줘야 함.
        // [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            // var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailFromClaimsPrincipal(User);
            // FindByEmailAsync(email);

            return new UserDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                DisplayName = user.DisplayName
            };
        }

        // 아래는 user 등록을 하는 경우 이미 존재하는 email이 있는지 여부를 확인 하는 API. 여기서 직접적으로 처리를 한다기 보다, email이 존재 하는 경우 true를 넘겨줘서 이걸 client 측에서 처리 할 예정.
        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }


        // 아래는 UserAddress를 찾는 API.
        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            // var email = User.FindFirstValue(ClaimTypes.Email);

            // Console.WriteLine(User);
            // var user = await _userManager.FindByEmailAsync(email);
            var user = await _userManager.FindUserByClamisPrincipalWithAddressAsync(User);
            // Console.WriteLine(user);

            // return user.Address;
            return _mapper.Map<Address, AddressDto>(user.Address);
        }

        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto addressDto)
        {
            var user = await _userManager.FindUserByClamisPrincipalWithAddressAsync(User);

            // 아래는 automapper를 이용해서 자동으로 properties를 업데이트 하는 코드 (addressDto에서 address로 자동 변경 하기)
            user.Address = _mapper.Map<AddressDto, Address>(addressDto);

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded) return Ok(_mapper.Map<Address, AddressDto>(user.Address));

            return BadRequest("Problem updating the user");

        }




        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null) return Unauthorized(new ApiResponse(401));
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));
            return new UserDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                DisplayName = user.DisplayName
            };
            // var user = await _signInManager.CheckPasswordSignInAsync()
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {

            // 아래는 혹시 email address가 이미 존재하는 경우 바로 return을 해 주는 코드 임
            if (CheckEmailExistsAsync(registerDto.Email).Result.Value)
            {
                return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = new[] { "Email address is in use" } });
            }

            var user = new AppUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Email,
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded) return BadRequest(new ApiResponse(400));

            return new UserDto
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = _tokenService.CreateToken(user),
            };
        }
    }
}