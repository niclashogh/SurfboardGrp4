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
        public string Name { get; set; }
        [Required]
        public double Length { get; set; }
        [Required]
        public double Width { get; set; }
        [Required] 
        public double Thickness { get; set; }
        [Required]
        public double Volume { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        public string? Equipment { get; set; }
        [Display(Name = "Image")]
        public string? ImgUrl { get; set; }
        // public bool isAvalaible {  get; set; }
        public ICollection<Rental>? Rentals { get; set; }
    }
}
