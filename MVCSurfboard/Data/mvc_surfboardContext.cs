using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using mvc_surfboard.Models;

namespace mvc_surfboard.Data
{
    public class mvc_surfboardContext : IdentityDbContext<ApplicationUser>
    {
        public mvc_surfboardContext (DbContextOptions<mvc_surfboardContext> options)
            : base(options)
        {
        }

        public DbSet<mvc_surfboard.Models.Surfboard> Surfboard { get; set; } = default!;

        public DbSet<mvc_surfboard.Models.Rental>? Rental { get; set; }
    }
}   
