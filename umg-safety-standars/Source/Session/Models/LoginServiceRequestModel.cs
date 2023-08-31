using System.Diagnostics.CodeAnalysis;

namespace umg_safety_standars.Source.Session.Models
{
    [ExcludeFromCodeCoverage]
    public class LoginServiceRequestModel
    {
        public string Nit { get; set; } = null!;
        public string Psw { get; set; } = null!;

    }
}
