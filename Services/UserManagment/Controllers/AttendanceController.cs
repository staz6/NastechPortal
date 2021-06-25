using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using EventBus.Messages.Events;
using EventBus.Messages.Models;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagment.Dto.ServicesDto;
using UserManagment.Helper;
using UserManagment.Interface;

namespace UserManagment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttendanceController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAccountRepository _accountRepo;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IRequestClient<UserGetAttendanceEventRequest> _client;
        public AttendanceController(IPublishEndpoint publishEndpoint, IMapper mapper,
         IAccountRepository accountRepo, IRequestClient<UserGetAttendanceEventRequest> client)
        {
            _client = client;
            _publishEndpoint = publishEndpoint;
            _accountRepo = accountRepo;
            _mapper = mapper;
        }

        [HttpPost("checkIn")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult> CheckIn()
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            if (email == null) return BadRequest();
            var result = await _accountRepo.CheckIn(email);


            var eventMessage = _mapper.Map<UserCheckInEvent>(result);

            await _publishEndpoint.Publish(eventMessage);

            return Accepted();


        }

        [HttpPost("checkOut")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult> CheckOut()
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            if (email == null) return BadRequest();
            var result = await _accountRepo.CheckOut(email);
            var userCheckOutEventDto = new UserCheckOutEventDto
            {
                UserId = result
            };

            var eventMessage = _mapper.Map<UserCheckOutEvent>(userCheckOutEventDto);

            await _publishEndpoint.Publish(eventMessage);

            return Accepted();


        }

        [HttpGet("getAttendance")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize(AuthenticationSchemes = "Bearer",Roles =Roles.Employee)]
        public async Task<ActionResult> getAttendance()
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            if (email == null) return BadRequest();
            string userId = await _accountRepo.getUserId(email);

            using var request = _client.Create(new UserGetAttendanceEventRequest
            {
                UserId = userId
            });
            var response = await request.GetResponse<UserGetAttendanceEventResponse>();

            IEnumerable<AttendanceEventDto> attendance = response.Message.Attendance;

            return Ok(attendance);
        }

        [HttpPost("leaveRequest")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize(AuthenticationSchemes = "Bearer",Roles =Roles.Employee)]
        public async Task<ActionResult> leaveRequest(UserLeaveDto model)
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            if (email == null) return BadRequest();
            string userId = await _accountRepo.getUserId(email);
            var userLeaveEventDto = new UserLeaveEventDto{
                UserId=userId,
                From=model.From,
                Till=model.Till,
                Reason=model.Reason
            };
            var eventMessage = _mapper.Map<UserLeaveEvent>(userLeaveEventDto);
            await _publishEndpoint.Publish(eventMessage);
            return Accepted();



        }

    }
}