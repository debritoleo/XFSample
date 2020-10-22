using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using XFSample.Interfaces;
using XFSample.Models;

namespace XFSample.Infra.Data
{
    public class Repository<T> : IRepository<T> where T : class, IBasicEntity, new()
    {
        private readonly SQLiteAsyncConnection _sqliteConnection;
        public Repository(string dbPath)
        {
            _sqliteConnection = new SQLiteAsyncConnection(dbPath);
            _sqliteConnection.CreateTableAsync<Person>().GetAwaiter().GetResult();
        }

        public Task<T> GetAsync(int id)
        {
            return _sqliteConnection.Table<T>()
                                    .Where(x => x.Id == id)
                                    .FirstOrDefaultAsync();
        }

        public Task<List<T>> GetAllAsync()
        {
            return _sqliteConnection.Table<T>().ToListAsync();
        }

        public Task<int> SaveAsync(T item)
        {
            if (item.Id != 0)
                return _sqliteConnection.UpdateAsync(item);
            else
                return _sqliteConnection.InsertAsync(item);
        }

        public Task<int> DeleteAsync(T item)
        {
            return _sqliteConnection.DeleteAsync(item);
        }
    }
}
