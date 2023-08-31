using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using umg_safety_standars.Common.Resources.Types;

namespace umg_safety_standars.Common.Resources.Models
{
    [ExcludeFromCodeCoverage]
    public class ResponseSuccess<TResponse> :ResponseBase
    {
        [JsonPropertyName("data")]
        public TResponse? Data { get; set; } = default(TResponse);


        public int SuccessCode { get; set; }

        [JsonConstructor]
        public ResponseSuccess()
        {
        }

        public ResponseSuccess(HttpEnums statusCode, TResponse data)
        {
            base.StatusCode = (int)statusCode;
            Data = data;
            SuccessCode = 0;
        }

        public ResponseSuccess(HttpEnums statusCode, TResponse data, int successCode)
        {
            base.StatusCode = (int)statusCode;
            Data = data;
            SuccessCode = successCode;
        }

        public IActionResult GetResult()
        {
            return new ObjectResult(this)
            {
                StatusCode = base.StatusCode
            };
        }
    }
}