using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserManagement.Dto;
using UserManagement.Entities;
using UserManagement.Helper;
using UserManagement.Interface;

namespace UserManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAccountRepository _accountRepo;
        private readonly ILogger<AccountController> _logger;
        public AccountController(IAccountRepository accountRepo, IMapper mapper, ILogger<AccountController> logger)
        {
            _logger = logger;
            _accountRepo = accountRepo;
            _mapper = mapper;
        }


        [HttpPost("register")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        public async Task<ActionResult> Register(RegisterDto model)
        {
            if (model == null) return BadRequest();
            bool emailChk = await _accountRepo.getUserId(model.Email);
            if (emailChk)
            {
                bool chk = await _accountRepo.BiometricCheck(model.BioMetricId);
                if (chk)
                {
                    try
                    {
                        var resultStatusCode = await _accountRepo.RegisterUser(model);
                        return new ObjectResult(new ApiErrorResponse(resultStatusCode));
                    }
                    catch (Exception)
                    {
                        return new ObjectResult(new ApiErrorResponse(ErrorStatusCode.InvalidRequest));
                    }
                }
                else
                {
                    return new ObjectResult(new ApiErrorResponse(ErrorStatusCode.BiometricExist));
                }
            }
            else
            {
                return new ObjectResult(new ApiErrorResponse(ErrorStatusCode.DuplicateEmail));
            }

        }

        [HttpPost("login")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        public async Task<ActionResult<string>> Login(LoginDto model)
        {
            if (model == null)
            {
                return new ObjectResult(new ApiErrorResponse(ErrorStatusCode.InvalidRequest));
            }
            try
            {

                var result = await _accountRepo.Login(model);
                return result;
            }
            catch (Exception ex)
            {
                return new ObjectResult(new ApiErrorResponse(200, ex.Message));
            }

        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "" + Roles.Employee + "," + Roles.Admin + "")]
        [HttpGet("getCurrentUser")]
        public async Task<ActionResult<UsersInfoDto>> GetCurrentUser()
        {

            try
            {
                var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
                var user = await _accountRepo.getCurrentUser(email);
                return user;
            }
            catch
            {
                return new ObjectResult(new ApiErrorResponse(ErrorStatusCode.NotAuthorize));
            }

        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = Roles.Employee)]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [HttpPost("editemployee")]
        public async Task<ActionResult> EditEmployeeInfo(EditEmployeeInfoDto model)
        {
            try
            {
                var userId = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                if (userId == null) return new ObjectResult(new ApiErrorResponse(ErrorStatusCode.NotAuthorize));
                await _accountRepo.EditEditEmployeeInfo(userId, model);
                return Accepted();
            }
            catch
            {
                return new ObjectResult(new ApiErrorResponse(ErrorStatusCode.InvalidRequest));
            }

        }

        //[Authorize(AuthenticationSchemes="Bearer")]
        [HttpGet("getAllEmployee")]
        public async Task<ActionResult<IReadOnlyList<UsersInfoDto>>> getAllEmployee()
        {
            var mapObject = await _accountRepo.GetAllUser();
            var result = _mapper.Map<IReadOnlyList<UsersInfoDto>>(mapObject);
            _logger.LogInformation(result.ToString());
            
            return Ok(result);
        }
    }
}