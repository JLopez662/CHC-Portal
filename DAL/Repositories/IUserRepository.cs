using DAL.Models;
using System.Collections.Generic;

namespace DAL.Repositories
{
    public interface IUserRepository
    {
        User GetUser(string username, string password);
        IEnumerable<User> GetAllUsers();
        User GetUserById(int id);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
    }
}
