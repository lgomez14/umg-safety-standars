using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace umg_database.Sources.Supplier.Models;
[ExcludeFromCodeCoverage]
public class SupplierRegistryModel
{
    [Required]
    public string SupplierSocialReason { get; set; } = null!;
    [Required]
    public string SupplierBusinessName { get; set; } = null!;
    [Required]
    public string SupplierfirstName { get; set; } = null!;
    [Required]
    public string SupplierMiddleName { get; set; } = null!;
    [Required]
    public string SupplierThirdName { get; set; } = null!;
    [Required]
    public string SupplierLastName { get; set; } = null!;
    [Required]
    public string? SupplierMarriedName { get; set; }
    [Required]
    public string Dpi { get; set; } = null!;
    [Required]
    public string Nit { get; set; } = null!;
    [Required]
    public string Email { get; set; } = null!;
    [Required]
    public string Address { get; set; } = null!;
    [Required]
    public string Phone { get; set; } = null!;
    [Required]
    public string IconUrl { get; set; } = null!;
}
