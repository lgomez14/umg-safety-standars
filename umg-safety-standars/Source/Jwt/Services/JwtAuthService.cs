using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using umg_database.Common.Interfaces;
using umg_database.Sources.Jwt.Models;
using umg_database.Sources.Session.Entities;
using umg_safety_standars.Common.Interfaces;
using umg_safety_standars.Common.Resources.Models;
using umg_safety_standars.Common.Resources.Types;
using umg_safety_standars.Source.Jwt.Models;

namespace umg_safety_standars.Source.Jwt.Services;

public class JwtAuthService : IJwtService<JwtAuthServiceRequestModel, JwtModel>
{
    private readonly JwtSettings _jwtSettings;
    private readonly IPostRepository<SessionEntity, SessionEntity> _sessionPostRepository;
    private readonly IPutRepository<SessionEntity> _sessionPutRepository;

    public JwtAuthService(IConfiguration configuration, IServiceProvider provider)
    {
        _sessionPostRepository = provider.GetRequiredService<IPostRepository<SessionEntity, SessionEntity>>();
        _sessionPutRepository = provider.GetRequiredService<IPutRepository<SessionEntity>>();
        _jwtSettings = new()
        {
            Issuer = configuration["JwtSettings:Issuer"],
            Audience = configuration["JwtSettings:Audience"],
            ExpirationHours = Int32.Parse(configuration["JwtSettings:ExpirationHours"]!),
            JwtKey = configuration["JwtSettings:DataProtectorKey"]
        }; 
    }

    public async Task<ResponseSuccess<JwtModel>> GetAsync(JwtAuthServiceRequestModel data, CancellationToken cancellationToken = default)
    {
        SessionEntity sessionInfo = await _sessionPostRepository.PostAsync(null!);

        JwtModel jwtModel = GenerateNewJwtToken(sessionInfo.SessionId.ToString());

        sessionInfo.SupplierId = data.Id;
        sessionInfo.JwtExpiration = jwtModel.jwtExpiration;
        sessionInfo.JwtToken = jwtModel.BearerToken;

        await _sessionPutRepository.PutAsync(sessionInfo);

        return new(HttpEnums.Ok, jwtModel);

    }

    private JwtModel GenerateNewJwtToken(string sessionId)
    {
        var secretKey = _jwtSettings.JwtKey;
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
        DateTime expiration = DateTime.UtcNow.AddHours(_jwtSettings.ExpirationHours);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, sessionId),
            new Claim("IsAuthenticated", "true")
        };

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: expiration,
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        return new()
        {
            BearerToken = new JwtSecurityTokenHandler().WriteToken(token),
            jwtExpiration = expiration
        };
    }
}
