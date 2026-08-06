using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Parking.Models;

namespace Parking
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Database Connection

            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=parking.db")); // μαθαίνει πως να φτιάχνει AppDbContext


            builder.Services.AddIdentity<AppUser, IdentityRole>()  
            .AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();              
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/RegisterLogin/Login";
                options.AccessDeniedPath = "/RegisterLogin/Login";
            });

            var app = builder.Build(); 

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }


            // Seed Owners και Parking
            using (var scope = app.Services.CreateScope()) 
            {
               
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();  
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>(); 
                var signInManager = scope.ServiceProvider.GetRequiredService<SignInManager<AppUser>>();
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                // Φτιάξε τους ρόλους (Owner, User)
                if (!roleManager.RoleExistsAsync("Owner").GetAwaiter().GetResult()) // αν δεν υπάρχει Owner
                    roleManager.CreateAsync(new IdentityRole("Owner")).GetAwaiter().GetResult(); // // φτιάξε

                if (!roleManager.RoleExistsAsync("User").GetAwaiter().GetResult())
                    roleManager.CreateAsync(new IdentityRole("User")).GetAwaiter().GetResult();

                // seed τους Owners

                var ownersinram = new[] // στη μνήμη Ram
                {
                    new {Email="owner1parking@gmail.com", FullName="Ιδιοκτήτης Parking A" }, // το FullName ειναι custom πεδίο του AppUser, όχι του IdentityUser
                    new { Email="owner2parking@gmail.com", FullName="Ιδιοκτήτης Parking B"},
                    new  {Email="owner3parking@gmail.com", FullName="Ιδιοκτήτης Parking C" }

               };

                foreach (var o in ownersinram) // για καθε ενα απο τους επιθυμητους χρήστες

                {

                    var potentialuser = await userManager.FindByEmailAsync(o.Email); // ψάξε αν υπάρχει ήδη

                    if (potentialuser == null) // αν δεν υπάρχει, θα φτιάξω έναν μόνιμο με βάση τα στοιχεία του επιθυμητού. Πιθανόν να υπάρχει ήδη ΑΛΛΟΣ με αυτό το Mail

                    {

                        var permanentuser = new AppUser() // φτιάχνω το μόνιμο χρήστη. ΣΤΗ RAM! ΔΕΝ ΤΟΝ ΕΧΩ ΑΚΟΜΗ ΒΑΛΕΙ ΣΤΗ ΒΑΣΗ! Ο USERMANAGER, κάνει αλλαγές στη βάση

                        {

                            Email = o.Email,
                            UserName = o.Email,
                            FullName = o.FullName

                        };
                        

                        var addtodatabase = await userManager.CreateAsync(permanentuser, "Password123!"); 

                        if (addtodatabase.Succeeded) 

                        {
                            await userManager.AddToRoleAsync(permanentuser, "Owner"); // ΤΟΥ ΔΙΝΩ ΡΟΛΟ owner. Ο UserManager διαχειρίζεται τον πίνακα AspNetUsers και τον πίνακα σύνδεσης AspNetUserRoles



                            var parking = new ParkingLot(); 

                            if (o.Email == "owner1parking@gmail.com")

                            {



                                //parking.Id = 1; το δινει αυτόματα η βάση.
                                parking.Name = "Parking A";
                                parking.Location = "Chalandri, Grammou 12";
                                parking.TotalSpots = 10;
                                parking.ReservedSpots = 0;
                                parking.PricePerHour = 2.50m;



                            }



                            else if (o.Email == "owner2parking@gmail.com")

                            {

                                // parking.Id = 2; το δινει αυτόματα η βάση.
                                parking.Name = "Parking B";
                                parking.Location = "Chalandri, Bakogianni 10";
                                parking.TotalSpots = 20;
                                parking.ReservedSpots = 0;
                                parking.PricePerHour = 2.00m;

                            }

                            else if (o.Email == "owner3parking@gmail.com")

                            {

                                // parking.Id = 3; το δινει αυτόματα η βάση.
                                parking.Name = "Parking C";
                                parking.Location = "Chalandri, Attikis 8";
                                parking.TotalSpots = 15;
                                parking.ReservedSpots = 0;
                                parking.PricePerHour = 1.50m;

                            }

                            parking.OwnerId = permanentuser.Id; 


                            // τώρα έχω φτιάξει πλήρως το καθε παρκιγκ και πρεπει ///να το βαλω στη ΒΔ/


                            

                            context.Add(parking);
                            await context.SaveChangesAsync(); 

                        }
                    }

                    
                }


            }







            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=RegisterLogin}/{action=Index}/{id?}"); //πρώτη σελίδα browser

            app.Run();


        }
    }
}
