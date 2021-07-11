using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AttendanceManagement.Dto;
using AttendanceManagement.Entities;
using AttendanceManagement.Helpers;
using AttendanceManagement.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.JsonWebTokens;
/// <summary>
/// Main controller for attendancemanagment microservice
/// </summary>
namespace AttendanceManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttendanceController : ControllerBase
    {
        private readonly IGenericRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<AttendanceController> _logger;
        public AttendanceController(IGenericRepository repo, IMapper mapper, ILogger<AttendanceController> logger)
        {
            _logger = logger;
            _mapper = mapper;
            _repo = repo;
        }

        /// <summary>
        /// Get attendance
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>list of employee attendance List GetAttendanceDto </returns>
        [HttpGet("getAttendance/{userId}")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "" + Roles.Employee + "," + Roles.Admin + "")]
        public async Task<ActionResult<List<GetAttendanceDto>>> getAttendance(string userId)
        {
            if(userId == null) return BadRequest();   
            _logger.LogInformation(userId);
            try{
                var result = await _repo.getUserAttendance(userId);
                var mapobject = _mapper.Map<List<GetAttendanceDto>>(result);
                return Ok(mapobject);
            }
            catch{
                return BadRequest();
            }
            
        }

        /// <summary>
        /// Post Leave Request !IMPORTANT implement custom error handling
        /// </summary>
        /// <param name="UserLeaveDto"></param>
        /// <returns></returns>
        [HttpPost("postLeave")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize(AuthenticationSchemes = "Bearer",Roles =Roles.Employee)]
        public async Task<ActionResult> leaveRequest(UserLeaveDto dto)
        {
            if(dto == null) return BadRequest();
            var userId = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if(dto == null) return BadRequest();
            try{
                var mapObject = _mapper.Map<Leave>(dto);
                mapObject.UserId = userId;
                await _repo.leaveRequest(mapObject);
                return Accepted();
            }
            catch{
                return BadRequest();
            }
            
        }

        /// <summary>
        /// Get all employee leave request
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>

        [HttpGet("getLeave/{userId}")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "" + Roles.Employee + "," + Roles.Admin + "")]
        public async Task<ActionResult<List<GetUserLeaveDto>>> getLeave(string userId)
        {
            if(userId == null) return BadRequest();
            //var userId = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            try{
                var result = await _repo.GetUserLeave(userId);
                var mapObject = _mapper.Map<List<GetUserLeaveDto>>(result);
                return Ok(mapObject);
            }
            catch{
                return BadRequest();
            }
            
        }

        // [HttpGet("getAllLeave")]
        // [ProducesResponseType((int)HttpStatusCode.Accepted)]
        // [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        // [Authorize(AuthenticationSchemes = "Bearer",Roles =Roles.Admin)]
        // public async Task<ActionResult<List<GetAllLeaveRequestDto>>> getAllLeave()
        // {
        //     var result = await _repo.GetAllLeave();
        //     return Ok(result);
        // }

        /// <summary>
        /// Edit leave request for admin
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>

        [HttpPut("editLeave/{id}")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize(AuthenticationSchemes = "Bearer",Roles =Roles.Admin)]
        public ActionResult adminEditLeaveRequest(int id,AdminEditLeaveRequest model)
        {
            if(model==null) return BadRequest();
            try{
                var result = _repo.AdminEditLeaveRequest(id, model);
                return Accepted();
            }
            catch{
                return BadRequest();
            }
            
        }
        
        [HttpGet("getLeaveHistory/{userId}")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "" + Roles.Employee + "," + Roles.Admin + "")]
        public async Task<ActionResult<GetLeaveHistoryDto>> getLeaveHistory(string userId)
        {
            if(userId==null) return BadRequest();
            try{
                var result =await  _repo.getLeaveHistory(userId);
                return result;
            }
            catch{
                return BadRequest();
            }
            
        }

     



    }
}