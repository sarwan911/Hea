using Hea.Models;

namespace Hea.Service
{
    public interface IUserService
    {
        public Task<IEnumerable<User>> GetAllUsers();
        public Task<User> GetUserById(int id);
        Task<User> CreateUser(User user);
        Task<User> UpdateUser(User user);
        Task<bool> DeleteUser(int id);
    }
}
