using Microsoft.AspNetCore.Mvc;
using umg_database.Sources.Supplier.Entities;
using umg_database.Sources.Supplier.Models;
using umg_safety_standars.Common.Interfaces;
using umg_safety_standars.Common.Resources.Models;
using umg_safety_standars.Common.Resources.Types;
using umg_safety_standars.Source.Supplier.Models;

namespace umg_safety_standars.Source.Supplier.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SupplierController : ControllerBase
{
    private readonly IPostService<SupplierRegistryModel, SupplierEntity> _servicePost;
    public SupplierController(IPostService<SupplierRegistryModel, SupplierEntity> servicePost) => _servicePost = servicePost;

    [HttpPost]
    public async Task<ResponseSuccess<SupplierServiceResponseModel>> PostAsync([FromBody] SupplierRegistryModel request)
    {
        ResponseSuccess<SupplierEntity> supplier = await _servicePost.PostAsync(request);

        return new()
        {
            Data = new()
            {
                Message = $"User {supplier.Data!.SupplierBusinessName} created!",
                ResponseData = new()
                {
                    Nit = supplier.Data.Nit,
                    SupplierId = supplier.Data.SupplierId
                }

            },
            StatusCode = (int)HttpEnums.Ok,
            SuccessCode = (int)HttpEnums.Ok
        };
    }
        
    
}
