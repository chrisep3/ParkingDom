namespace Parking.Models
{
    public class OwnerChangeSpotsViewModel
    {

        public string OwnerName { get; set; } = string.Empty;

        public int ParkingId { get; set; }

        public string Location { get; set; } = string.Empty;

        public int TotalSpots { get; set; }

        public int ReservedSpots { get; set; } // Αυτό θα πειράζει ο ιδιοκτήτης live


        public int FreeSpots { get; set; }






    }
}
