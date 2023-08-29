using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SurfboardGrp4.Models;

namespace SurfboardGrp4.Data
{
    public class SurfboardGrp4Context : DbContext
    {
        public SurfboardGrp4Context (DbContextOptions<SurfboardGrp4Context> options)
            : base(options)
        {
        }

        public DbSet<SurfboardGrp4.Models.Board> Board { get; set; } = default!;

        // this method is called when Entity Framework Core is building the database model
        // it allows configuration when mapping between C# classes and database tables
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // configure ImageUrls as a non-mapped property
            modelBuilder.Entity<Board>().Ignore(b => b.ImageUrls);
        }
    }
}
