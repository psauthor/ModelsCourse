using JurisTempus.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JurisTempus.Data
{
  public class BillingContext : DbContext
  {

    public BillingContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<Client> Clients { get; set; }
    public DbSet<Case> Cases { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<TimeBill> TimeBills { get; set; }

    protected override void OnModelCreating(ModelBuilder bldr)
    {
      base.OnModelCreating(bldr); // Do the Default

      bldr.Entity<Case>()
        .Property(c => c.FileNumber)
        .IsRequired()
        .HasMaxLength(50);

      bldr.Entity<Client>(t =>
      {
        t.HasData(new Client
        {
          Id = 1,
          Name = "Sloan Taxis",
          Phone = "555-555-1212",
          Contact = "Frank Sloan"
        });


        t.OwnsOne(c => c.Address)
          .HasData(new
          {
            ClientId = 1,
            Address1 = "123 Main Street",
            CityTown = "Atlanta",
            StateProvince = "GA",
            PostalCode = "12345"
          });
      });

      bldr.Entity<Employee>()
       .HasData(new Employee
       {
         Id = 1,
         FirstName = "Shawn",
         LastName = "Wildermuth",
         Role = "Paralegal",
         BillingRate = 45M,
         GovernmentId = "1234567890"
       });


      bldr.Entity<Case>()
        .Property(c => c.Status)
        .HasConversion<int>(); // Store as  int
      bldr.Entity<Case>().Property<int>("ClientId");

      bldr.Entity<Case>()
       .HasData(new
       {
         Id = 1,
         ClientId = 1,
         Status = CaseStatus.Open,
         FileNumber = "ATL12394872"
       });

      bldr.Entity<TimeBill>()
        .HasData(new 
        {
          Id = 1,
          CaseId = 1,
          EmployeeId = 1,
          Rate = 175.00m,
          TimeSegments = 5,
          WorkDate = DateTime.Today,
          WorkDescription = "Entered data for the client"
        });

    }
  }

}
