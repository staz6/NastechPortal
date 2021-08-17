using System;
using System.Threading.Tasks;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using UserManagement.Dto;
using UserManagement.Entities;
using UserManagement.Helper;
using UserManagement.Interface;

namespace UserManagement.Data
{
    public class Seed : ISeed
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IPublishEndpoint _publishEndpoint;
        public Seed(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
        RoleManager<IdentityRole> roleManager, IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task seedAsync()
        {
            string[] arr = new string[5];

            var chkRole = await _roleManager.RoleExistsAsync(Roles.Employee);
            if (!chkRole)
            {
                int n = 1;
                int bioId = 25;
                await _roleManager.CreateAsync(new IdentityRole(Roles.Employee));
                await _roleManager.CreateAsync(new IdentityRole(Roles.Admin));
                for (int i = 0; i < 5; i++)
                {
                    var user = new AppUser
                    {
                        Name = "string",
                        UserName = "user" + n + "@email.com",
                        Email = "user" + n + "@email.com",
                        Address = "string",
                        PersonalEmail = "string",
                        ContactNumber = "string",
                        Employee = new Employee
                        {
                            BioMetricId = bioId,
                            FatherName = "string",
                            CNIC = "string",
                            CurrentSalary = 25000,
                            Designation = "string",
                            EmergencyNumber = "string",
                            JoiningDate = System.DateTime.Now,
                            ProfileImage = "string",
                            Role = "Employee",
                            Status = "string",
                            ShiftStart = new DateTime(01,01,0001,10,00,00),
                            ShiftEnd= new DateTime(01,01,0001,19,00,00)
                        }
                    };
                    //var role = _roleManager.FindByIdAsync(Roles.Employee).Result;
                    var result = await _userManager.CreateAsync(user, "Pa$$w0rd");
                    await _userManager.AddToRoleAsync(user, Roles.Employee);

                    var generateSalaryEvent = new GenerateSalaryEvent
                    {
                        Salary = 25000,
                        UserId = user.Id
                    };
                    await _publishEndpoint.Publish(generateSalaryEvent);
                    n++;
                    bioId++;
                }

                var adminUser = new AppUser{
                    UserName="admin@email.com",
                    Email="admin@email.com"
                };
                var resultAdmin = await _userManager.CreateAsync(adminUser, "Pa$$w0rd");
                await _userManager.AddToRoleAsync(adminUser, Roles.Admin);
            }
        }
    }
}