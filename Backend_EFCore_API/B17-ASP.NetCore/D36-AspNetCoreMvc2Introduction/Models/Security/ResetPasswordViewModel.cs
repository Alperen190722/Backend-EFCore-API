using System.ComponentModel.DataAnnotations;

namespace D36_AspNetCoreMvc2Introduction.Models.Security
{
    public class ResetPasswordViewModel
    {    
        public string Code { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
