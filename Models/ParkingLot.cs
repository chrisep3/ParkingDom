using System.ComponentModel.DataAnnotations.Schema;

namespace Parking.Models
{
    public class ParkingLot
    {
        public int Id { get; set; } // αυτό το εχω βάλει στο AppDbContext στο Entity
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;

        public string OwnerId { get; set; } = string.Empty; // για να κολλάω εδώ το Id του Owner (που αυτόματα του δίνει το EF)
                                                            // οταν φτιαχνεται ο Owner κάνει Login
        [ForeignKey(nameof(OwnerId))]
        public AppUser Owner { get; set; } = null!;                               //ΕΠΕΙΔΗ ΔΕΝ ΥΠΑΡΧΕΙ OwnerId στο HasData, για να μη σκάει θέλει ερωτηματικό στο πεδίο OwnerId


        public int TotalSpots { get; set; } 

        public int ReservedSpots { get; set; } // Αυτό θα πειράζει ο ιδιοκτήτης live

        public int AvailableSpots => TotalSpots - ReservedSpots;

        public decimal PricePerHour { get; set; }



    }
}
