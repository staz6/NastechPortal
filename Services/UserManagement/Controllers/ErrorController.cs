using System;
using System.Threading.Tasks;
using EventBus.Messages.Events;
using MassTransit;
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
        private readonly IAccountRepository _repo;
                private readonly IPublishEndpoint _publishEndpoint;
        public ErrorController(ISeed seedRepo, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,

        RoleManager<IdentityRole> roleManager, IAccountRepository repo,IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
            _repo = repo;
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
        var role = _roleManager.FindByIdAsync(Roles.Admin).Result;
        if (role == null)
        {
            await _roleManager.CreateAsync(new IdentityRole(Roles.Admin));
        }
        var adminUser = new AppUser
        {
            UserName = "admin@email.com",
            Email = "admin@email.com"
        };
        var resultAdmin = await _userManager.CreateAsync(adminUser, "Pa$$w0rd");
        await _userManager.AddToRoleAsync(adminUser, Roles.Admin);

        return Accepted();
    }

    [HttpPost("ReigsterEmployee")]
    public async Task<ActionResult> RegisterEmployee()
    {

        RegisterDtoSeed model = new RegisterDtoSeed
        {
            Id="fa5f4ad6-a005-4d10-8b9f-904cedef73c7",
            Name = "Muhammad Aahad",
            FatherName = "",
            Address = "",
            Email = "aahad123@gmail.com",
            PersonalEmail = "aahad123@email.com",
            ContactNumber = "0335 122050231",
            Password = "Pa$$w0rd",
            ConfirmPassword = "Pa$$w0rd",
            BioMetricId = 1234,
            CurrentSalary = 20000,
            Designation = "Backend Developer",
            Status = "Permanent",
            JoiningDate = DateTime.Now,
            EmergencyNumber = "0335 122034132",
            ProfileImage = "imagepath",
            CNIC = "02-323214123-99",
            Role = "Employee",
            ShiftTiming = "10:00 AM to 09:00 PM"

        };
        await _repo.RegisterTestUser(model);
        RegisterDtoSeed model1 = new RegisterDtoSeed
        {
            Id="fa5f4ad6-a005-4d10-8b9f-904cedef73c8",
            Name = "Sharjeel Mirze",
            FatherName = "",
            Address = "",
            Email = "sharjeel123@gmail.com",
            PersonalEmail = "sharjeel123@email.com",
            ContactNumber = "0335 122050231",
            Password = "Pa$$w0rd",
            ConfirmPassword = "Pa$$w0rd",
            BioMetricId = 3,
            CurrentSalary = 20000,
            Designation = "Sales",
            Status = "Permanent",
            JoiningDate = DateTime.Now,
            EmergencyNumber = "0335 122034132",
            ProfileImage = "imagepath",
            CNIC = "02-323214123-99",
            Role = "Employee",
            ShiftTiming = "10:00 AM to 09:00 PM"

        };
        await _repo.RegisterTestUser(model1);

        RegisterDtoSeed model2 = new RegisterDtoSeed
        {
            Id="fa5f4ad6-a005-4d10-8b9f-904cedef73c6",
            Name = "Abdullah Mirze",
            FatherName = "",
            Address = "",
            Email = "Abdullah@gmail.com",
            PersonalEmail = "Abdullah@email.com",
            ContactNumber = "0335 122050231",
            Password = "Pa$$w0rd",
            ConfirmPassword = "Pa$$w0rd",
            BioMetricId = 4,
            CurrentSalary = 20000,
            Designation = "Sales",
            Status = "Permanent",
            JoiningDate = DateTime.Now,
            EmergencyNumber = "0335 122034132",
            ProfileImage = "imagepath",
            CNIC = "02-323214123-99",
            Role = "Employee",
            ShiftTiming = "10:00 AM to 09:00 PM"

        };
        await _repo.RegisterTestUser(model2);
        RegisterDtoSeed model3 = new RegisterDtoSeed
        {
            Id="fa5f4ad6-a005-4d10-8b9f-904cedef73c0",
            Name = "Shakir Ali",
            FatherName = "",
            Address = "",
            Email = "Shakir@gmail.com",
            PersonalEmail = "Shakir@email.com",
            ContactNumber = "0335 122050231",
            Password = "Pa$$w0rd",
            ConfirmPassword = "Pa$$w0rd",
            BioMetricId = 5,
            CurrentSalary = 20000,
            Designation = "Infrastructure Engineer",
            Status = "Permanent",
            JoiningDate = DateTime.Now,
            EmergencyNumber = "0335 122034132",
            ProfileImage = "imagepath",
            CNIC = "02-323214123-99",
            Role = "Employee",
            ShiftTiming = "10:00 AM to 09:00 PM"

        };
        await _repo.RegisterTestUser(model3);
        RegisterDtoSeed model4 = new RegisterDtoSeed
        {
            Id="fa5f4ad6-a005-4d10-8b9f-904cedef73u7",
            Name = "Salman Junaid",
            FatherName = "",
            Address = "",
            Email = "Salman@gmail.com",
            PersonalEmail = "Salman@email.com",
            ContactNumber = "0335 122050231",
            Password = "Pa$$w0rd",
            ConfirmPassword = "Pa$$w0rd",
            BioMetricId = 6,
            CurrentSalary = 20000,
            Designation = "Frontend Developer",
            Status = "Permanent",
            JoiningDate = DateTime.Now,
            EmergencyNumber = "0335 122034132",
            ProfileImage = "imagepath",
            CNIC = "02-323214123-99",
            Role = "Employee",
            ShiftTiming = "10:00 AM to 09:00 PM"

        };
        await _repo.RegisterTestUser(model4);
        RegisterDtoSeed model5 = new RegisterDtoSeed
        {
            Id="fa5f4ad6-a005-4d10-8b9f-904cedef7iu8",
            Name = "Zain-ul-Abdin",
            FatherName = "",
            Address = "",
            Email = "Zain@gmail.com",
            PersonalEmail = "Zain@email.com",
            ContactNumber = "0335 122050231",
            Password = "Pa$$w0rd",
            ConfirmPassword = "Pa$$w0rd",
            BioMetricId = 7,
            CurrentSalary = 20000,
            Designation = "Animator",
            Status = "Permanent",
            JoiningDate = DateTime.Now,
            EmergencyNumber = "0335 122034132",
            ProfileImage = "imagepath",
            CNIC = "02-323214123-99",
            Role = "Employee",
            ShiftTiming = "10:00 AM to 09:00 PM"

        };
        await _repo.RegisterTestUser(model5);
        RegisterDtoSeed model6 = new RegisterDtoSeed
        {
            Id="fa5f4ad6-a005-4d10-8b9f-904cedef7543",
            Name = "Yumeena Ahmed",
            FatherName = "",
            Address = "",
            Email = "Yumeena@gmail.com",
            PersonalEmail = "Yumeena@email.com",
            ContactNumber = "0335 122050231",
            Password = "Pa$$w0rd",
            ConfirmPassword = "Pa$$w0rd",
            BioMetricId = 8,
            CurrentSalary = 20000,
            Designation = "Sales",
            Status = "Permanent",
            JoiningDate = DateTime.Now,
            EmergencyNumber = "0335 122034132",
            ProfileImage = "imagepath",
            CNIC = "02-323214123-99",
            Role = "Employee",
            ShiftTiming = "10:00 AM to 09:00 PM"

        };
        await _repo.RegisterTestUser(model6);
        RegisterDtoSeed model7 = new RegisterDtoSeed
        {
            Id="fa5f4ad6-a005-4d10-8b9f-904cedef21",
            Name = "Maryum Sohail",
            FatherName = "",
            Address = "",
            Email = "Maryum@gmail.com",
            PersonalEmail = "Maryum@email.com",
            ContactNumber = "0335 122050231",
            Password = "Pa$$w0rd",
            ConfirmPassword = "Pa$$w0rd",
            BioMetricId = 9,
            CurrentSalary = 20000,
            Designation = "Sales",
            Status = "Permanent",
            JoiningDate = DateTime.Now,
            EmergencyNumber = "0335 122034132",
            ProfileImage = "imagepath",
            CNIC = "02-323214123-99",
            Role = "Employee",
            ShiftTiming = "10:00 AM to 09:00 PM"

        };
        await _repo.RegisterTestUser(model6);
        return Ok();
    }


}
}