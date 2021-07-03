using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalaryManagment.Dto;
using SalaryManagment.Entities;
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

        [HttpPost("generateMonthlySalary")]   
        public async Task<ActionResult> getMonthlySalary()
        {
            
            await _repo.generateMonthlySalary();
            return Ok();
        }

        [HttpGet("getSalaryHistory")]
        [Authorize]
        public async Task<ActionResult<IReadOnlyList<GetSalaryHistoryDto>>> getSalaryHistory()
        {
            var userId = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var result = await _repo.getSalaryHistory(userId);
            return Ok(_mapper.Map<IReadOnlyList<GetSalaryHistoryDto>>(result));
        }

        [HttpGet("userSalaryDeduction/{id}")]
        [Authorize]
        public async Task<ActionResult<IReadOnlyList<GetEmployeeSalaryDeduction>>> getEmployeeSalaryDeduction(string id)
        {

            var result = await _repo.getEmployeeSalaryHistory(id);
            var mapObject = _mapper.Map<IReadOnlyList<GetEmployeeSalaryDeduction>>(result);

            return Ok(mapObject);
            
        }
        [HttpPost("postSalaryDeduction")]
        [Authorize]
        public async Task<ActionResult> postSalaryDeduction(PostSalaryDeduction model)
        {
            var mapOject = _mapper.Map<SalaryDeduction>(model);
            await _repo.postSalaryDeduction(mapOject);
            return Ok();
        }
    }
}