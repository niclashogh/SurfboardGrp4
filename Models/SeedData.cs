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
                    },


                    new Board
                    {
                        Name = "The Emerald Glider",
                        Length = 9.2,
                        Width = 22.8,
                        Thickness = 2.8,
                        Volume = 65.4,
                        Type = "Longboard",
                        Price = 895,
                        Equipment = "",
                    },

                    new Board
                    {
                        Name = "The Bomb",
                        Length = 5.5,
                        Width = 21,
                        Thickness = 2.5,
                        Volume = 33.7,
                        Type = "Shortboard",
                        Price = 645,
                        Equipment = "",
                    },


                    new Board
                    {
                        Name = "Walden Magic",
                        Length = 9.6,
                        Width = 19.4,
                        Thickness = 3,
                        Volume = 80,
                        Type = "Longboard",
                        Price = 1025,
                        Equipment = "",
                    },

                    new Board
                    {
                        Name = "Naish One",
                        Length = 12.6,
                        Width = 30,
                        Thickness = 6,
                        Volume = 301,
                        Type = "SUB",
                        Price = 854,
                        Equipment = "Paddle",
                    },

                    new Board
                    {
                        Name = "Six Tourer",
                        Length = 11.6,
                        Width = 32,
                        Thickness = 6,
                        Volume = 270,
                        Type = "SUB",
                        Price = 611,
                        Equipment = "Fin, Paddle, Pump, Leash",
                    },

                    new Board
                    {
                        Name = "Naish Maliko",
                        Length = 14,
                        Width = 25,
                        Thickness = 6,
                        Volume = 330,
                        Type = "SUB",
                        Price = 1304,
                        Equipment = "Fin, Paddle, Pump, Leash",
                    }


                );
                context.SaveChanges();
            }
        }
    }
}