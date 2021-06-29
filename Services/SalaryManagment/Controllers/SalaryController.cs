using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalaryManagment.Interface;

namespace SalaryManagment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalaryController : ControllerBase
    {
        private readonly IGenericRepository _repo;
        public SalaryController(IGenericRepository repo)
        {
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
    }
}