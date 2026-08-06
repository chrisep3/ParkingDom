using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


using Parking.Models;

namespace Parking.Controllers
{
    public class RegisterLogin : Controller
    {

        private readonly UserManager<AppUser> _userManager; //  εκπρόσωπος του UserManager
        private readonly SignInManager<AppUser> _signInManager; //  εκπρόσωπος του SignInManager



        public RegisterLogin(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)  // dependency injection
        {                                                       
            _userManager = userManager;
            _signInManager = signInManager;
        }

        

       
        public IActionResult Index() // Startup
        {
            return View();
        }


        public IActionResult Register() 
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel info) 
        {

            if (!ModelState.IsValid)  
            {
                return View(info);} 


            if (info.ConfirmationPassword !=info.Password)
            {
                ModelState.AddModelError( nameof(info.ConfirmationPassword),"Οι κωδικοί δεν είναι ίδιοι. Προσπαθήστε ξανά.");

                return View(info);

            }


            var user = new AppUser(); 
                                      
            user.Email = info.Email; 
            user.FullName = info.FullName; 
            user.UserName = info.Email;   
            


            
            var baseitem = await _userManager.CreateAsync(user, info.Password); 

            //  Ρωτάμε αν η εγγραφή ΠΕΤΥΧΕ στη βάση
            if (baseitem.Succeeded) //password policy και μοναδικό username/email
            {
                
                    var roleitem = await _userManager.AddToRoleAsync(user, "User"); //δίνουμε ρόλο User

                    if (!roleitem.Succeeded)
                    {
                        foreach (var error in roleitem.Errors)
                        {
                            ModelState.AddModelError("", error.Description);

                        }


                        return View(info); 
                    }

                await _signInManager.SignInAsync(user, false); // Ο χρήστης έχει πλέον συνδεθεί και διαθέτει authentication cookie.

                return RedirectToAction("Create", "Reservation"); //Αυτό στέλνει τον χρήστη στη φόρμα κράτησης
            }

            else // !baseitem.Succeeded

                foreach (var error in baseitem.Errors)  // Αν ΦΤΑΣΕΙ ΕΔΩ, σημαίνει ότι η βάση απέτυχε (π.χ. διπλότυπο email ή αδύναμος κωδικός).
                                                        // Γι' αυτό γεμίζουμε το ModelState με τα πραγματικά λάθη που μας γύρισε η βάση:
                {
                    ModelState.AddModelError("", error.Description);
                }

            return View(info); // εδώ πάει αν η εγγραφή στη βάση αποτύχει



        }

        [HttpGet]
        public IActionResult Login() // εδώ έρχεται όταν πατησει Login Ο χρήστης
        {
            return View();
            
        }

        [HttpPost]
        public async Task <IActionResult> Login(LoginModel info) 
        {

            if (!ModelState.IsValid) 
            {
                return View(info); 

            }

            var singinitem = await _signInManager.PasswordSignInAsync(info.Email, info.Password, false, false);

            if (singinitem.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(info.Email);

                if (user == null)
                {
                    await _signInManager.SignOutAsync();
                    return Unauthorized();
                }

                if (await _userManager.IsInRoleAsync(user, "Owner"))
                {
                    return RedirectToAction("Welcome", "OwnerChangeSpots");
                }

                return RedirectToAction("Create", "Reservation");
            }

            ModelState.AddModelError("", "Λάθος Username ή/και Password");

            return View(info);


        }

        // ΓΙΑ ΝΑ ΚΑΝΕΙ LOGOUT
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "RegisterLogin");
        }


    }
}
