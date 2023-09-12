using Microsoft.EntityFrameworkCore;
using mvc_surfboard.Data;

namespace mvc_surfboard.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new mvc_surfboardContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<mvc_surfboardContext>>()))
            {
                // Look for any surfboards
                if (context.Surfboard.Any())
                {
                    return;   // DB has been seeded
                }

                context.Surfboard.AddRange(
                    new Surfboard
                    {
                        Name = "The Minilog",
                        Length = 6,
                        Width = 21,
                        Thickness = 2.75,
                        Volume = 38.8,
                        Type = "Shortboard",
                        Price = 565,
                        Equipment = "",
                        ImgUrl = "https://images.blue-tomato.com/is/image/bluetomato/304985477_front.jpg-sc3VZ7vW-FFI4Aqdn9Iz-SHhuWM/Lost+Glydra+7+0+Surfboard.jpg?$b8$"
                    },

                    new Surfboard
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

                    new Surfboard
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

                    new Surfboard
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
                    },

                    new Surfboard
                    {
                        Name = "The Emerald Glider",
                        Length = 9.2,
                        Width = 22.8,
                        Thickness = 2.8,
                        Volume = 65.4,
                        Type = "Longboard",
                        Price = 895,
                        Equipment = "",
                        ImgUrl = "https://images.blue-tomato.com/is/image/bluetomato/304199479_front.jpg-3c-91210fWAvivUDHGWJiePAiFo/Bomber+FCS+II+5+10+Softtop+Surfboard.jpg?$b8$"
                    },

                    new Surfboard
                    {
                        Name = "The Bomb",
                        Length = 5.5,
                        Width = 21,
                        Thickness = 2.5,
                        Volume = 33.7,
                        Type = "Shortboard",
                        Price = 645,
                        Equipment = "",
                        ImgUrl = ""
                    },


                    new Surfboard
                    {
                        Name = "Walden Magic",
                        Length = 9.6,
                        Width = 19.4,
                        Thickness = 3,
                        Volume = 80,
                        Type = "Longboard",
                        Price = 1025,
                        Equipment = "",
                        ImgUrl = "https://images.blue-tomato.com/is/image/bluetomato/304199483_front.jpg-DYt5uJfJyxC7tZlhbOGpyt7BFL0/Flash+Eric+Geiselman+FCS+II+5+7+Softtop+Surfboard.jpg?$b8$"
                    },

                    new Surfboard
                    {
                        Name = "Naish One",
                        Length = 12.6,
                        Width = 30,
                        Thickness = 6,
                        Volume = 301,
                        Type = "SUB",
                        Price = 854,
                        Equipment = "Paddle",
                        ImgUrl = "https://images.blue-tomato.com/is/image/bluetomato/304736912_front.jpg-iRn1K4X-y97gnlbsMVx8V83u7yw/Ezi+Rider+7+039+0+Surfboard.jpg?$b8$"
                    },

                    new Surfboard
                    {
                        Name = "Six Tourer",
                        Length = 11.6,
                        Width = 32,
                        Thickness = 6,
                        Volume = 270,
                        Type = "SUB",
                        Price = 611,
                        Equipment = "Fin, Paddle, Pump, Leash",
                        ImgUrl = "https://images.blue-tomato.com/is/image/bluetomato/304736908_front.jpg-H4PsPhTNHmbDVcLjz0z9rBUUEp0/Happy+Hour+Epoxy+6+6+Surfboard.jpg?$b8$"
                    },

                    new Surfboard
                    {
                        Name = "Naish Maliko",
                        Length = 14,
                        Width = 25,
                        Thickness = 6,
                        Volume = 330,
                        Type = "SUB",
                        Price = 1304,
                        Equipment = "Fin, Paddle, Pump, Leash",
                        ImgUrl = ""
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
