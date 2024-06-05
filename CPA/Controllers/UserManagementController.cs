using DAL.Models;
using DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CPA.Controllers
{
    public class UserManagementController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserManagementController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            var users = _userRepository.GetAllUsers();
            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                _userRepository.AddUser(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        public IActionResult Edit(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                _userRepository.UpdateUser(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        [HttpPost]
        public IActionResult UpdateUser(int id, string email, string firstName, string lastName)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                return Json(new { success = false, message = "User not found" });
            }

            user.Email = email;
            user.FirstName = firstName;
            user.LastName = lastName;

            _userRepository.UpdateUser(user);

            return Json(new { success = true, message = "User updated successfully" });
        }

        public IActionResult Delete(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _userRepository.DeleteUser(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult LockUnlock([FromBody] int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                return Json(new { success = false, message = "Error while Locking/Unlocking" });
            }

            if (user.LockoutEnd != null && user.LockoutEnd > DateTime.Now)
            {
                user.LockoutEnd = DateTime.Now;
                _userRepository.UpdateUser(user);
                return Json(new { success = true, message = "User Unlocked Successfully" });
            }
            else
            {
                user.LockoutEnd = DateTime.Now.AddYears(1000);
                _userRepository.UpdateUser(user);
                return Json(new { success = true, message = "User Locked Successfully" });
            }
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userRepository.GetAllUsers();
            return Json(new { data = users });
        }
    }
}
