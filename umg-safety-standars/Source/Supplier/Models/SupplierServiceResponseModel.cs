using System.Diagnostics.CodeAnalysis;

namespace umg_safety_standars.Source.Supplier.Models
{
    [ExcludeFromCodeCoverage]
    public class SupplierServiceResponseModel
    {
        public string Message { get; set; } = null!;
        public ResponseData ResponseData { get; set; } = null!;
    }
    [ExcludeFromCodeCoverage]
    public class ResponseData
    {
        public Guid SupplierId { get; set; }
        public string Nit { get; set; } = null!;
    }
}
