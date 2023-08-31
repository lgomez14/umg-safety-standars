using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using umg_database.Common.Interfaces;
using umg_database.Common.Resource;
using umg_database.Sources.Supplier.Entities;
using umg_database.Sources.Supplier.Models;
using umg_safety_standars.Context;

namespace umg_safety_standars.Sources.Supplier.Repositories
{
    [ExcludeFromCodeCoverage]
    public class SupplierRepository : IGetRepository<SupplierIdFilterModel, SupplierEntity>,
        IPostRepository<SupplierRegistryModel, SupplierEntity>, IGetRepository<SupplierLoginFilterModel, SupplierEntity>
    {
        private readonly SafetyStandarContext _context;
        private readonly IDataProtector _protector;
        private readonly string _dataProtectorKey;
        private readonly string _genericKey;

        public SupplierRepository(SafetyStandarContext context, IDataProtectionProvider protector, IConfiguration configuration)
        {
            _dataProtectorKey = configuration["SecretKeys:DataProtectorKey"]!;
            _genericKey = configuration["SecretKeys:GenericKey"]!;
            _protector = protector.CreateProtector(_dataProtectorKey);
            _context = context;
        } 
        public async Task<SupplierEntity> GetAsync(SupplierIdFilterModel filter, CancellationToken cancellationToken = default)
        {
            SupplierEntity? supplierEntity = await _context.Suppliers
                                    .Where(Supplier => Supplier.SupplierId == filter.SupplierId)
                                    .OrderByDescending(Supplier => Supplier.CreatedAt)
                                    .FirstOrDefaultAsync(cancellationToken) ?? throw new Exception(DatabaseException.SupplierNotFound);
            
            return supplierEntity!;
        }

        public Task<SupplierEntity> GetAsync(SupplierLoginFilterModel data, CancellationToken cancellationToken = default)
        {
            SupplierEntity? supplierEntity = _context.Suppliers.FirstOrDefault(entity =>
                entity.Nit == data.Nit);

            if (supplierEntity is null)
                throw new Exception(DatabaseException.SupplierNotFound);

            if (_protector.Unprotect(supplierEntity.Psw) != data.Password)
                throw new Exception(DatabaseException.SupplierNotFound);

            return Task.FromResult(supplierEntity);
        }

        public async Task<SupplierEntity> PostAsync(SupplierRegistryModel data, CancellationToken cancellationToken = default)
        {
            if (data == null) throw new Exception(DatabaseException.SupplierInfoIncorrect);

            SupplierEntity supplier = new()
            {
                SupplierId = Guid.NewGuid(),
                SupplierSocialReason = data.SupplierSocialReason,
                SupplierBusinessName = data.SupplierBusinessName,
                SupplierfirstName = data.SupplierfirstName,
                SupplierMiddleName = data.SupplierMiddleName,
                SupplierThirdName = data.SupplierThirdName,
                SupplierLastName = data.SupplierLastName,
                SupplierMarriedName = data.SupplierMarriedName,
                Dpi = _protector.Protect(data.Dpi),
                Nit = data.Nit,
                Address = _protector.Protect(data.Address),
                Email = _protector.Protect(data.Email),
                Phone = _protector.Protect(data.Phone),
                IconUrl = data.IconUrl,
                CreatedAt = DateTime.UtcNow,
                Name = $"{data.SupplierfirstName} {data.SupplierMiddleName} {data.SupplierThirdName} {data.SupplierLastName} {data.SupplierMarriedName}",
                IsActive = true,
                Psw = _protector.Protect(_genericKey),
            };

            await _context.Suppliers.AddAsync(supplier);
            await _context.SaveChangesAsync();
            return supplier;
        }
        
    }
}