namespace UserManagement.Helper
{
    public static class ErrorStatusCode
    {
        public const int ValidRegister = 100;
        public const int InvalidRegister = 101;
        public const int DuplicateEmail = 102;
        public const int InvalidRequest = 500;
        public const int NotAuthorize = 401;
        public const int BiometricExist = 600;
    }
}