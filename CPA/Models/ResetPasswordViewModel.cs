using System.ComponentModel.DataAnnotations;

namespace CPA.Models
{
    public class ResetPasswordViewModel
    {
        public string Token { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }


}
