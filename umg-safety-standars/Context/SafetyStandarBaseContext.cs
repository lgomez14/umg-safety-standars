using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace umg_safety_standars.Context
{
    [ExcludeFromCodeCoverage]
    public class SafetyStandarBaseContext : DbContext
    {
        public SafetyStandarBaseContext(DbContextOptions options) : base(options){}        
    }
}