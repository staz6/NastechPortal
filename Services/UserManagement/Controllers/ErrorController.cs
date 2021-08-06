using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Dto;
using UserManagement.Entities;
using UserManagement.Helper;
using UserManagement.Interface;

namespace UserManagement.Controllers
{
    [ApiController]
    [Route("errors")]
    public class ErrorController : ControllerBase
    {
        private readonly ISeed _seedRepo;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public ErrorController(ISeed seedRepo, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
        RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager=roleManager;
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

    [HttpPost("RegisterAdmin")]
    public async Task<ActionResult> RegisterAdmin()
    {
                var adminUser = new AppUser{
                    UserName="admin@email.com",
                    Email="admin@email.com"
                };
                var resultAdmin = await _userManager.CreateAsync(adminUser, "Pa$$w0rd");
                await _userManager.AddToRoleAsync(adminUser, Roles.Admin);

        return Accepted();
    }
}
}