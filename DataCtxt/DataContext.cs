﻿using Microsoft.EntityFrameworkCore;
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
        public DbSet<New> news { get; set; }
        public DbSet<Message> messages { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    }
}