using System.ComponentModel.DataAnnotations;

namespace D36_AspNetCoreMvc2Introduction.Models.Security
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
