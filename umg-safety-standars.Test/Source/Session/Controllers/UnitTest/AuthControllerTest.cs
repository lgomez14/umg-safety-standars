using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using umg_safety_standars.Common.Interfaces;
using umg_safety_standars.Common.Resources.Models;
using umg_safety_standars.Source.Session.Controllers;
using umg_safety_standars.Source.Session.Models;
using Xunit;

namespace umg_safety_standars.Test.Source.Session.Controllers.UnitTest
{
    public class AuthControllerTest
    {
        private readonly AuthController _controller;
        private readonly Mock<IPostService<LoginServiceRequestModel, LoginServiceResponseModel>> _servicePost;

        public AuthControllerTest()
        {
            _servicePost = new Mock<IPostService<LoginServiceRequestModel, LoginServiceResponseModel>>();
            _controller = new AuthController(_servicePost.Object);
        }

        [Fact(DisplayName = "PostAsync should return success response.")]
        public async Task AuthController_PostAsync_ShouldReturnSuccessResponse()
        {
            var requestData = new LoginServiceRequestModel
            {
                Nit = "test_nit",
                Psw = "test_password"
            };

            var responseData = new LoginServiceResponseModel
            {
                IsAuthenticated = true,
                Jwt = "test_jwt_token"
            };

            _servicePost
                .Setup(x => x.PostAsync(It.IsAny<LoginServiceRequestModel>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ResponseSuccess<LoginServiceResponseModel> { Data = responseData });

            var result = await _controller.PostAsync(requestData);

            Assert.NotNull(result);
            var okResult = Assert.IsType<ResponseSuccess<LoginServiceResponseModel>>(result);

            if (okResult.Data != null)
            {
                Assert.Equal(responseData.IsAuthenticated, okResult.Data.IsAuthenticated);
                Assert.Equal(responseData.Jwt, okResult.Data.Jwt);
            }
        }
    }
}
