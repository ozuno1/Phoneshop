using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class RoleViewModel
    {
        [Required]
        [Display(Name = "Role")]
        public string Name { get; set; }
    }
}
