using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Name { get; set; }
        [StringLength(50, MinimumLength = 5)]
        public string Description { get; set; }
        public decimal Price { get; set; }
        [Display(Name = "Choose image for the product")]
        [Required]
        [NotMapped]
        public IFormFile Image { get; set; }
        public string? File { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Display(Name="Category")]
        public int ProductCategoryId { get; set; }
        public ProductCategory? ProductCategory{ get; set; }
    }
}
