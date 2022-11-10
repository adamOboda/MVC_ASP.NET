using Microsoft.EntityFrameworkCore;
using MVC_ASP.NET.Models;

namespace MVC_ASP.NET.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
    }
}
