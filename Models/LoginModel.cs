using System.ComponentModel.DataAnnotations; // για τα Required

namespace Parking.Models
{
    public class LoginModel
    {

     // Τα όποια λάθη τα πιάνει το Modelstate.IsValid στον Controller
        
        [Required(ErrorMessage = " Yποχρεωτικό Πεδίο")]

        public string Email { get; set; } = string.Empty;


        [Required(ErrorMessage = " Yποχρεωτικό Πεδίο")]
        public string Password { get; set; } = string.Empty;



    }
}
