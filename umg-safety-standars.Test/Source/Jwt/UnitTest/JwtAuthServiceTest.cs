using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using umg_database.Common.Interfaces;
using umg_database.Sources.Session.Entities;
using umg_safety_standars.Source.Jwt.Models;
using umg_safety_standars.Source.Jwt.Services;

namespace umg_safety_standars.Test.Source.Jwt.UnitTest
{
    public class JwtAuthServiceTest
    {
        private readonly JwtAuthService _serviceTest;
        private readonly Mock<IPostRepository<SessionEntity, SessionEntity>> _sessionPostRepository;
        private readonly Mock<IPutRepository<SessionEntity>> _sessionPutRepository;

        public JwtAuthServiceTest()
        {
            _sessionPostRepository = new Mock<IPostRepository<SessionEntity, SessionEntity>>();
            _sessionPutRepository = new Mock<IPutRepository<SessionEntity>>();

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    { "JwtSettings:Issuer", "test" },
                    { "JwtSettings:Audience", "test" },
                    { "JwtSettings:ExpirationHours", "1" },
                    { "JwtSettings:DataProtectorKey", "randomkey123456789012345678901234567890" }
                })
                .Build();

            var serviceProvider = new ServiceCollection()
                .AddScoped(_ => _sessionPostRepository.Object)
                .AddScoped(_ => _sessionPutRepository.Object)
                .BuildServiceProvider();

            _serviceTest = new JwtAuthService(configuration, serviceProvider);
        }

        [Fact(DisplayName = "GetAsync should return a valid JwtModel")]
        public async Task GetAsync_ShouldReturnValidJwtModel()
        {
            // Arrange
            var sessionId = Guid.NewGuid();
            var request = new JwtAuthServiceRequestModel { Id = 1 };

            _sessionPostRepository.Setup(x => x.PostAsync(null!, CancellationToken.None))
                .ReturnsAsync(new SessionEntity
                {
                    SessionId = sessionId,
                    Id = 1,
                    SupplierId = 1,
                    JwtExpiration = DateTime.UtcNow,
                    JwtToken = "test",
                    CreatedAt = DateTime.UtcNow
                });

            var result = await _serviceTest.GetAsync(request);

            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.NotNull(result.Data.BearerToken);
            Assert.True(result.Data.jwtExpiration > DateTime.UtcNow);
        }
    }
} 