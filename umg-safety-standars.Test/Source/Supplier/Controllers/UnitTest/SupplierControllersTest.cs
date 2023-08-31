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
using umg_safety_standars.Source.Supplier.Controllers;
using umg_safety_standars.Source.Supplier.Models;

namespace umg_safety_standars.Test.Source.Supplier.Controllers.UnitTest
{
    public class SupplierControllerTest
    {
        private readonly SupplierController _controllerTest;
        private readonly Mock<IPostService<SupplierRegistryModel, SupplierEntity>> _servicePost;

        public SupplierControllerTest()
        {
            _servicePost = new Mock<IPostService<SupplierRegistryModel, SupplierEntity>>();
            _controllerTest = new SupplierController(_servicePost.Object);
        }

        [Fact(DisplayName = "Valid request should return success response with message and data.")]
        public async void PostAsync_ValidRequest_ShouldReturnSuccessResponseWithMessageAndData()
        {
            Guid expectedSupplierId = Guid.Parse("727ae2bc-9d1e-48ba-8566-3d088b2708d1");
            SupplierEntity _dummySupplierEntity = new SupplierEntity()
            {
                Nit = "123456789",
                SupplierId = expectedSupplierId,
                SupplierBusinessName = "Test"
            };
            ResponseSuccess<SupplierEntity> _dummyServiceResponse = new ResponseSuccess<SupplierEntity>()
            {
                Data = _dummySupplierEntity,
                StatusCode = (int)HttpEnums.Ok,
                SuccessCode = (int)HttpEnums.Ok
            };
            _servicePost
                .Setup(x => x.PostAsync(It.IsAny<SupplierRegistryModel>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_dummyServiceResponse);

            SupplierRegistryModel _dummyRequest = new SupplierRegistryModel()
            {
                Nit = "123456789",
                SupplierBusinessName = "test"
            };

            ResponseSuccess<SupplierServiceResponseModel> result = await _controllerTest.PostAsync(_dummyRequest);

            Assert.Equal((int)HttpEnums.Ok, result.StatusCode);
            Assert.Equal((int)HttpEnums.Ok, result.SuccessCode);
            if (result.Data != null)
            {
                Assert.Equal("User Test created!", result.Data.Message);
                Assert.Equal("123456789", result.Data.ResponseData.Nit);
                Assert.Equal(expectedSupplierId, result.Data.ResponseData.SupplierId);
            }

        }
    }
}