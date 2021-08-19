namespace ProjectManagement.Helper
{
    public static class ErrorStatusCode
    {
        public const int CreateSuccess=1;
        public const int UpdateSuccess=2;
        public const int DeleteSuccess=3;
        public const int ValidRegister = 100;
        public const int InvalidRegister = 101;
        public const int DuplicateEmail = 102;
        public const int InvalidLogin = 103;
        public const int InvalidRequest = 500;
        public const int NotAuthorize = 401;
        public const int BiometricExist = 600;
    }
}