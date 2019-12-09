﻿namespace TeisterMask.Data
{
    using Microsoft.EntityFrameworkCore;

    using Models;

    public class TeisterMaskContext : DbContext
    {
        public TeisterMaskContext() { }

        public TeisterMaskContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Task> Tasks { get; set; }

        public DbSet<EmployeeTask> EmployeesTasks { get; set; }

        public DbSet<Project> Projects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>(task =>
            {
                task
                    .HasOne(t => t.Project)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(t => t.ProjectId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<EmployeeTask>(employeeTask =>
            {
                employeeTask.HasKey(et => new { et.EmployeeId, et.TaskId });

                employeeTask
                .HasOne(et => et.Employee)
                .WithMany(e => e.EmployeesTasks)
                .HasForeignKey(et => et.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

                employeeTask
                .HasOne(et => et.Task)
                .WithMany(t => t.EmployeesTasks)
                .HasForeignKey(et => et.TaskId)
                .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}