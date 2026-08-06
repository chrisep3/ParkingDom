using Microsoft.AspNetCore.Identity;


namespace Parking.Models
{
    public class AppUser : IdentityUser
    {


        public string FullName { get; set; } = string.Empty; //Id
                                                             //├── UserName
                                                             //├── Email
                                                             //├── PasswordHash
                                                             //├── PhoneNumber...



    }

}
