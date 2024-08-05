using GPTshka4.Models.DbModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GPTshka4.Context
{
    public class ApplicationContext : IdentityDbContext<User> 
    {
        public DbSet<User> Users { get; set; } = null!;

        public DbSet<Message> Messages { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }
}
