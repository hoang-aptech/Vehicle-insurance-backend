using Microsoft.EntityFrameworkCore;
using vehicle_insurance_backend.models;

namespace vehicle_insurance_backend.DataCtxt
{
    public class DataContext : DbContext
    {
        public DbSet<CustomerInformation> CustomerInformation { get; set; }
        public DbSet<VehicleInformation> VehicleInformation { get; set; }
        public DbSet<InsuranceProcess> InsuranceProcess { get; set; }
        public DbSet<Estimate> Estimates { get; set; }
        public DbSet<CompanyExpenses> CompanyExpenses { get; set; }
        public DbSet<CompanyBillingPolicy> CompanyBillingPolicy { get; set; }
        public DbSet<ClaimDetails> ClaimDetails { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    }
}
