using System;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DeliverySoft.Core.Models
{
    public class ApiError
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public HttpStatusCode StatusCode { get; }
        public Guid ErrorId { get; }
        public string Message { get; }

        public ApiError(HttpStatusCode statusCode, Guid errorId, string message)
        {
            this.StatusCode = statusCode;
            this.ErrorId = errorId;
            this.Message = message;
        }
    }
}