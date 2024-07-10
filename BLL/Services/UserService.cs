using BLL.Interfaces;
using DAL.Models;
using DAL.Repositories;
using System.Collections.Generic;

namespace BLL.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User Authenticate(string username, string password)
        {
            return _userRepository.GetUser(username, password);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }

        public void Register(User user)
        {
            _userRepository.AddUser(user);
        }

        User GetUserById(int id)
        {
            return _userRepository.GetUserById(id);
        }
    }
}
