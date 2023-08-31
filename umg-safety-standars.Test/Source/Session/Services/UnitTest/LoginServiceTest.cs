using Microsoft.Extensions.DependencyInjection;
using Moq;
using umg_database.Common.Interfaces;
using umg_database.Sources.Jwt.Models;
using umg_database.Sources.Session.Entities;
using umg_database.Sources.Supplier.Entities;
using umg_database.Sources.Supplier.Models;
using umg_safety_standars.Common.Interfaces;
using umg_safety_standars.Common.Resources.Models;
using umg_safety_standars.Common.Resources.Types;
using umg_safety_standars.Source.Jwt.Models;
using umg_safety_standars.Source.Session.Models;
using umg_safety_standars.Source.Session.Services;

namespace umg_safety_standars.Test.Source.Session.Services.UnitTest;

public class AuthControllerTest
{
    private readonly LoginService _serviceTest;
    private readonly Mock<IJwtService<JwtAuthServiceRequestModel, JwtModel>> _jwtGenerator;
    private readonly Mock<IGetRepository<SupplierLoginFilterModel, SupplierEntity>> _getSupplierByNitRepository;
    private readonly Mock<IPutRepository<SessionEntity>> _sessionPutRepository;

    public AuthControllerTest()
    {
        IServiceCollection serviceProvider = new ServiceCollection();
        _jwtGenerator = new Mock<IJwtService<JwtAuthServiceRequestModel, JwtModel>>();
        _getSupplierByNitRepository = new Mock<IGetRepository<SupplierLoginFilterModel, SupplierEntity>>();
        _sessionPutRepository = new();

        serviceProvider.AddScoped(_ => _jwtGenerator.Object);
        serviceProvider.AddScoped(_ => _getSupplierByNitRepository.Object);
        serviceProvider.AddScoped(_ => _sessionPutRepository.Object);

        _serviceTest = new(serviceProvider.BuildServiceProvider());
    }

    [Fact(DisplayName = "Supplier Entity Null should be return Not Found Response.")]
    public async void LoginService_ShouldReturnNotFound()
    {
        SupplierEntity _dummySupplierEntity = null!;
        _getSupplierByNitRepository
            .Setup(x => x.GetAsync(It.IsAny<SupplierLoginFilterModel>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(_dummySupplierEntity);

        LoginServiceRequestModel _dummyLoginServiceRequestModel = new LoginServiceRequestModel()
        {
            Nit = "5465465",
            Psw = "test"

        };

        CancellationToken token = default;

        ResponseSuccess<LoginServiceResponseModel> result = await _serviceTest.PostAsync(_dummyLoginServiceRequestModel,token);

        Assert.Equal((int)HttpEnums.NotFound, result.StatusCode);
    }
    [Fact(DisplayName = "Jwt Response is null should throw BadRequest Exception.")]
    public async Task LoginService_JwtResponseIsNull_ShouldThrowBadRequestException()
    {
        SupplierEntity _dummySupplierEntity = new SupplierEntity();
        _getSupplierByNitRepository
            .Setup(x => x.GetAsync(It.IsAny<SupplierLoginFilterModel>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(_dummySupplierEntity);

        LoginServiceRequestModel _dummyLoginServiceRequestModel = new LoginServiceRequestModel()
        {
            Nit = "5465465",
            Psw = "test"
        };

        CancellationToken token = default;

        await Assert.ThrowsAsync<Exception>(async () =>
        {
            await _serviceTest.PostAsync(_dummyLoginServiceRequestModel, token);
        });
    }
    [Fact(DisplayName = "Jwt Response has bad status code should throw BadRequest Exception.")]
    public async Task LoginService_JwtResponseHasBadStatusCode_ShouldThrowBadRequestException()
    {
        SupplierEntity _dummySupplierEntity = new SupplierEntity();
        _getSupplierByNitRepository
            .Setup(x => x.GetAsync(It.IsAny<SupplierLoginFilterModel>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(_dummySupplierEntity);

        ResponseSuccess<JwtModel> _dummyJwtResponse = new ResponseSuccess<JwtModel>()
        {
            StatusCode = (int)HttpEnums.InternalServerError
        };
        _jwtGenerator
            .Setup(x => x.GetAsync(It.IsAny<JwtAuthServiceRequestModel>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(_dummyJwtResponse);

        LoginServiceRequestModel _dummyLoginServiceRequestModel = new LoginServiceRequestModel()
        {
            Nit = "5465465",
            Psw = "test"
        };

        CancellationToken token = default;

        await Assert.ThrowsAsync<Exception>(async () =>
        {
            await _serviceTest.PostAsync(_dummyLoginServiceRequestModel, token);
        });
    }

    [Fact(DisplayName = "Jwt Response has null data should throw BadRequest Exception.")]
    public async Task LoginService_JwtResponseHasNullData_ShouldThrowBadRequestException()
    {
        SupplierEntity _dummySupplierEntity = new SupplierEntity();
        _getSupplierByNitRepository
            .Setup(x => x.GetAsync(It.IsAny<SupplierLoginFilterModel>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(_dummySupplierEntity);

        ResponseSuccess<JwtModel> _dummyJwtResponse = new ResponseSuccess<JwtModel>()
        {
            StatusCode = (int)HttpEnums.Ok,
            Data = null
        };
        _jwtGenerator
            .Setup(x => x.GetAsync(It.IsAny<JwtAuthServiceRequestModel>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(_dummyJwtResponse);

        LoginServiceRequestModel _dummyLoginServiceRequestModel = new LoginServiceRequestModel()
        {
            Nit = "5465465",
            Psw = "test"
        };

        CancellationToken token = default;

        await Assert.ThrowsAsync<Exception>(async () =>
        {
            await _serviceTest.PostAsync(_dummyLoginServiceRequestModel, token);
        });
    }

    [Fact(DisplayName = "Valid Jwt Response should return success response with authentication and token.")]
    public async Task LoginService_ValidJwtResponse_ShouldReturnSuccessResponseWithAuthenticationAndToken()
    {
        SupplierEntity _dummySupplierEntity = new SupplierEntity();
        _getSupplierByNitRepository
            .Setup(x => x.GetAsync(It.IsAny<SupplierLoginFilterModel>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(_dummySupplierEntity);

        JwtModel _dummyJwtData = new JwtModel()
        {
            BearerToken = "some-token"
        };
        ResponseSuccess<JwtModel> _dummyJwtResponse = new ResponseSuccess<JwtModel>()
        {
            StatusCode = (int)HttpEnums.Ok,
            Data = _dummyJwtData
        };
        _jwtGenerator
            .Setup(x => x.GetAsync(It.IsAny<JwtAuthServiceRequestModel>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(_dummyJwtResponse);

        LoginServiceRequestModel _dummyLoginServiceRequestModel = new LoginServiceRequestModel()
        {
            Nit = "5465465",
            Psw = "test"
        };

        CancellationToken token = default;

        ResponseSuccess<LoginServiceResponseModel> result = await _serviceTest.PostAsync(_dummyLoginServiceRequestModel, token);
                
        Assert.NotNull(result.Data);
        Assert.True(result.Data.IsAuthenticated);
        Assert.Equal("some-token", result.Data.Jwt);
    }
}