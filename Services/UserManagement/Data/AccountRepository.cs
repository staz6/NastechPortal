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

using UserManagement.Entities;
using UserManagement.Helper;
using UserManagement.Interface;

namespace UserManagement.Data
{
    /// <summary>
    /// All bussiness logic takes place here 
    /// </summary>
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

        /// <summary>
        /// use for getting user id via email
        /// </summary>
 

        public async Task<bool> getUserId(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if(user==null)
            {
                return true;
            }
            return false;
            
        }


        /// <summary>
        /// Basic login implementaion calling TokenService repository via _tokenservice to generate token
        /// if request successfull
        /// </summary>
        /// <returns>Token</returns>
        public async Task<string> Login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if(result.Succeeded)
            {
                var roleName = await _userManager.GetRolesAsync(user);

                
                string Token = _tokenService.CreateToken(user, roleName[0]);
                return Token;
            }
            else
            {
                return null;
            }
            
        }
        /// <summary>
        /// Use for registering new employee
        /// </summary>
  
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
                        JoiningDate = model.JoiningDate,
                        ProfileImage = model.ProfileImage,
                        Role = model.Role,
                        Status = model.Status,
                        ShiftTiming = model.ShiftTiming
                    }
                };
                
                var role = _roleManager.FindByIdAsync(model.Role).Result;
                var result = await _userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded) return ErrorStatusCode.InvalidRegister;

                await _userManager.AddToRoleAsync(user, model.Role);
                
                /// <summary>
                /// Sending the employee salary to SalaryManagment microservices
                /// </summary>
                /// <value></value>
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
        /// <summary>
        /// IMPORTANT calling this function via AttendanceRecordConsumer which get biometricmanagment service
        /// rabbitmq call and sending the records to Attendancemanagment micro service
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

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

            SendAttendanceRecordEvent result = new SendAttendanceRecordEvent
            {
                SendAttendanceRecord = listobj
            };
            _logger.LogInformation(result.SendAttendanceRecord.ToString());
            await _publishEndpoint.Publish(result);
        }

        /// <summary>
        /// Basic function to get currentuser via id should have use automapper here 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>UsersInfoDto</returns>
        public async Task<UsersInfoDto> getCurrentUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user !=null)
            {
                var employee = await _context.Employees.FirstOrDefaultAsync(x => x.AppUserId == user.Id);

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
                    Status = employee.Status,
                    AppUserId = employee.AppUserId,
                    JoiningDate = employee.JoiningDate
                };
            }
            else{
                throw new Exception();
            }
            
        }

        /// <summary>
        /// Basic function to get List of all employee  
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>UsersInfoDto</returns>
        public async Task<IReadOnlyList<Employee>> GetAllUser()
        {
            var result = await _context.Employees.Include(c => c.AppUser).ToListAsync();
            // var mapObject = _mapper.Map<List<UsersInfoDto>>(result);
            return result;
        }


        /// <summary>
        /// Edit function so employee can edit his info
        /// </summary>

        public async Task EditEditEmployeeInfo(string userId, EditEmployeeInfoDto model)
        {
            var user = await _context.Users.Include(x => x.Employee).FirstOrDefaultAsync(x => x.Id == userId);
            if(user == null)
                throw new Exception();
            else{
                user.PersonalEmail = model.PersonalEmail;
                user.Employee.EmergencyNumber = model.EmergencyNumber;
                user.Address = model.Address;
                user.ContactNumber = model.ContactNumber;

                await _context.SaveChangesAsync();
            }
            

        }

       /// <summary>
       /// Check function to see if biometric id already exist since every bio id needs to be unique
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
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

        /// <summary>
        /// Update salary on accessible to admin IMPORTANT add the message broker functionality 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task updateSalary(int id, UpdateSalaryDto model)
        {
            try{
                var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id==id);
                if(employee ==null) throw new Exception();
                employee.CurrentSalary=model.Salary;
                await _context.SaveChangesAsync();
            }
            catch(Exception)
            {
                throw new Exception();
            }
        }
    }
}