namespace EventBus.Messages.Common
{
    public static class EventBusConstants
    {
        public const string UserCheckInQueue = "usercheckin-queue";
        public const string UserCheckOutQueue = "usercheckout-queue";
        public const string UserGetAttendanceEvent = "usergetattendance-queue";
        public const string UserLeaveQueue = "userleave-queue";
        public const string GetAttendaceRecordQueue = "getattendancerecord-queue";
        public const string SetAttendanceRecordQueue = "setattendancerecord-queue";
        public const string generateSalaryQueue = "generatesalary-queue";
    }
}