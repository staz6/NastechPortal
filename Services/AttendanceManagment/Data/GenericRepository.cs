using System;
using System.Linq;
using System.Threading.Tasks;
using AttendanceManagment.Entities;
using AttendanceManagment.Interface;
using Microsoft.EntityFrameworkCore;

namespace AttendanceManagment.Data
{
    public class GenericRepository : IGenericRepository
    {
        private readonly AppDbContext _context;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CheckIn(Attendance model)
        {
            
            model.Date =DateTime.Now.ToString("dddd, dd MMMM yyyy");
            var modelObject = await _context.Attendances.FirstOrDefaultAsync(x => x.UserId==model.UserId && x.Date==model.Date);
            DateTime startDate = 
                new DateTime(DateTime.Today.Year,DateTime.Today.Month,DateTime.Today.Day,10,00,00);
            if(modelObject==null)
            {
            model.CheckIn=DateTime.Now;
            var calculateGrace = model.CheckIn.Subtract(startDate);
            if(calculateGrace.TotalMinutes > 0)
            {
                model.EffectiveHours = calculateGrace.Hours+":"+calculateGrace.Minutes;
            }
            else
            {
                model.EffectiveHours= "Early";
            }
            //model.EffectiveHours=calculateGrace;
            await _context.AddAsync(model);
            await SaveChanges();
            }
            

        }

        public async Task CheckOut(Attendance model)
        {
            model.Date = DateTime.Now.ToString("dddd, dd MMMM yyyy");
            var modelObject = await _context.Attendances.FirstOrDefaultAsync(x => x.UserId==model.UserId && x.Date==model.Date);
            if(modelObject!=null)
            {
                modelObject.CheckOut=DateTime.Now;
                modelObject.WorkedHours = modelObject.CheckOut.Subtract(modelObject.CheckIn).ToString("hh\\:mm\\:ss");
                _context.Attendances.Update(modelObject);
                _context.Update(modelObject);
            }
            await SaveChanges();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}