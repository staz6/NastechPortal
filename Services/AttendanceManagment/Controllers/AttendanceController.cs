using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AttendanceManagment.Dto;
using AttendanceManagment.Entities;
using AttendanceManagment.Helpers;
using AttendanceManagment.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.JsonWebTokens;

namespace AttendanceManagment.Controllers
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

        [HttpGet("getAttendance")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize(AuthenticationSchemes = "Bearer",Roles =Roles.Employee)]
        public async Task<ActionResult<List<GetAttendanceDto>>> getAttendance()
        {
            var userId = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            _logger.LogInformation(userId);
            var result = await _repo.getUserAttendance(userId);
            var mapobject = _mapper.Map<List<GetAttendanceDto>>(result);
            return Ok(mapobject);
        }
        [HttpPost("postLeave")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize(AuthenticationSchemes = "Bearer",Roles =Roles.Employee)]
        public async Task<ActionResult> leaveRequest(UserLeaveDto dto)
        {
            var userId = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var mapObject = _mapper.Map<Leave>(dto);
            mapObject.UserId=userId;
            await _repo.leaveRequest(mapObject);
            return Ok("Success");
        }

        [HttpGet("getLeave")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize(AuthenticationSchemes = "Bearer",Roles =Roles.Employee)]
        public async Task<ActionResult<List<GetUserLeaveDto>>> getLeave()
        {
            var userId = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var result = await _repo.GetUserLeave(userId);
            var mapObject = _mapper.Map<List<GetUserLeaveDto>>(result);
            return Ok(mapObject);
        }

        [HttpGet("getAllLeave")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize(AuthenticationSchemes = "Bearer",Roles =Roles.Employee)]
        public async Task<ActionResult<List<GetAllLeaveRequestDto>>> getAllLeave()
        {
            var result = await _repo.GetAllLeave();
            return Ok(result);
        }



    }
}