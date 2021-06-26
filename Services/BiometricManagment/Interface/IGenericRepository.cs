using System.Collections.Generic;
using System.Threading.Tasks;
using BiometricManagment.Dto;

namespace BiometricManagment.Interface
{
    public interface IGenericRepository
    {
        Task GenerateAttendanceRecordAsync();

        Task<int> GetRecordFromCSV();

        Task UploadDate(IEnumerable<FileHelperAttendanceLogDto> model);
    }
}