using DAL.Models;
using DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Text;
using System.Security.Cryptography;
using BLL.Interfaces;

namespace CPA.Controllers
{
    public class UserManagementController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;

        public UserManagementController(IUserRepository userRepository, IEmailService emailService)
        {
            _userRepository = userRepository;
            _emailService = emailService;
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

        [HttpPost]
        public async Task<IActionResult> CreateUser(string email, string firstName, string lastName, string password, string phone)
        {
            using var sha256 = SHA256.Create();
            var hashedPasswordBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            var hashedPassword = BitConverter.ToString(hashedPasswordBytes).Replace("-", "").ToLower();

            var user = new User
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                Username = email, // Assuming username is same as email
                Password = hashedPassword,
                Phone = phone
            };

            if (ModelState.IsValid)
            {
                _userRepository.AddUser(user);

                return Json(new { success = true, message = "User created successfully", data = user });
            }

            return Json(new { success = false, message = "Failed to create user" });
        }

        [HttpGet]
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
        public async Task<IActionResult> UpdateUser(int id, string email, string firstName, string lastName, string phone, string password)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                return Json(new { success = false, message = "User not found" });
            }

            user.Email = email;
            user.FirstName = firstName;
            user.LastName = lastName;
            user.Phone = phone;

            if (!string.IsNullOrEmpty(password))
            {
                using var sha256 = SHA256.Create();
                var hashedPasswordBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var hashedPassword = BitConverter.ToString(hashedPasswordBytes).Replace("-", "").ToLower();
                user.Password = hashedPassword;
            }

            _userRepository.UpdateUser(user);
            return Json(new { success = true, message = "User updated successfully", data = user });
        }

        [HttpGet]
        public IActionResult GetUserById(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                return Json(new { success = false, message = "User not found" });
            }
            return Json(new { success = true, data = user });
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

        [HttpPost]
        public async Task<IActionResult> PasswordRecovery(string email)
        {
            var user = _userRepository.GetUserByEmail(email);
            if (user == null)
            {
                return Json(new { success = false, message = "User not found" });
            }

            await SendPasswordRecoveryEmailAsync(user);
            return Json(new { success = true, message = "Password recovery email sent successfully" });
        }

        private async Task SendPasswordRecoveryEmailAsync(User user)
        {
            user.PasswordResetToken = Guid.NewGuid().ToString();
            user.PasswordResetTokenExpiration = DateTime.Now.AddHours(1);
            _userRepository.UpdateUser(user);

            var resetLink = Url.Action("ResetPassword", "Account", new { token = user.PasswordResetToken }, Request.Scheme);
            var subject = "Password Reset Request";
            var message = $"Hello {user.FirstName},<br><br>You requested a password reset. Click the link below to reset your password:<br><a href='{resetLink}'>Reset Password</a><br><br>Best Regards,<br>CPA Portal Team";

            await _emailService.SendEmailAsync(user.Email, subject, message);
        }
    }
}
