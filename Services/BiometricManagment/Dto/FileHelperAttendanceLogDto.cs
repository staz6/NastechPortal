using System;
using FileHelpers;

namespace BiometricManagment.Dto
{
    [DelimitedRecord(",")]
    public class FileHelperAttendanceLogDto
    {
        public int BiometricId { get; set; }

        public string TimeStamp { get; set; }
    }
}