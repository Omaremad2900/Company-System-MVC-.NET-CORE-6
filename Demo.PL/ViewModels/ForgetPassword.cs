using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
    public class ForgetPassword
    {
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
    }
}
