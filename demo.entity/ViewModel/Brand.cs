using System.ComponentModel.DataAnnotations;

namespace Demo.Entity.ViewModel
{
    public class Brand
    {
        public int BrandId { get; set; }
        [Required]
        [StringLength(20)]
        public string BrandName { get; set; } = "";

        [StringLength(100)]
        public string? BrandDescription { get; set; }

        public string? OperationType { get; set; }

    }
}
