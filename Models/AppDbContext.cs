using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MvcMovie.Models
{
    public class AppDbContext : IdentityDbContext{
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
        public DbSet<Movie> Movies {get; set;}

        public DbSet<Comments> Comments {get; set;}
    }
}