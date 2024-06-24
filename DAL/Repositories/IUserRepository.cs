using DAL.Models;
using System.Collections.Generic;

namespace DAL.Repositories
{
    public interface IUserRepository
    {
        User GetUser(string username, string password);
        IEnumerable<User> GetAllUsers();
        User GetUserById(int id);
        User GetUserByEmail(string email);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);

        // Add methods for Demográficos
        Demografico GetDemograficoById(int id);
        void UpdateDemografico(Demografico demografico);
    }
}
