using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserManagment.Dto;
using UserManagment.Helper;
using UserManagment.Interface;

namespace UserManagment.Controllers
{
    [ApiController]
    [Route("errors")]
    public class ErrorController : ControllerBase
    {
        private readonly ISeed _seedRepo;
        public ErrorController(ISeed seedRepo)
        {
            _seedRepo = seedRepo;
        }

        [HttpPost("errorCheck")]
        public ActionResult<LoginDto> ErrorCode(int erroCode)
        {
            return new ObjectResult(new ApiErrorResponse(erroCode));
        }

        [HttpPost("SeedData")]
        public async Task<ActionResult> SeedDate()
        {
            await _seedRepo.seedAsync();
            return Ok();
        }
    }
}