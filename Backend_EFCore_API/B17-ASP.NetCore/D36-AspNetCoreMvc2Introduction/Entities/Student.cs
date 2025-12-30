using System.ComponentModel.DataAnnotations;

namespace D36_AspNetCoreMvc2Introduction.Entities
{
    public class Student
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Öğrenci Adı alanı zorunludur.")]
        [MaxLength(50)]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
