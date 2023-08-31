using System.Diagnostics.CodeAnalysis;

namespace umg_safety_standars.Source.Session.Models
{
    [ExcludeFromCodeCoverage]
    public class LoginServiceResponseModel
    {
        public string? Jwt { get; set; }
        public bool IsAuthenticated { get; set; } = false;
    }
}
