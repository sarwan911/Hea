using Hea.Models;
using Hea.Data;
using Microsoft.EntityFrameworkCore;

namespace Hea.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly Context _context;
        public UserRepository(Context context)
        {
            _context = context;
        }
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<User> GetUserById(int id)
        {
            return await _context.Users.FindAsync(id);
        }
        public async Task<User> CreateUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<User> UpdateUser(User user)
        {
            var existingUser = await _context.Users.FindAsync(user.UserId);
            if (existingUser == null) return null;
            _context.Entry(existingUser).CurrentValues.SetValues(user);
            await _context.SaveChangesAsync();
            return existingUser;
        }
        public async Task<bool> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
        public IEnumerable<User> GetAllDoctors()
        {
            return _context.Users.Where(u => u.Role == "Doctor").ToList();
        }

        public IEnumerable<User> GetAllPatients()
        {
            return _context.Users.Where(u => u.Role == "Patient").ToList();
        }
    }
}
