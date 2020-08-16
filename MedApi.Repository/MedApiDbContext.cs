namespace MedApi.Repository
{
    using Entities;
    using Microsoft.EntityFrameworkCore;

    public class MedApiDbContext : DbContext
    {
        public MedApiDbContext(DbContextOptions<MedApiDbContext> options)
            : base(options)
        { }

        public DbSet<Medication> Medications { get; set; }
    }
}
