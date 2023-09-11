using Microsoft.AspNetCore.Identity;

namespace mvc_surfboard.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Rental>? Rentals { get;}
    }
}
