using System.Data.SqlTypes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mvc_surfboard.Models
{
    public class Surfboard
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Navn")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Længde")]
        public double Length { get; set; }
        [Required]
        [Display(Name = "Bredde")]
        public double Width { get; set; }
        [Required]
        [Display(Name = "Tykkelse")]
        public double Thickness { get; set; }
        [Required]
        public double Volume { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        [Display(Name = "Udstyr")]
        public string? Equipment { get; set; }
        [Display(Name = "Billede")]
        public string? ImgUrl { get; set; }
        // public bool isAvalaible {  get; set; }
        public ICollection<Rental>? Rentals { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
