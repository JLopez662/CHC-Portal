using DAL.Models;
using DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Text;
using System.Security.Cryptography;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text.Json;

namespace CPA.Controllers
{
    public class UserManagementController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        private readonly ILogger<UserManagementController> _logger;

        public UserManagementController(IUserRepository userRepository, IEmailService emailService, ILogger<UserManagementController> logger)
        {
            _userRepository = userRepository;
            _emailService = emailService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
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
        public async Task<IActionResult> CreateUser(string email, string firstName, string lastName, string password, string phone, string role)
        {
            using var sha256 = SHA256.Create();
            var hashedPasswordBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            var hashedPassword = BitConverter.ToString(hashedPasswordBytes).Replace("-", "").ToLower();

            var user = new User
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                Username = email,
                Password = hashedPassword,
                Phone = phone,
                Role = role
            };

            if (ModelState.IsValid)
            {
                _userRepository.AddUser(user);
                //await SendWelcomeEmailAsync(user);
                // Send the welcome email in a background task
                Task.Run(() => SendWelcomeEmailAsync(user));
                return Json(new { success = true, message = "User created successfully", data = user });
            }

            return Json(new { success = false, message = "Failed to create user" });
        }

        private async Task SendWelcomeEmailAsync(User user)
        {
            var subject = "Welcome to CPA Portal";
            var message = $"Hello {user.FirstName},<br><br>Welcome to CPA Portal! Your account has been successfully created.<br><br>Best Regards,<br>CPA Portal Team";

            await _emailService.SendEmailAsync(user.Email, subject, message);
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
        public async Task<IActionResult> UpdateUser(int id, string email, string firstName, string lastName, string phone, string password, string role)
        {
            _logger.LogInformation("UpdateUser called with ID: {Id}, Email: {Email}, FirstName: {FirstName}, LastName: {LastName}, Phone: {Phone}, Role: {Role}", id, email, firstName, lastName, phone, role);

            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                _logger.LogWarning("User with ID {Id} not found", id);
                return Json(new { success = false, message = "User not found" });
            }

            user.Email = email;
            user.FirstName = firstName;
            user.LastName = lastName;
            user.Phone = phone;
            user.Role = role;

            if (!string.IsNullOrEmpty(password))
            {
                using var sha256 = SHA256.Create();
                var hashedPasswordBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var hashedPassword = BitConverter.ToString(hashedPasswordBytes).Replace("-", "").ToLower();
                user.Password = hashedPassword;
            }

            _userRepository.UpdateUser(user);

            if (User.Identity.Name == user.Email)
            {
                HttpContext.Session.SetString("UserRole", user.Role);

                // Re-authenticate the user with updated claims
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim("FullName", user.FirstName),
                    new Claim(ClaimTypes.Role, user.Role)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(30)
                    });
            }

            _logger.LogInformation("User updated successfully: {User}", user);

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
        public IActionResult LockUnlock([FromBody] JsonElement payload)
        {
            if (!payload.TryGetProperty("id", out JsonElement idElement) || idElement.ValueKind != JsonValueKind.Number)
            {
                _logger.LogWarning("LockUnlock called with invalid payload or null ID");
                return Json(new { success = false, message = "Invalid request data" });
            }

            int id = idElement.GetInt32();
            _logger.LogInformation("LockUnlock called with ID: {UserId}", id); // Log the user ID for debugging

            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                _logger.LogWarning("User with ID {UserId} not found", id);
                return Json(new { success = false, message = "User not found" });
            }

            try
            {
                if (user.LockoutEnd != null && user.LockoutEnd > DateTime.Now)
                {
                    user.LockoutEnd = null; // Unlock the user
                    _userRepository.UpdateUser(user);
                    _logger.LogInformation("User unlocked successfully: {User}", user);
                    return Json(new { success = true, message = "User Unlocked Successfully" });
                }
                else
                {
                    user.LockoutEnd = DateTime.Now.AddYears(1000); // Lock the user
                    _userRepository.UpdateUser(user);
                    _logger.LogInformation("User locked successfully: {User}", user);
                    return Json(new { success = true, message = "User Locked Successfully" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while locking/unlocking user with ID {UserId}", id);
                return Json(new { success = false, message = "Error while locking/unlocking user" });
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
