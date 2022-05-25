using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter your name!")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "Please enter your phone number!")]
        public string CustomerPhone { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Please enter your email!")]
        public string CustomerEmail { get; set; }
        [Required(ErrorMessage = "Please enter your address!")]
        public string CustomerAddress { get; set; }
        public IList<OrderDetail> Details { get; set; }
    }
}
