using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SurfboardGrp4.Models;

namespace SurfboardGrp4.Data
{
    public class BoardContext : DbContext
    {
        public BoardContext(DbContextOptions<BoardContext> options)
            : base(options)
        {
        }

        public DbSet<SurfboardGrp4.Models.Board> Board { get; set; } = default!;
    }
}
