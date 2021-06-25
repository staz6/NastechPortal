using System;

namespace UserManagment.Helper
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
                200 => "Invalid Login Attempt",
                400 => "Bad Request",
                ErrorStatusCode.NotAuthorize => "Not Authorize",
                404 => "Resource Not Found",
                ErrorStatusCode.InvalidRequest => "Invalid Request",
                _ => null
            };
        }
    }
}