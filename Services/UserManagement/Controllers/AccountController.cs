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
        /// <summary>
        /// For the actual implementation goto AccountRepository
        /// </summary>
        private readonly IMapper _mapper;
        private readonly IAccountRepository _accountRepo;
        private readonly ILogger<AccountController> _logger;
        public AccountController(IAccountRepository accountRepo, IMapper mapper, ILogger<AccountController> logger)
        {
            _logger = logger;
            _accountRepo = accountRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Register function only admin have access to it
        /// </summary>

        [HttpPost("register")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = Roles.Admin)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        public async Task<ActionResult> Register(RegisterDto model)
        {
            if (model == null) return BadRequest();
            bool emailChk = await _accountRepo.getUserId(model.Email);
            if(model.Role == Roles.Admin || model.Role ==Roles.Employee)
            {
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
            else{
                return new ObjectResult(new ApiErrorResponse(ErrorStatusCode.InvalidRequest));
            }
            

        }
        /// <summary>
        /// Login function 
        /// </summary>
       

        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        public async Task<ActionResult<string>> Login(LoginDto model)
        {

            if (model == null)
            {
                return new ObjectResult(new ApiErrorResponse(ErrorStatusCode.InvalidRequest));
            }
            if (ModelState.IsValid)
            {
                try
                {

                    var result = await _accountRepo.Login(model);
                    if(result==null) return new ObjectResult(new ApiErrorResponse(ErrorStatusCode.InvalidLogin));

                    return result;
                }
                catch (Exception )
                {
                    return new ObjectResult(new ApiErrorResponse(ErrorStatusCode.InvalidLogin));
                }

            }
            else{
                return new ObjectResult(new ApiErrorResponse(ErrorStatusCode.InvalidRequest));
            }

        }
        /// <summary>
        /// Get Current user only need Authorization Header with Bearer +token 
        /// </summary>
        /// <returns></returns>

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "" + Roles.Employee + "," + Roles.Admin + "")]
        [HttpGet("getCurrentUser")]
        public async Task<ActionResult<UsersInfoDto>> GetCurrentUser()
        {

            try
            {
                string userId = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                var user = await _accountRepo.getCurrentUser(userId);
                return user;
            }
            catch
            {
                return new ObjectResult(new ApiErrorResponse(ErrorStatusCode.NotAuthorize));
            }

        }

        /// <summary>
        /// For employee so he can edit his info
        /// </summary>

        [Authorize(AuthenticationSchemes = "Bearer", Roles = Roles.Employee)]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [HttpPost("editemployee")]
        public async Task<ActionResult> EditEmployeeInfo(EditEmployeeInfoDto model)
        {
            if(model != null)
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
            else{
                return new ObjectResult(new ApiErrorResponse(ErrorStatusCode.InvalidRequest));
            }
            

        }

        /// <summary>
        /// Will be use for admin panel
        /// </summary>
        /// <returns></returns>

        [Authorize(AuthenticationSchemes="Bearer",Roles =Roles.Admin)]
        [HttpGet("getAllEmployee")]
        public async Task<ActionResult<IReadOnlyList<UsersInfoDto>>> getAllEmployee()
        {
            var mapObject = await _accountRepo.GetAllUser();
            var result = _mapper.Map<IReadOnlyList<UsersInfoDto>>(mapObject);
            _logger.LogInformation(result.ToString());

            return Ok(result);
        }

        /// <summary>
        /// Update Salary only accessible to admin
        /// </summary>

        
        [Authorize(AuthenticationSchemes ="Bearer",Roles =Roles.Admin)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [HttpPut("updateSalary/{id}")]
        public async Task<ActionResult> updateSalary(int id,UpdateSalaryDto model)
        {
            if(ModelState.IsValid)
            {
                if(model !=null)
                {
                    await _accountRepo.updateSalary(id,model);
                }
            }
            return BadRequest();
        }
    }
}