using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalaryManagment.Dto;
using SalaryManagment.Interface;

namespace SalaryManagment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalaryController : ControllerBase
    {
        private readonly IGenericRepository _repo;
        private readonly IMapper _mapper;
        public SalaryController(IGenericRepository repo, IMapper mapper)
        {
            _mapper = mapper;

            _repo = repo;
        }

        [HttpPost("getMonthlySalary")]
        [Authorize]
        public async Task<ActionResult> getMonthlySalary()
        {
            var userId = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            await _repo.getMonthlySalary(userId);
            return Ok(userId);
        }

        [HttpGet("getSalaryHistory")]
        [Authorize]
        public async Task<ActionResult<IReadOnlyList<GetSalaryHistoryDto>>> getSalaryHistory()
        {
            var userId = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var result = await _repo.getSalaryHistory(userId);
            return Ok(_mapper.Map<IReadOnlyList<GetSalaryHistoryDto>>(result));
        }
    }
}