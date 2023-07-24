using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using EntityDemo.Entities;
using EntityDemo.Entities.Copnfiguration.Seeding;
using EntityDemo.Enums;
using Microsoft.EntityFrameworkCore;

namespace EntityDemo.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<DateTime>().HaveColumnType("date");
            configurationBuilder.Properties<string>().HaveMaxLength(150);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // modelBuilder.Entity<Genre>().HasKey(c => c.Identifire);  

            // modelBuilder.Entity<Genre>().ToTable("GenreTable", "Movies");
            // modelBuilder.Entity<Genre>().Property(c => c.Name)
            // .HasMaxLength(150)
            // .HasColumnName("GenreName")
            // .IsRequired();

            // modelBuilder.Entity<Actor>().Property(p => p.DateOfBirth)
            // .HasColumnType("date");
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            // modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            Seeding.Seed(modelBuilder);
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<CinemaOffer> CinemaOffers { get; set; }
        public DbSet<CinemaHall> CinemaHalls { get; set; }
    }
}