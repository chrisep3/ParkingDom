using System.ComponentModel.DataAnnotations;

namespace Parking.Models
{
    public class RegisterModel //υπάρχει για να παραλάβει όσα γράφει ο χρήστης
    {                          // Η ΒΑΣΗ ΧΡΗΣΙΜΟΠΟΙΕΙ ΤΟ AppUser
        
        [Required(ErrorMessage = " Yποχρεωτικό Πεδίο")]
        public string FullName { get; set; } = string.Empty;


        [EmailAddress]
        [Required(ErrorMessage = " Yποχρεωτικό Πεδίο")]
        public string Email { get; set; } = string.Empty;


        [Required(ErrorMessage = " Yποχρεωτικό Πεδίο")]
        public string Password { get; set; } = string.Empty;


        [Required(ErrorMessage = " Yποχρεωτικό Πεδίο")]
        public string ConfirmationPassword { get; set; } = string.Empty;


    }
}
