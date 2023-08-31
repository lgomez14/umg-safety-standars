using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using umg_database.Common.Base;
using umg_database.Sources.Session.Entities;

namespace umg_database.Sources.Supplier.Entities
{
    [Table("umg_supplier")]
    [ExcludeFromCodeCoverage]
    public class SupplierEntity : BaseEntity
    {
        [Required]
        [Column("supplier_id")]
        public Guid SupplierId { get; set; }
        [Required]
        [Column("social_reason")]
        public string SupplierSocialReason { get; set; } = null!;
        [Required]
        [Column("business_name")]
        public string SupplierBusinessName { get; set; } = null!;
        [Required]
        [Column("first_name")]
        public string SupplierfirstName { get; set; } = null!;
        [Required]
        [Column("middle_name")]
        public string? SupplierMiddleName { get; set; }
        [Required]
        [Column("third_name")]
        public string SupplierThirdName { get; set; } = null!;
        [Required]
        [Column("last_name")]
        public string? SupplierLastName { get; set; } 
        [Required]
        [Column("married_name")]
        public string? SupplierMarriedName { get; set; }
        [Required]
        [Column("dpi")]
        public string Dpi { get; set; } = null!;
        [Required]
        [Column("nit")]
        public string Nit { get; set; } = null!;
        [Required]
        [Column("email")]
        public string Email { get; set; } = null!;
        [Required]
        [Column("address")]
        public string Address { get; set; } = null!;
        [Required]
        [Column("phone")]
        public string Phone { get; set; } = null!;
        [Required]
        [Column("icon_url")]
        public string IconUrl { get; set; } = null!;
        [Column("is_active")]
        public bool IsActive { get; set; } = false;
        [Required]
        [Column("supplier_psw")]
        public string Psw { get; set; } = null!;
        public ICollection<SessionEntity> Sessions { get; } = new List<SessionEntity>();
    }
}