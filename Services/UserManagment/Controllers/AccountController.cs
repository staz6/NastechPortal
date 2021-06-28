using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagment.Dto;
using UserManagment.Entities;
using UserManagment.Helper;
using UserManagment.Interface;

namespace UserManagment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAccountRepository _accountRepo;
        public AccountController(IAccountRepository accountRepo, IMapper mapper)
        {
            _accountRepo = accountRepo;
            _mapper = mapper;
        }

        
        [HttpPost("register")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        public async Task<ActionResult> Register(RegisterDto model)
        {
            if (model == null) return BadRequest();
            try
            {
                var resultStatusCode = await _accountRepo.RegisterUser(model);
                return new ObjectResult(new ApiErrorResponse(resultStatusCode));
            }
            catch (Exception)
            {
                return new ObjectResult(new ApiErrorResponse(ErrorStatusCode.InvalidRequest));
            }


            //return Accepted();

        }

        [HttpPost("login")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            if(model == null)
            {
                return new ObjectResult(new ApiErrorResponse(ErrorStatusCode.InvalidRequest));
            }
            try{

                var result =await _accountRepo.Login(model);
                return new UserDto{
                Name= result.Name,
                Token= result.Token
            };
            }
            catch(Exception ex)
            {
                return new ObjectResult(new ApiErrorResponse(200,ex.Message));
            }

        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = ""+Roles.Employee+","+Roles.Admin+"")]
        [HttpGet("getCurrentUser")]
        public async Task<ActionResult<UsersInfoDto>> GetCurrentUser()
        {
            
            try{
                var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var user = await _accountRepo.getCurrentUser(email);
            return user;
            }
            catch{
                return new ObjectResult(new ApiErrorResponse(ErrorStatusCode.NotAuthorize));
            }
            
        }
    }
}