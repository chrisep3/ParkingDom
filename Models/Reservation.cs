using System.ComponentModel.DataAnnotations.Schema;

namespace Parking.Models
{
    public class Reservation
    {

        public int Id { get; set; } //το Id είναι int, το δίνει αυτόματα η Βάση Δεδομένων (SQL), ξεκινώντας από το 1 και ανεβαίνοντας (+1) σε κάθε νέα εγγραφή.


        public string UserId { get; set; } = string.Empty; // για κάθε κράτηση που κάνει ένας χρήστης θα πρέπει όταν κάνει Login να έχει ένΑ UserId
        [ForeignKey(nameof(UserId))]
        public AppUser User { get; set; } = null!;



        public int ParkingId { get; set; } //Το πατάει ο χρήστης στο Create.chtml. 
        // αυτή η κράτηση ανήκει στο ParkingLot με Id = 3 πχ, άρα σχετίζεται με το ParkingLot
        public string Make { get; set; } = string.Empty;

        public string LicencePlate { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        // Η ΣΧΕΣΗ (Navigation Property): Λέει στη C# ότι κάθε κράτηση "ανήκει" σε ένα πάρκινγκ

        [ForeignKey("ParkingId")]
        public ParkingLot ParkingLot { get; set; } = null!; // Navigation Property λέγεται το ParkinLot

        //Το ParkingId δείχνει σε ένα ParkingLot.


        // Ουσιαστικά επιτρέπει να κάνω χρήση όλου του μοντέλου ParkingLot, αφού πρώτα έχω κανει Include   πχ..
        // var name = reservation.ParkingLot.Name;




    }
}
