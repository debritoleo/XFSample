using System.Collections.Generic;
using System.Threading.Tasks;

namespace XFSample.Infra.Data
{
    public interface IRepository<T>
    {
        Task<T> GetAsync(int id);
        Task<List<T>> GetAllAsync();
        Task<int> SaveAsync(T item);
        Task<int> DeleteAsync(T item);
    }
}
