using System.ComponentModel.DataAnnotations;

namespace Parking.Models
{
    public class ReservationParkingLotViewModel
    {

        public List<ParkingLot> ParkingLots { get; set; } = new List<ParkingLot>(); // ΑΥΤΑ ΕΙΝΑΙ ΤΟΥ ParkingLot


        
        /// ΟΛΑ ΑΠΟ ΕΔΩ ΚΑΙ ΚΑΤΩ ΕΙΝΑΙ του Reservation Model

        [Required(ErrorMessage = " Yποχρεωτικό Πεδίο")]
        public int ParkingId { get; set; }

        [Required(ErrorMessage = " Yποχρεωτικό Πεδίο")]
        public string Make { get; set; } = string.Empty;


        [Required(ErrorMessage = " Yποχρεωτικό Πεδίο")]
        public string LicencePlate { get; set; } = string.Empty;

        [Required (ErrorMessage = "Υποχρεωτικό Πεδίο")]
        public DateTime StartTime { get; set; }


        [Required(ErrorMessage = " Yποχρεωτικό Πεδίο")]
        public DateTime EndTime { get; set; }






    }
}
