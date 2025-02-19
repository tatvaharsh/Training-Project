using System.ComponentModel.DataAnnotations;

namespace Demo.Entity.ViewModel
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Required]
        [StringLength(20)]
        public string CategoryName { get; set; } = "";

        [StringLength(100)]
        public string? CategoryDescription { get; set; }

        public string? OperationType { get; set; }

    }
}
