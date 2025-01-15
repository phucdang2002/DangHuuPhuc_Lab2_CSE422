using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab2_DangHuuPhuc_CSE422.Models
{
    public class Device
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public Status Status { get; set; }
        [DisplayName("Date of entry")]
        public DateTime DateOfEntry { get; set; }
    }
}
