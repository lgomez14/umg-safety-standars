using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using umg_safety_standars.Common.Interfaces;
using umg_safety_standars.Common.Resources.Models;
using umg_safety_standars.Source.Session.Models;

namespace umg_safety_standars.Source.Session.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IPostService<LoginServiceRequestModel, LoginServiceResponseModel> _servicePost;
        public AuthController(IPostService<LoginServiceRequestModel, LoginServiceResponseModel> servicePost) => _servicePost = servicePost;

        [HttpPost]
        public async Task<ResponseSuccess<LoginServiceResponseModel>> PostAsync([FromBody] LoginServiceRequestModel request) => await _servicePost.PostAsync(request);

    }
}
