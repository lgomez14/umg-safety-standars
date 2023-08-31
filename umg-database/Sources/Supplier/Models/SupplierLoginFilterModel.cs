using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace umg_database.Sources.Supplier.Models
{
    [ExcludeFromCodeCoverage]
    public class SupplierLoginFilterModel
    {
        public string Nit { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
