using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WikiAnimal.Domain.Model;

namespace WikiAnimal.Domain
{
    public class AnimalDatabaseContext : DbContext
    {
        public AnimalDatabaseContext(DbContextOptions<AnimalDatabaseContext> options) : base(options)
        {
            ModelInitializer.Initialize(this);
        }

        public virtual DbSet<Animal> Animals { get; set; }
        public virtual DbSet<TypeOfAnimal> TypeOfAnimals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // использование Fluent API
            base.OnModelCreating(modelBuilder);
        }
    }
}