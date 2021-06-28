using System.Threading.Tasks;
using AutoMapper;
using EventBus.Messages.Events;
using SalaryManagment.Entities;
using SalaryManagment.Interface;

namespace SalaryManagment.Data
{
    public class GenericRepository : IGenericRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public GenericRepository(AppDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task generateSalary(GenerateSalaryEvent model)
        {
           var mapObject= _mapper.Map<Salary>(model);
            _context.Salarys.AddAsync(mapObject).GetAwaiter().GetResult();
            await _context.SaveChangesAsync();
        }
    }
}