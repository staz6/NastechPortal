using Microsoft.AspNetCore.Mvc;
using UserManagment.Dto;
using UserManagment.Helper;

namespace UserManagment.Controllers
{
    [ApiController]
    [Route("errors")]
    public class ErrorController : ControllerBase
    {
        [HttpPost("errorCheck")]
        public ActionResult<LoginDto> ErrorCode(int erroCode)
        {
            return new ObjectResult(new ApiErrorResponse(erroCode));
        }
    }
}