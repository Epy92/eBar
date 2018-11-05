using System.ComponentModel.DataAnnotations;

namespace eBarPortal.Models.Account
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Parola")]
        public string Password { get; set; }
    }
}
