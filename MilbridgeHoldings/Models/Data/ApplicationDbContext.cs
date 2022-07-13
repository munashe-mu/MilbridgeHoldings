using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MilbridgeHoldings.Data;

namespace MilbridgeHoldings.Models.Data
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public DbSet<WorkCentre> WorkCentres { get; set; }
        public DbSet<Machines> Machines { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<JobTitle> JobTitles { get; set; }
    
    }
}




