using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Contact
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter your name!")]
        public string Name { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Please enter your email!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter your message!")]
        public string? Message { get; set; }
    }
}
