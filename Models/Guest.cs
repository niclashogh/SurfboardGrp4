using System.ComponentModel.DataAnnotations;

namespace mvc_surfboard.Models
{
    public class Guest
    {
        public ICollection<Rental>? Rentals { get; set; }
        
        [DataType(DataType.EmailAddress), Key]
        public string Email { get; set; }

    }
}
