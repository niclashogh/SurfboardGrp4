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
            using (var context = new BoardContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<BoardContext>>()))
            {
                // Look for any movies.
                if (context.Board.Any())
                {
                    return;   // DB has been seeded
                }

                context.Board.AddRange(
                    new Board
                    {
                        Name = "Board A",
                        Length = 10,
                        Width = 10,
                        Thickness = 10,
                        Volume = 10,
                        Type = "Unknown",
                        Price = 11,
                        Equipment = "",
                        ImgUrl = "https://www.stuffdesign.dk/wp-content/uploads/2021/01/dd-13251n.jpg"
                    },

                    new Board
                    {
                        Name = "Board B",
                        Length = 10,
                        Width = 10,
                        Thickness = 10,
                        Volume = 10,
                        Type = "Unknown",
                        Price = 11,
                        Equipment = "",
                        ImgUrl = "https://www.stuffdesign.dk/wp-content/uploads/2021/01/dd-13251n.jpg"
                    },

                    new Board
                    {
                        Name = "Board C",
                        Length = 10,
                        Width = 10,
                        Thickness = 10,
                        Volume = 10,
                        Type = "Unknown",
                        Price = 11,
                        Equipment = "",
                        ImgUrl = "https://www.stuffdesign.dk/wp-content/uploads/2021/01/dd-13251n.jpg"
                    },

                    new Board
                    {
                        Name = "Board D",
                        Length = 10,
                        Width = 10,
                        Thickness = 10,
                        Volume = 10,
                        Type = "Unknown",
                        Price = 11,
                        Equipment = "",
                        ImgUrl = "https://www.stuffdesign.dk/wp-content/uploads/2021/01/dd-13251n.jpg"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}