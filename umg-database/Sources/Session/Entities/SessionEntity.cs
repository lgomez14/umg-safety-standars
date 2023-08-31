using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using umg_database.Common.Base;
using umg_database.Sources.Supplier.Entities;

namespace umg_database.Sources.Session.Entities
{
    [Table("umg_session")]
    [ExcludeFromCodeCoverage]

    public class SessionEntity : BaseEntity
    {
        [Required]
        [Column("session_id")]
        public Guid SessionId { get; set; }
        [Column("jwt_token")]
        public string? JwtToken { get; set; }
        [Column("supplier_id")]
        public int? SupplierId { get; set; }
        public virtual SupplierEntity? Supplier { get; set; }
        [Column("jwt_expiration")]
        public DateTime? JwtExpiration { get; set; }
        [Column("is_active")]
        public bool IsActive { get; set; }

    }
}