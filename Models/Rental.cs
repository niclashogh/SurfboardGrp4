using System.ComponentModel.DataAnnotations.Schema;

namespace SurfboardGrp4.Models
{
    public class Rental
    {
        //[ForeignKey(Board.ID)]
        public Board? Board { get; set; }
        public int UserID { get; set; }
        public int RentalID { get; set; }
        public int BoardID { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public double TotalPrice { get; set; }

    }
}
