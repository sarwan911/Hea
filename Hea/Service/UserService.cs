using Hea.Models;
using Hea.Repository;

namespace Hea.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            try
            {
                return await _userRepository.GetAllUsers();
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                throw new Exception("An error occurred while retrieving users.", ex);
            }
        }

        public async Task<User> GetUserById(int id)
        {
            try
            {
                return await _userRepository.GetUserById(id);
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                throw new Exception($"An error occurred while retrieving the user with ID {id}.", ex);
            }
        }

        public async Task<User> CreateUser(User user)
        {
            try
            {
                return await _userRepository.CreateUser(user);
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                throw new Exception("An error occurred while creating the user.", ex);
            }
        }

        public async Task<User> UpdateUser(User user)
        {
            try
            {
                return await _userRepository.UpdateUser(user);
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                throw new Exception("An error occurred while updating the user.", ex);
            }
        }

        public async Task<bool> DeleteUser(int id)
        {
            try
            {
                return await _userRepository.DeleteUser(id);
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                throw new Exception($"An error occurred while deleting the user with ID {id}.", ex);
            }
        }
    }
}