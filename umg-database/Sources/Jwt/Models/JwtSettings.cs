using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace umg_database.Sources.Jwt.Models
{
    [ExcludeFromCodeCoverage]
    public class JwtSettings
    {
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public int ExpirationHours { get; set; }
        public string? JwtKey { get; set; }
    }
}
