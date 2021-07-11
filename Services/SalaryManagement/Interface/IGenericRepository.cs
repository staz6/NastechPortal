using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventBus.Messages.Events;
using EventBus.Messages.Models;
using SalaryManagement.Dto;
using SalaryManagement.Dto.ServicesDto;
using SalaryManagement.Entities;

namespace SalaryManagement.Interface
{
    public interface IGenericRepository
    {
        Task generateSalary(GenerateSalaryEvent model);
        Task getMonthlySalary(string userId);
        Task DeductSalary(List<DeductSalaryConsumerDto> model);
        Task deductSalary(List<UserGetAttendanceEventDto> model,int deductAmount);
        Task monthSalary(string userId,DateTime month,int salarayAmount);
        Task<IReadOnlyList<SalaryByMonth>> getSalaryHistory(string userId);
        Task generateMonthlySalary();
        Task<IReadOnlyList<SalaryDeduction>> getEmployeeSalaryHistory(string userId);

        Task postSalaryDeduction(SalaryDeduction model);
        Task<GenerateSalarySlipDto> generateSalarySlip(string userId);
    }
}