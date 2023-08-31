using umg_database.Common.Interfaces;
using umg_database.Common.Resource;
using umg_database.Sources.Jwt.Models;
using umg_database.Sources.Session.Entities;
using umg_database.Sources.Supplier.Entities;
using umg_database.Sources.Supplier.Models;
using umg_safety_standars.Common.Interfaces;
using umg_safety_standars.Common.Resources.Models;
using umg_safety_standars.Common.Resources.Types;
using umg_safety_standars.Source.Jwt.Models;
using umg_safety_standars.Source.Session.Models;

namespace umg_safety_standars.Source.Session.Services;

public class LoginService : IPostService<LoginServiceRequestModel, LoginServiceResponseModel>
{
    private readonly IJwtService<JwtAuthServiceRequestModel, JwtModel> _jwtGenerator;
    private readonly IGetRepository<SupplierLoginFilterModel, SupplierEntity> _getSupplierByNitRepository;
    private readonly IPutRepository<SessionEntity> _sessionPutRepository;
    public LoginService(IServiceProvider serviceProvider)
    {
        _jwtGenerator = serviceProvider.GetRequiredService<IJwtService<JwtAuthServiceRequestModel, JwtModel>>();
        _getSupplierByNitRepository = serviceProvider.GetRequiredService<IGetRepository<SupplierLoginFilterModel, SupplierEntity>>();
        _sessionPutRepository = serviceProvider.GetRequiredService<IPutRepository<SessionEntity>>();
    }

    public async Task<ResponseSuccess<LoginServiceResponseModel>> PostAsync(LoginServiceRequestModel data, CancellationToken cancellationToken = default)
    {
        SupplierEntity supplier = await _getSupplierByNitRepository.GetAsync(new()
        {
            Nit = data.Nit,
            Password = data.Psw
        });

        if (supplier == null) 
            return new() 
            { 
                Data = new LoginServiceResponseModel(),
                StatusCode = (int)HttpEnums.NotFound,
            };

        ResponseSuccess<JwtModel> jwtResponse = await _jwtGenerator.GetAsync(new() { Id = supplier.Id }, cancellationToken);

        if (jwtResponse == null)
            throw new Exception(HttpException.BadRequest);

        if (jwtResponse.StatusCode != (int)HttpEnums.Ok) 
            throw new Exception(HttpException.BadRequest);

        if (jwtResponse.Data == null)
            throw new Exception(HttpException.BadRequest);

        return new ResponseSuccess<LoginServiceResponseModel>()
        {
            Data = new LoginServiceResponseModel()
            {
                IsAuthenticated = true,
                Jwt = jwtResponse.Data.BearerToken,
            }
        };
    }
}
