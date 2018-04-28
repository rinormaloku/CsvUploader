using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MfaCsvUploader.Data.Entities;

namespace MfaCsvUploader.Data
{
    public class CsvContext : DbContext
    {
        public CsvContext(DbContextOptions options) : base(options) {}
        
        public DbSet<CsvTeacher> Teachers { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<CsvTeacher>()
                .HasIndex(b => b.Name)
                .IsUnique();
        }
    }
}
