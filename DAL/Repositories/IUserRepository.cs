using DAL.Models;
using System.Collections.Generic;

namespace DAL.Repositories
{
    public interface IUserRepository
    {
        User GetUser(string username, string password);
        IEnumerable<User> GetAllUsers();
        User GetUserById(int id);
        Task<User> GetUserByUsernameAsync(string username);
        User GetUserByEmail(string email);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
    }
}
