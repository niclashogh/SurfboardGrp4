using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurfboardGrp4.Models
{
    [PrimaryKey(nameof(ID))]
    public class Board
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Thickness { get; set; }
        public double Volume { get; set; }
        public string Type { get; set; }
        public int Price { get; set; }
        public string? Equipment { get; set; }

        public string? ImgUrl { get; set; }
    }

    [PrimaryKey(nameof(Url))]
    public class ImageUrl
    {
        [ForeignKey(nameof(Board.ID))]
        public string? Url { get; set; }

    }
}
