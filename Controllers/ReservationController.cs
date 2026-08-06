using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parking.Models;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authorization;


//////// AUTHORIZE
//Αν κάποιος προσπαθήσει να χτυπήσει στο URL π.χ. /Reservation/Create χωρίς να έχει κάνει πρώτα Login,
//το .NET τον πετάει αυτόματα έξω και τον στέλνει στη σελίδα του Login.


//Roles = "User")
//Δεν αρκεί απλά να έχει κάνει Login κάποιος, πρέπει να έχει συγκεκριμένα τον ρόλο "User".







namespace Parking.Controllers
{
    [Authorize(Roles = "User")]
    public class ReservationController : Controller
    {

        private readonly AppDbContext _context;

        public ReservationController(AppDbContext context)
        {

            _context = context;

        }

        public async Task <IActionResult> Create() 
        {

     

            var x = new ReservationParkingLotViewModel(); 

            x.ParkingLots = await _context.parkings.ToListAsync();  // στο πεδίο του ParkingLots, περναω τη λιστα παρκιγκ της βασης, με όλα του τα πεδία

            return View(x); 


        }

        [HttpPost]
        public async Task <IActionResult> Create(ReservationParkingLotViewModel reservation)
        {
            if (!ModelState.IsValid) 
            {

           

               reservation.ParkingLots= await _context.parkings.ToListAsync();  
               return View(reservation);


            }




            // Βρίσκω το parking που επέλεξε ο χρήστης.
            var parking = await _context.parkings.FirstOrDefaultAsync(p => p.Id == reservation.ParkingId);

            // Αν δεν υπάρχει parking με αυτό το ID,
            // επιστρέφω τη φόρμα με μήνυμα λάθους.
            if (parking == null)
            {
                ModelState.AddModelError("", "Παρακαλώ εισάγετε έγκυρο Id");

                reservation.ParkingLots =await _context.parkings.ToListAsync();

                return View(reservation);
            }


            
            // Δεν επιτρέπεται κράτηση να ξεκινά στο παρελθόν.
            if (reservation.StartTime < DateTime.Now)
            {
                ModelState.AddModelError("", "Η κράτηση δεν μπορεί να ξεκινά στο παρελθόν.");

                reservation.ParkingLots =await _context.parkings.ToListAsync();

                return View(reservation);
            }


            
            // Η έναρξη πρέπει να είναι πριν από τη λήξη.
            if (reservation.StartTime >= reservation.EndTime)
            {
                ModelState.AddModelError( "","Η ημερομηνία έναρξης πρέπει να είναι νωρίτερα από την ημερομηνία λήξης.");

                reservation.ParkingLots = await _context.parkings.ToListAsync();

                return View(reservation);
            }



            // Βρίσκω πόσα αυτοκίνητα είναι μέσα στο parking την ώρα που θέλει να μπει ο νέος πελάτης
            var overlappingReservations =await _context.reservations.CountAsync(r =>
                    r.ParkingId == reservation.ParkingId &&
                    r.StartTime < reservation.EndTime &&
                    r.EndTime > reservation.StartTime);



            // Αν τα αμάξια που έχουν ήδη κρατημένες θέσεις(overlappingReservations) ξεπερνούν τις συνολικές (TotalSpots), απαγορεύω τη νέα κράτηση και του ξαναστέλνω τη φόρμα
            if (overlappingReservations >= parking.TotalSpots)
            {
                ModelState.AddModelError("", "Δεν υπάρχουν διαθέσιμες θέσεις στο συγκεκριμένο parking για αυτές τις ώρες.");

                reservation.ParkingLots =await _context.parkings.ToListAsync();

                return View(reservation);
            }


            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }




            var dbreservation = new Reservation(); // ΦΤΆΝΩ ΕΔΏ ΑΝ ΌΛΑ ΕΙΝΑΙ ΚΑΛΑ

            {
                dbreservation.ParkingId = reservation.ParkingId;
                dbreservation.Make = reservation.Make;
                dbreservation.LicencePlate = reservation.LicencePlate;
                dbreservation.StartTime = reservation.StartTime;
                dbreservation.EndTime = reservation.EndTime;

                // Συνδέω την κράτηση με τον συνδεδεμένο χρήστη μέσω του foreign key UserId.
                dbreservation.UserId = userId;

            }

            await _context.reservations.AddAsync(dbreservation); // προσθέτω τη νέα κράτηση στη βάση
            await _context.SaveChangesAsync();  


            // Πήγαινε στην ConfirmDeleteNew του ίδιου controller και στείλε στην παράμετρο id, το ID της κράτησης που μόλις αποθηκεύτηκε.
            return RedirectToAction("ConfirmDeleteNew", new { id = dbreservation.Id });
        }

        [HttpGet]

        public async Task <IActionResult> ConfirmDeleteNew(int id)

        {


            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // x.UserId == userId, έτσι ώστε:
            // να μην μπορεί κάποιος να βάλει στη γραμμή διεύθυνσης ένα άλλο Id, π.χ. ...ConfirmDeleteNew/3 και να δει κράτηση άλλου πελάτη 
            var ResToCheck = await _context.reservations.Include(x => x.ParkingLot).FirstOrDefaultAsync(x =>x.Id == id && x.UserId == userId);

           

           

            if (ResToCheck == null)

            {
                return NotFound();
            }

            return View(ResToCheck);

        }

        [HttpPost]

        public async Task <IActionResult> Delete (int id)

        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // μόνο ο χρήστης που έκανε την κράτηση μπορεί να την διαγράψει (x.UserId == userId)
            var ResToDelete = await _context.reservations.FirstOrDefaultAsync(x =>x.Id == id && x.UserId == userId);

            if (ResToDelete == null)
            {
                return NotFound();

            }

            _context.reservations.Remove(ResToDelete);
            await _context.SaveChangesAsync();

            return RedirectToAction("Create");
        
        }


    }

}
