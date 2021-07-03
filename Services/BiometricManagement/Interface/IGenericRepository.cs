using System.Collections.Generic;
using System.Threading.Tasks;
using BiometricManagement.Dto;

namespace BiometricManagement.Interface
{
    public interface IGenericRepository
    {
        Task GenerateAttendanceRecordAsync();

        Task<int> GetRecordFromCSV();

        Task UploadDate(IEnumerable<FileHelperAttendanceLogDto> model);
    }
}