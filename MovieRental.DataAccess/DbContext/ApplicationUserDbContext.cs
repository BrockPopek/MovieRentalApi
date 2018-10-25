using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.IO;
using MovieRental.DataAccess.Entities;


namespace MovieRental.DataAccess.DbContext
{
    public class ApplicationUserDbContext : IdentityDbContext<ApplicationUser>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                       new ConfigurationBuilder()
                           .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), $"appsettings.json"))
                           .Build()
                           .GetConnectionString("DatabaseConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // use the following line to update migrations in a PowerShell prompt.
            // Add-Migration WhateverName -Project MovieRental.DataAccess

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Id = Guid.NewGuid().ToString(), Name = "Manager", NormalizedName = "MANAGER" },
                new IdentityRole() { Id = Guid.NewGuid().ToString(), Name = "Customer", NormalizedName = "CUSTOMER" });
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Rental> Rentals { get; set; }

    }
}
