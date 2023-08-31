using umg_database.Common.Interfaces;
using umg_database.Common.Resource;
using umg_database.Sources.Supplier.Entities;
using umg_database.Sources.Supplier.Models;
using umg_safety_standars.Common.Interfaces;
using umg_safety_standars.Common.Resources.Models;
using umg_safety_standars.Common.Resources.Types;

namespace umg_safety_standars.Source.Supplier.Services
{
    public class SupplierRegistryService : IPostService<SupplierRegistryModel, SupplierEntity>
    {
        private readonly IPostRepository<SupplierRegistryModel, SupplierEntity> _supplierPostRepository;
        public SupplierRegistryService(IServiceProvider services)
        {
            _supplierPostRepository = services.GetRequiredService<IPostRepository<SupplierRegistryModel, SupplierEntity>>();

        }
        public async Task<ResponseSuccess<SupplierEntity>> PostAsync(SupplierRegistryModel data, CancellationToken cancellationToken = default)
        {
            SupplierEntity supplierEntity = await _supplierPostRepository.PostAsync(data, cancellationToken);
            if (supplierEntity is null) throw new Exception(HttpException.BadRequest);
            
            return new()
            {
                Data = supplierEntity,
                StatusCode = (int)HttpEnums.Ok
            };
        }
    }
}