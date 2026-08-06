using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parking.Models;


namespace Parking.Controllers
{
    [Authorize(Roles = "Owner")]
    public class OwnerChangeSpotsController : Controller
    {
        private readonly UserManager<AppUser> _userManager; 
        private readonly AppDbContext _context; 

        public OwnerChangeSpotsController(UserManager<AppUser> userManager, AppDbContext context)                        //η μεταβλητή userManager που φτάνει στον constructor σου, έρχεται γεμάτη με όλο το λογισμικό διαχείρισης χρηστών της Microsoft,
                                                                                                                         //ρυθμισμένο ειδικά για το δικό σου μοντέλο (AppUser).
        {                                        
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Welcome() // Το User από το Cookie υπάρχει.
        {

           


            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized();
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return Unauthorized();
            }


            var parx = await _context.parkings.FirstOrDefaultAsync(p => p.OwnerId == userId); // παίρνω το πάρκινγκ που ανήκει στον User που είναι συνδεδεμένος στο σύστημα
            
            if (parx == null)
            {
                return View("NoParking");
            }

            var x = new OwnerChangeSpotsViewModel();

            x.OwnerName = user.FullName; // περνάω το όνομα του χρήστη που είναι συνδεδεμένος στο σύστημα
            x.Location = parx.Location;
            x.TotalSpots = parx.TotalSpots;
            x.ReservedSpots = parx.ReservedSpots;
            x.FreeSpots = parx.TotalSpots - parx.ReservedSpots;
            x.ParkingId = parx.Id; // τα Id τα χει δωσει αυτοματα η βαση

            

            return View(x);
        }



        [HttpPost]
        public async Task <IActionResult> Welcome(OwnerChangeSpotsViewModel model) 
        {

            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }


            //p.OwnerId == userId, έτσι ώστε όπως και στους Users
            // να αλλάξει το ParkingId της φόρμας και να πειράξει parking άλλου owner
            var dbParking = await _context.parkings.FirstOrDefaultAsync(p =>p.Id == model.ParkingId && p.OwnerId == userId); 

            if (dbParking == null) 
            
            {
                return View("NoParking");
            }

            if (model.ReservedSpots < 0 || model.ReservedSpots > dbParking.TotalSpots)// πιανω τα σφλαματα και δίνω πίσω το μοντέλο εκ νεου

            {
                ModelState.AddModelError("", "Εισάγετε έγκυρο αριθμό κρατημένων θέσεων");


                var viewmodeltobecorrected = new OwnerChangeSpotsViewModel();

                viewmodeltobecorrected.ParkingId = dbParking.Id;
                viewmodeltobecorrected.OwnerName = user.FullName;
                viewmodeltobecorrected.Location = dbParking.Location;
                viewmodeltobecorrected.TotalSpots = dbParking.TotalSpots;
                viewmodeltobecorrected.FreeSpots = dbParking.TotalSpots - dbParking.ReservedSpots;

                return View(viewmodeltobecorrected);

                

            }


           

            dbParking.ReservedSpots = model.ReservedSpots; 

            await _context.SaveChangesAsync();

            return View("FinalSpots", dbParking);
                
          



        }
    }

}
