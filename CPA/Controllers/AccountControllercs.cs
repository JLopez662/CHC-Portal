using DAL.Models;
using DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using BLL.Services;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CPA.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;

        public AccountController(IUserRepository userRepository, IEmailService emailService)
        {
            _userRepository = userRepository;
            _emailService = emailService;
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _userRepository.GetUser(username, password);
            if (user != null)
            {
                TempData["Success"] = "You have successfully logged in.";
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                ViewBag.Error = "Invalid username or password";
                return View("~/Views/Home/Index.cshtml");
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [HttpPost]
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
                Username = username,
                Password = hashedPassword
            };

            _userRepository.AddUser(user);

            string subject = "Welcome to CPA Portal";
            string message = $"Hello {firstName},\n\nThank you for registering at CPA Portal.\n\nBest Regards,\nCPA Portal Team";
            await _emailService.SendEmailAsync(email, subject, message);

            ViewBag.Success = "Registration successful. Please log in.";
            return RedirectToAction("Index", "Home", new { success = ViewBag.Success });
        }
    }
}
