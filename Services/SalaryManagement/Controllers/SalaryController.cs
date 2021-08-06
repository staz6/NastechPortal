using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalaryManagement.Dto;
using SalaryManagement.Entities;
using SalaryManagement.Helpers;
using SalaryManagement.Interface;

namespace SalaryManagement.Controllers
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

        /// <summary>
        /// This method is used for updating/Generating the employees Salary 
        /// This will automatically generate/update the salary of the month provided
        /// </summary>
        /// <returns></returns>

        [HttpPost("generateMonthlySalary")] 
        [Authorize(AuthenticationSchemes = "Bearer", Roles = Roles.Admin)] 
        [ProducesResponseType((int)HttpStatusCode.Accepted)] 
        public async Task<ActionResult> getMonthlySalary(DateTime date)
        {
            if(date==null) return BadRequest();
            await _repo.generateMonthlySalary(date);
            return Ok();
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "" + Roles.Employee + "," + Roles.Admin + "")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [HttpGet("salary/{userId}")]
        public async Task<ActionResult<IReadOnlyList<GetSalaryHistoryDto>>> getSalaryHistory(string userId)
        {          
            if(userId==null) return BadRequest();
            var result = await _repo.getSalaryHistory(userId);
            return Ok(_mapper.Map<IReadOnlyList<GetSalaryHistoryDto>>(result));
        }

        [HttpGet("salaryDeduction/{id}")]
        
        public async Task<ActionResult<IReadOnlyList<GetEmployeeSalaryDeduction>>> getEmployeeSalaryDeduction(string id)
        {

            var result = await _repo.getEmployeeSalaryHistory(id);
            var mapObject = _mapper.Map<IReadOnlyList<GetEmployeeSalaryDeduction>>(result);

            return Ok(mapObject);
            
        }
        [HttpPost("salaryDeduction")]
        
        public async Task<ActionResult> postSalaryDeduction(PostSalaryDeduction model)
        {
            var mapOject = _mapper.Map<SalaryDeduction>(model);
            await _repo.postSalaryDeduction(mapOject);
            return Ok();
        }
        
        [HttpGet("salarySlip/{id}")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "" + Roles.Employee + "," + Roles.Admin + "")]
        public async Task<ActionResult<GenerateSalarySlipDto>> generateSalarySlip(int id)
        {
            var result = await _repo.generateSalarySlip(id);
            return result;   
        }
    }
}