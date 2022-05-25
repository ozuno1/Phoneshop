using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<WebApplication1.Models.Product>? Product { get; set; }
        public DbSet<WebApplication1.Models.ProductCategory>? ProductCategory { get; set; }
        public DbSet<WebApplication1.Models.Blog>? Blogs{ get; set; }
        public DbSet<WebApplication1.Models.Order>? Orders{ get; set; }
        public DbSet<WebApplication1.Models.OrderDetail>? OrderDetails{ get; set; }
        public DbSet<WebApplication1.Models.Contact>? Contacts{ get; set; }


    }
}