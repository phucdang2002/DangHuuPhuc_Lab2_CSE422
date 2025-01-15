using System.ComponentModel.DataAnnotations;

namespace Lab2_DangHuuPhuc_CSE422.Models
{
    public class User
    {
        public int Id { get; set; }
        [RegularExpression("^[a-zA-Z]+([ '-][a-zA-Z]+)*$\r\n")]
        [Required]
        public string? Name { get; set; }
        [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$\r\n")]
        [Required]
        public string? Email { get; set; }
        [RegularExpression("^[0-9]*$")]
        [MaxLength(10)]
        [Required]
        public string? PhoneNumber {  get; set; }
    }
}
