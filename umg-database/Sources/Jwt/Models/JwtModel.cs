using System.Diagnostics.CodeAnalysis;

namespace umg_database.Sources.Jwt.Models
{
    [ExcludeFromCodeCoverage]
    public class JwtModel
    {
        public string BearerToken { get; set; } = null!;
        public DateTime? jwtExpiration { get; set; }
    }
}