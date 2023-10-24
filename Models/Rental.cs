using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mvc_surfboard.Models
{
    public class Rental
    {
        public int RentalId { get; set; }
        public string? UserId { get; set; }
        public string? GuestEmail { get; set; }

        [Required]
        public int SurfboardId { get; set; }

        [Required]
        [Display(Name = "Startdato")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "Slutdato")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Display(Name = "Total Pris")]
        [Column(TypeName = "decimal(18, 2)")]
        [Required]
        public decimal TotalCost { get; set; }

        // foreign key attributes specify navigation properties
        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; }
        [ForeignKey("GuestEmail")]
        public Guest? Guest { get; set; }

        [ForeignKey("SurfboardId")]
        public Surfboard? Surfboard { get; set; }

        [Timestamp]
        public byte[]? RowVersion { get; set; }

    }
}
