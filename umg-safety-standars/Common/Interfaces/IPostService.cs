
using umg_safety_standars.Common.Resources.Models;

namespace umg_safety_standars.Common.Interfaces
{
    public interface IPostService<in TRequest, TResponse> where TRequest : class where TResponse : class
    {
        public Task<ResponseSuccess<TResponse>> PostAsync(TRequest data, CancellationToken cancellationToken = default);
        
    }
}