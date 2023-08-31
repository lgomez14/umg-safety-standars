using System.Reflection.Metadata;
using umg_safety_standars.Common.Resources.Models;

namespace umg_safety_standars.Common.Interfaces;

public interface IJwtService<TRequest,TResponse> where TResponse : class where TRequest : class
{
    public Task<ResponseSuccess<TResponse>> GetAsync(TRequest data, CancellationToken cancellationToken = default);
}
