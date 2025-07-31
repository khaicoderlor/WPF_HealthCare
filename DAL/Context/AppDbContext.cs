using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceStep> ServiceSteps { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStep> OrderSteps { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<EggGained> EggGaineds { get; set; }
        public DbSet<EmbryoGained> EmbryoGaineds { get; set; }
        public DbSet<EmbryoTransfer> EmbryoTransfers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer("Server=localhost;Database=Prn_GroupProject;UID=sa;PWD=12345;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>()
                .HasOne(e => e.Patient)
                .WithMany()
                .HasForeignKey(e => e.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderStep>()
                .HasOne(e => e.ServiceStep)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<EmbryoGained>()
                .HasOne(e => e.Order)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
                .HasOne(e => e.OrderStep)
                .WithMany(x => x.Appointments)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
                .HasOne(e => e.Patient)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EmbryoTransfer>()
                .HasOne(e => e.EmbryoGained)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EmbryoTransfer>()
                .HasOne(e => e.Order)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
