using Chingari.Services.OrderAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Chingari.Services.OrderAPI.DbContexts
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<OrderDetails> OrderDetails { get; set; }
    }
}
