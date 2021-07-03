using System;
using FileHelpers;

namespace BiometricManagement.Dto
{
    [DelimitedRecord(",")]
    public class FileHelperAttendanceLogDto
    {
        public int BiometricId { get; set; }

        public string TimeStamp { get; set; }
    }
}