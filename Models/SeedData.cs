using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SurfboardGrp4.Data;
using System;
using System.Linq;

namespace SurfboardGrp4.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new SurfboardGrp4Context(
                serviceProvider.GetRequiredService<
                    DbContextOptions<SurfboardGrp4Context>>()))
            {
                // Look for any movies.
                if (context.Board.Any())
                {
                    return;   // DB has been seeded
                }

                context.Board.AddRange(
                    new Board
                    {
                        Name = "A",
                        Length = 10,
                        Width = 10,
                        Thickness = 10,
                        Volume = 10,
                        Type = "",
                        Price = 10,
                        Equipment = "",
                        ImgUrl = ""
                    },

                    new Board
                    {
                        Name = "B",
                        Length = 10,
                        Width = 10,
                        Thickness = 10,
                        Volume = 10,
                        Type = "",
                        Price = 10,
                        Equipment = "",
                        ImgUrl = ""
                    },

                    new Board
                    {
                        Name = "C",
                        Length = 10,
                        Width = 10,
                        Thickness = 10,
                        Volume = 10,
                        Type = "",
                        Price = 10,
                        Equipment = "",
                        ImgUrl = ""
                    },

                    new Board
                    {
                        Name = "H",
                        Length = 10,
                        Width = 10,
                        Thickness = 10,
                        Volume = 10,
                        Type = "",
                        Price = 10,
                        Equipment = "",
                        ImgUrl = ""
                    }
                );
                context.SaveChanges();
            }
        }
    }
}