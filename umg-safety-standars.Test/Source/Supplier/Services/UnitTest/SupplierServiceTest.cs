using Microsoft.Extensions.DependencyInjection;
using Moq;
using umg_database.Common.Interfaces;
using umg_database.Sources.Supplier.Entities;
using umg_database.Sources.Supplier.Models;
using umg_safety_standars.Source.Supplier.Services;
using umg_database.Sources.Jwt.Models;
using umg_database.Sources.Session.Entities;
using umg_safety_standars.Common.Interfaces;
using umg_safety_standars.Common.Resources.Models;
using umg_safety_standars.Common.Resources.Types;
using umg_safety_standars.Source.Jwt.Models;
using umg_safety_standars.Source.Session.Models;
using umg_safety_standars.Source.Session.Services;
namespace umg_safety_standars.Test.Source.Supplier.Services.UnitTest
{
    public class SupplierServiceTest
    {
        private readonly SupplierRegistryService _serviceTest;
        private readonly Mock<IPostRepository<SupplierRegistryModel, SupplierEntity>> _supplierPostRepository;

        public SupplierServiceTest()
        {
            IServiceCollection serviceProvider = new ServiceCollection();
            _supplierPostRepository = new Mock<IPostRepository<SupplierRegistryModel, SupplierEntity>>(); 

            serviceProvider.AddScoped(_ => _supplierPostRepository.Object);

            _serviceTest = new(serviceProvider.BuildServiceProvider());
        }

        [Fact(DisplayName = "Supplier Entity Null should throw Exception.")]
        public async Task SupplierRegistryService_ShouldThrowException()
        {
            SupplierEntity _dummySupplierEntity = null!;
            _supplierPostRepository
                .Setup(x => x.PostAsync(It.IsAny<SupplierRegistryModel>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_dummySupplierEntity);

            SupplierRegistryModel _dummySupplierRegistryModel = new SupplierRegistryModel();

            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await _serviceTest.PostAsync(_dummySupplierRegistryModel);
            });
        }

        [Fact(DisplayName = "Supplier Entity is null should throw BadRequest Exception.")]
        public async Task SupplierRegistryService_ShouldThrowBadRequestException()
        {
            SupplierEntity _dummySupplierEntity = null!;
            _supplierPostRepository
                .Setup(x => x.PostAsync(It.IsAny<SupplierRegistryModel>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_dummySupplierEntity);

            SupplierRegistryModel _dummySupplierRegistryModel = new SupplierRegistryModel();

            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await _serviceTest.PostAsync(_dummySupplierRegistryModel);
            });
        }

        [Fact(DisplayName = "Supplier Entity is valid should return success.")]
        public async Task SupplierRegistryService_ShouldReturnSuccess()
        {
            SupplierEntity _dummySupplierEntity = new SupplierEntity();
            _supplierPostRepository
                .Setup(x => x.PostAsync(It.IsAny<SupplierRegistryModel>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_dummySupplierEntity);

            SupplierRegistryModel _dummySupplierRegistryModel = new SupplierRegistryModel();

            var result = await _serviceTest.PostAsync(_dummySupplierRegistryModel);

            Assert.NotNull(result);
            Assert.Equal((int)HttpEnums.Ok, result.StatusCode);
            Assert.Equal(_dummySupplierEntity, result.Data);
        }
    }
}