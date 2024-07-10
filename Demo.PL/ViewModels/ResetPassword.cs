using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
    public class ResetPassword
    {
        [Required(ErrorMessage="New Password is Requird")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required(ErrorMessage ="Confirm New Password is Required")]
        [DataType(DataType.Password)]
        [Compare("NewPassword",ErrorMessage ="Password doesn't match")]
        public string ConfirmNewPassword { get; set; }

    }
}
