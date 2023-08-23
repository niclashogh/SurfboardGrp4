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
                        Name = "The Minilog",
                        Length = 6,
                        Width = 21,
                        Thickness = 2.75,
                        Volume = 38.8,
                        Type = "Shortboard",
                        Price = 565,
                        Equipment = "",
                        ImgUrl = ""
                    },

                    new Board
                    {
                        Name = "The Wide Glider",
                        Length = 7.1,
                        Width = 21.75,
                        Thickness = 2.75,
                        Volume = 44.16,
                        Type = "Funboard",
                        Price = 685,
                        Equipment = "",
                        ImgUrl = ""
                    },

                    new Board
                    {
                        Name = "The Golden Ratio",
                        Length = 6.3,
                        Width = 21.85,
                        Thickness = 2.9,
                        Volume = 43.22,
                        Type = "Funboard",
                        Price = 695,
                        Equipment = "",
                        ImgUrl = ""
                    },

                    new Board
                    {
                        Name = "Mahi Mahi",
                        Length = 5.4,
                        Width = 20.75,
                        Thickness = 2.3,
                        Volume = 29.39,
                        Type = "Fish",
                        Price = 645,
                        Equipment = "",
                        ImgUrl = ""
                    }
                );
                context.SaveChanges();
            }
        }
    }
}