using Microsoft.EntityFrameworkCore;
using vehicle_insurance_backend.models;

namespace vehicle_insurance_backend.DataCtxt
{
    public class DataContext : DbContext
    {
        public DbSet<User> users { get; set; }
        public DbSet<Advertisement> advertisements { get; set; }
        public DbSet<Billing> billings { get; set; }
        public DbSet<CustomerInsurance> customerInsurances { get; set; }
        public DbSet<CustomerSupport> customerSupports { get; set; }
        public DbSet<Insurance> insurances { get; set; }
        public DbSet<Vehicle> vehicles { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Advertisement>().Property(a => a.type).HasConversion<string>();

            modelBuilder.Entity<Advertisement>().Property(a => a.deleted).HasConversion<string>();

            modelBuilder.Entity<Billing>().Property(a => a.deleted).HasConversion<string>();

            modelBuilder.Entity<CustomerInsurance>().Property(a => a.deleted).HasConversion<string>();

            modelBuilder.Entity<CustomerSupport>().Property(a => a.deleted).HasConversion<string>();

            modelBuilder.Entity<CustomerSupport>().Property(a => a.type).HasConversion<string>();

            modelBuilder.Entity<Insurance>().Property(a => a.deleted).HasConversion<string>();

            modelBuilder.Entity<Insurance>().Property(a => a.name).HasConversion<string>();

            modelBuilder.Entity<User>().Property(a => a.deleted).HasConversion<string>();

            modelBuilder.Entity<User>().Property(a => a.verified).HasConversion<string>();

            modelBuilder.Entity<Vehicle>().Property(a => a.deleted).HasConversion<string>();

        }
        public DbSet<vehicle_insurance_backend.models.New> New { get; set; } = default!;
    }
}
