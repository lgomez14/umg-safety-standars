namespace umg_database.Common.Interfaces
{
    public interface IPutRepository<in TRequest> where TRequest : class
    {
        public Task PutAsync(TRequest data);
    }
}