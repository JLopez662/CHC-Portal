using DAL.Models;
using DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using BLL.Services;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CPA.Models;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace CPA.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IUserRepository userRepository, IEmailService emailService, ILogger<AccountController> logger)
        {
            _userRepository = userRepository;
            _emailService = emailService;
            _logger = logger;
        }

        [HttpGet("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("Login")]
        public IActionResult Login(string username, string password)
        {
            using var sha256 = SHA256.Create();
            var hashedPasswordBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            var hashedPassword = BitConverter.ToString(hashedPasswordBytes).Replace("-", "").ToLower();

            var user = _userRepository.GetUser(username, hashedPassword);
            if (user != null)
            {
                if (user.LockoutEnd != null && user.LockoutEnd > DateTime.Now)
                {
                    ViewBag.Error = "Your account has been locked.";
                    return View("~/Views/Home/Index.cshtml");
                }
                TempData["FirstName"] = user.FirstName;
                TempData["Success"] = "You have successfully logged in.";
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                ViewBag.Error = "Invalid username or password";
                return View("~/Views/Home/Index.cshtml");
            }
        }


        [HttpGet("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(string firstName, string lastName, string email, string phone, string username, string password)
        {
            using var sha256 = SHA256.Create();
            var hashedPasswordBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            var hashedPassword = BitConverter.ToString(hashedPasswordBytes).Replace("-", "").ToLower();

            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Phone = phone,
                Username = email,
                Password = hashedPassword
            };

            _userRepository.AddUser(user);

            string subject = "Welcome to CPA Portal";
            string message = $"Hello {firstName},\n\nThank you for registering at CPA Portal.\n\nBest Regards,\nCPA Portal Team";
            await _emailService.SendEmailAsync(email, subject, message);

            ViewBag.Success = "Registration successful. Please log in.";
            return RedirectToAction("Index", "Home", new { success = ViewBag.Success });
        }

        [HttpGet("ForgotPassword")]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var user = _userRepository.GetUserByEmail(email);
            if (user == null)
            {
                ViewBag.Error = "No user found with this email address.";
                return View();
            }

            await SendPasswordRecoveryEmailAsync(user);

            ViewBag.Success = "Password reset link has been sent to your email.";
            return View();
        }

        [HttpGet("ResetPassword")]
        public IActionResult ResetPassword(string token)
        {
            var user = _userRepository.GetAllUsers().FirstOrDefault(u => u.PasswordResetToken == token && u.PasswordResetTokenExpiration > DateTime.Now);
            if (user == null)
            {
                ViewBag.Error = "Invalid or expired password reset token.";
                return View();
            }

            return View(new ResetPasswordViewModel { Token = token });
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _userRepository.GetAllUsers().FirstOrDefault(u => u.PasswordResetToken == model.Token && u.PasswordResetTokenExpiration > DateTime.Now);
            if (user == null)
            {
                ViewBag.Error = "Invalid or expired password reset token.";
                return View();
            }

            using var sha256 = SHA256.Create();
            var hashedPasswordBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(model.NewPassword));
            var hashedPassword = BitConverter.ToString(hashedPasswordBytes).Replace("-", "").ToLower();

            user.Password = hashedPassword;
            user.PasswordResetToken = null;
            user.PasswordResetTokenExpiration = null;

            try
            {
                _userRepository.UpdateUser(user);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error updating user password: {Exception}", ex);
                ViewBag.Error = "An error occurred while resetting the password. Please try again later.";
                return View();
            }

            ViewBag.Success = "Password reset successful. You can now log in with your new password.";
            return RedirectToAction("Index", "Home", new { success = ViewBag.Success });
        }

        [HttpGet("PasswordRecovery")]
        public IActionResult PasswordRecovery()
        {
            return View();
        }

        [HttpPost("PasswordRecovery")]
        public async Task<IActionResult> PasswordRecovery(string email)
        {
            _logger.LogInformation("Password recovery requested for {Email}", email);
            var user = _userRepository.GetUserByEmail(email);
            if (user == null)
            {
                _logger.LogWarning("No user found with email {Email}", email);
                ViewBag.Error = "No user found with this email address.";
                return View();
            }

            await SendPasswordRecoveryEmailAsync(user);

            _logger.LogInformation("Password reset email sent to {Email}", email);
            ViewBag.Success = "Password reset link has been sent to your email.";
            return View();
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
