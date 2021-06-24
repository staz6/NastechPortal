using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserManagment.Dto;
using UserManagment.Dto.ServicesDto;
using UserManagment.Entities;
using UserManagment.Helper;
using UserManagment.Interface;

namespace UserManagment.Data
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;

        public AccountRepository(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
             RoleManager<IdentityRole> roleManager, ITokenService tokenService, AppDbContext context)
        {
            _context = context;
            _roleManager = roleManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<UserCheckInEventDto> CheckIn(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var shiftTiming = await _context.Employees.FirstOrDefaultAsync(x => x.Id == user.EmployeeId);
            return new UserCheckInEventDto
            {
                UserId = user.Id,
                ShiftTiming = shiftTiming.ShiftTiming
            };
        }
        public async Task<string> CheckOut(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user.Id;
        }

        public async Task<UsersInfoDto> getCurrentUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            return new UsersInfoDto
            {
                Name = user.Name,
                Email = user.Email
            };
        }

        public async Task<UserDto> Login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            var roleName = await _userManager.GetRolesAsync(user);

            var loginUser = new UserDto
            {
                Name = user.Email,
                Token = _tokenService.CreateToken(user, roleName[0])
            };

            return loginUser;
        }

        public async Task RegisterUser(RegisterDto model)
        {
            var user = new AppUser
            {
                Name = model.Name,
                UserName = model.Email,
                Email = model.Email,
                Address = model.Address,
                ContactNumber = model.ContactNumber,
                Employee = new Employee
                {
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

            await _userManager.AddToRoleAsync(user, Roles.Employee);


        }
    }
}