using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagment.Dto.ServicesDto;
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
        public AttendanceController(IPublishEndpoint publishEndpoint, IMapper mapper, IAccountRepository accountRepo)
        {
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
    }
}