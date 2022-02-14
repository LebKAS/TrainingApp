using DatingApp.api.Models;

namespace DatingApp.api.Data
{
    public interface IDatingRepository
    {
         void add<T> (T entity) where T:class;
         void Delete<T> (T entity) where T:class;
         Task<bool> SaveAll();
         Task<IEnumerable<User>> GetUsers();
         Task<User> GetUser(int Id);

    }
}