using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PayrollComputation.Model;
using System;

namespace PayrollComputation.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<PaymentRecord> PaymentRecords { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<TaxYear> TaxYears { get; set; }
    }
}
