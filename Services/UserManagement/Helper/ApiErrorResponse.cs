using System;

namespace UserManagement.Helper
{
    public class ApiErrorResponse
    {
        public ApiErrorResponse(int statusCode, string message=null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        

        public int StatusCode { get; set; }
        public string Message { get; set; }

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch{
                ErrorStatusCode.ValidRegister => "Register Successfull",
                ErrorStatusCode.InvalidRegister => "Invalid Register Attempt",
                ErrorStatusCode.DuplicateEmail => "Email already exist",
                200 => "Invalid Login Attempt",
                400 => "Bad Request",
                ErrorStatusCode.NotAuthorize => "Not Authorize",
                404 => "Resource Not Found",
                ErrorStatusCode.BiometricExist => "Biometric is register to another employee",
                ErrorStatusCode.InvalidRequest => "Invalid Request",
                _ => null
            };
        }
    }
}