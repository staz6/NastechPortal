using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventBus.Messages.Events;
using EventBus.Messages.Models;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UserManagement.Dto;
using UserManagement.Dto.ServicesDto;
using UserManagement.Entities;
using UserManagement.Helper;
using UserManagement.Interface;

namespace UserManagement.Data
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;
        private readonly ILogger<AccountRepository> _logger;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public AccountRepository(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ILogger<AccountRepository> logger, IMapper mapper,
             RoleManager<IdentityRole> roleManager, ITokenService tokenService, AppDbContext context, IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
            _mapper = mapper;
            _logger = logger;
            _context = context;
            _roleManager = roleManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        // public async Task<UserCheckInEventDto> CheckIn(string email)
        // {
        //     // var user = await _userManager.FindByEmailAsync(email);
        //     // var shiftTiming = await _context.Employees.FirstOrDefaultAsync(x => x.Id == user.EmployeeId);
        //     // return new UserCheckInEventDto
        //     // {
        //     //     UserId = user.Id,
        //     //     ShiftTiming = shiftTiming.ShiftTiming
        //     // };
        // }
        // public async Task<string> CheckOut(string email)
        // {
        //     var user = await _userManager.FindByEmailAsync(email);
        //     return user.Id;
        // }

        public async Task<bool> getUserId(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if(user==null)
            {
                return true;
            }
            return false;
            
        }

        public async Task<UsersInfoDto> getCurrentUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var employee = await _context.Employees.FirstOrDefaultAsync(x => x.AppUserId==user.Id);

            return new UsersInfoDto
            {
                EmployeeId = employee.Id,
                Address = user.Address,
                CNIC = employee.CNIC,
                CurrentSalary = employee.CurrentSalary,
                Designation = employee.Status,
                Email = user.Email,
                PersonalEmail = user.PersonalEmail,
                MobileNumber = user.ContactNumber,
                EmergencyNumber = employee.EmergencyNumber,
                Name = user.Name,
                ShiftTiming = employee.ShiftTiming,
                Status = employee.Status
            };
        }

        public async Task<string> Login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            
            var roleName = await _userManager.GetRolesAsync(user);

            var loginUser = new UserDto
            {
                Name = user.Email,
                Token = _tokenService.CreateToken(user, roleName[0])
            };

            return loginUser.Token;
        }

        public async Task<int> RegisterUser(RegisterDto model)
        {
            
            try
            {
                var user = new AppUser
                {
                    Name = model.Name,
                    UserName = model.Email,
                    Email = model.Email,
                    Address = model.Address,
                    PersonalEmail = model.PersonalEmail,
                    ContactNumber = model.ContactNumber,
                    Employee = new Employee
                    {
                        BioMetricId = model.BioMetricId,
                        FatherName = model.FatherName,
                        CNIC = model.CNIC,
                        CurrentSalary = model.CurrentSalary,
                        Designation = model.Designation,
                        EmergencyNumber = model.Designation,
                        JoiningDate = DateTime.Now,
                        ProfileImage = model.ProfileImage,
                        Role = Roles.Employee,
                        Status = model.Status,
                        ShiftTiming = model.ShiftTiming
                    }
                };
                
                var chkRole = await _roleManager.RoleExistsAsync(Roles.Employee);
                if (!chkRole)
                {
                    await _roleManager.CreateAsync(new IdentityRole(Roles.Employee));
                }
                var role = _roleManager.FindByIdAsync(Roles.Employee).Result;
                var result = await _userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded) return ErrorStatusCode.InvalidRegister;

                await _userManager.AddToRoleAsync(user, Roles.Employee);
                
                var generateSalaryEvent  = new GenerateSalaryEvent{
                       Salary=model.CurrentSalary,
                       UserId=user.Id
                 };
                await _publishEndpoint.Publish(generateSalaryEvent);
                
                return ErrorStatusCode.ValidRegister;
                
            }
            catch (Exception)
            {
                return ErrorStatusCode.InvalidRequest;
            }



        }

        public async Task GetAttendanceRecord(IEnumerable<GetAttendanceRecordEventDto> model)
        {
            List<SendAttendanceRecordEventDto> listobj = new List<SendAttendanceRecordEventDto>();
            
            foreach (var item in model)
            {
                var employee =await  _context.Employees.FirstOrDefaultAsync(x => x.BioMetricId == item.BiometricId);
                
                if(employee != null){
                    var user = await _context.Users.FirstOrDefaultAsync(x => x.Id==employee.AppUserId);
                    if(user!=null)
                    {
                        var a = new SendAttendanceRecordEventDto{
                        UserId=user.Id,
                        ShiftTiming=employee.ShiftTiming,
                        TimeStamp=item.TimeStamp
                        };
                        listobj.Add(a);
                    }
                    
                }
                
            }
            // var mapObject = _mapper.Map<IEnumerable<GetAttendanceRecordEventDto>, IEnumerable<SendAttendanceRecordEventDto>>(model)
            //                         .Where(x => x.EmployeeId != 0);
            // foreach(var item in mapObject)
            // {
            //     var employee =await  _context.Employees.FirstOrDefaultAsync(x => x.Id == item.EmployeeId);
            //     item.ShiftTiming = employee.ShiftTiming;
            // }
            SendAttendanceRecordEvent result = new SendAttendanceRecordEvent
            {
                SendAttendanceRecord = listobj
            };
            _logger.LogInformation(result.SendAttendanceRecord.ToString());
            await _publishEndpoint.Publish(result);
        }

        public async Task<IReadOnlyList<Employee>> GetAllUser()
        {
            var result = await _context.Employees.Include(c => c.AppUser).ToListAsync();
            // var mapObject = _mapper.Map<List<UsersInfoDto>>(result);
            return result;
        }

        public async Task EditEditEmployeeInfo(string userId, EditEmployeeInfoDto model)
        {
            var user = await _context.Users.Include(x => x.Employee).FirstOrDefaultAsync(x => x.Id == userId);
            //var employee = await _context.Employees.FirstOrDefaultAsync(x => x.AppUserId==userId);
            user.PersonalEmail = model.PersonalEmail;
            user.Employee.EmergencyNumber = model.EmergencyNumber;
            user.Address = model.Address;
            user.ContactNumber = model.ContactNumber;

            await _context.SaveChangesAsync();

        }

        public async Task<bool> BiometricCheck(int id)
        {
            var chk = await _context.Employees.FirstOrDefaultAsync(x => x.BioMetricId ==id);
            if(chk==null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}