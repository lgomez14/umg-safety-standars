using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using umg_database.Sources.Session.Entities;
using umg_database.Sources.Supplier.Entities;

namespace umg_safety_standars.Context
{
    [ExcludeFromCodeCoverage]
    public class SafetyStandarContext : SafetyStandarBaseContext
    {
        public SafetyStandarContext(DbContextOptions<SafetyStandarContext> options) : base(options){}

        #region Supplier
        public virtual DbSet<SupplierEntity> Suppliers { get; set;} = null!;

        #endregion

        #region Session
        public virtual DbSet<SessionEntity> Session { get; set; } = null!;

        #endregion
    }
}