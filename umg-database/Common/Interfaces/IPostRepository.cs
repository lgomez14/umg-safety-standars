namespace umg_database.Common.Interfaces
{
    public interface IPostRepository<TRequest, TResponse> where TResponse : class where TRequest : class
    {
        public Task<TResponse> PostAsync(TRequest data, CancellationToken cancellationToken = default);
    }
}