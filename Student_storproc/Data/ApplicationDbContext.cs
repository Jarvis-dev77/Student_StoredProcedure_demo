using Microsoft.EntityFrameworkCore;
using Student_storproc.Models;

namespace Student_storproc.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {


        }
        public DbSet<Student> Students { get; set; }
        
    }
}
