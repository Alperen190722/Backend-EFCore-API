using System.ComponentModel.DataAnnotations;

namespace D40_Abc.Northwind.MvcWebUI.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        //[Required]
        //public string ConfirmPassword { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
