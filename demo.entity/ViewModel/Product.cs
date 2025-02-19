using System.ComponentModel.DataAnnotations;

namespace Demo.Entity.ViewModel
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required]
        [StringLength(20)]
        public string ProductName { get; set; } = "";

        [StringLength(100)]
        public string? ProductDescription { get; set; }

        [Range(1.0, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
        [DataType(DataType.Currency)]
        public decimal ProductPrice { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Quantity must be greater than zero")]
        public int ProductQauntity { get; set; }
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }
        public string? OperationType { get; set; }
    }
}
