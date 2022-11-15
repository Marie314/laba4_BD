﻿using HairdressingSalon.DAL.Autocomplete;
using HairdressingSalon.DAL.Database.EntitiesConfiguration;
using HairdressingSalon.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairdressingSalon.DAL.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceKind> ServiceKinds { get; set; }
        public DbSet<Worker> Workers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            DbAutocompleter.GenerateRandomValues();

            modelBuilder.ApplyConfiguration(new ClientsConfiguration());
            modelBuilder.ApplyConfiguration(new FeedbacksConfiguration());
            modelBuilder.ApplyConfiguration(new OrdersConfiguration());
            modelBuilder.ApplyConfiguration(new ServiceKindsConfiguration());
            modelBuilder.ApplyConfiguration(new ServicesConfiguration());
            modelBuilder.ApplyConfiguration(new WorkersConfiguration());
        }
    }
}
