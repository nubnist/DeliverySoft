using System;
using System.Net;
using DeliverySoft.Core.Models;

namespace DeliverySoft.Core
{
    public class ApiException : Exception
    {
        public Guid ErrorId { get; } = Guid.NewGuid();
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.InternalServerError;

        public ApiException() : base() { }
    
        public ApiException(string message) : base(message) { }
    
        public ApiException(HttpStatusCode statusCode) : base()
        {
            this.StatusCode = statusCode;
        }
    
        public ApiException(HttpStatusCode statusCode, string message) : base(message)
        {
            this.StatusCode = statusCode;
        }
    
        public virtual ApiError GetApiError() => new ApiError(this.StatusCode, this.ErrorId, this.Message);
    }
}
