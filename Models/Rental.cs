using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mvc_surfboard.Models
{
    public class Rental
    {
        public int RentalId { get; set; }
        [Required]
        public string UserId { get; set; }

        [Required]
        public int SurfboardId { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Display(Name = "Total Cost")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalCost { get; set; }

        // foreign key attributes specify navigation properties
        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; }
        [ForeignKey("SurfboardId")]
        public Surfboard? Surfboard { get; set; } 

    }
}
