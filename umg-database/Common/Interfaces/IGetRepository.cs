using System.Diagnostics.CodeAnalysis;

namespace umg_database.Common.Interfaces
{
    public interface IGetRepository<TFilter, TResponse> where TResponse : class
    {
        Task<TResponse> GetAsync(TFilter filter, CancellationToken cancellationToken = default);
    }
}