using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace umg_safety_standars.Common.Resources.Models
{
    [ExcludeFromCodeCoverage]
    public abstract class ResponseBase
    {
        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }
        [JsonConstructor]
        public ResponseBase(){}
        
    }
}