using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? File { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }

    }
}
