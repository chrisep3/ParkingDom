using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Parking.Models;



namespace Parking
{
    public class AppDbContext : IdentityDbContext<AppUser>   // Λέει στο Migration ποιοι πίνακες θα χτιστούν (Users, Roles κ.λπ.) και τι στήλες θα έχουν.

    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }  //// Εδώ λες: "Πάρε τις ρυθμίσεις (SqLite, σκληρός δίσκος) και δώσ' τις στον πατέρα μου (base). Λέμε bAse και όχι IdentityDbContext"


        public DbSet<ParkingLot> parkings { get; set; } // τα προσθετω εγω. Δεν τα χει ετοιμα το IdentityDbContext
        public DbSet<Reservation> reservations { get; set; }

        //public DbSet<AppUser> users { get; set; } θα χρειαζοταν αν κληρονομουσα μονο απ ο DbContext



        protected override void OnModelCreating(ModelBuilder modelBuilder) //γράφουμε καρφωτά μερικά έτοιμα πάρκινγκ και κρατήσεις, ώστε η βάση να "γεννηθεί" γεμάτη.
        {
            base.OnModelCreating(modelBuilder);
/*
            // Αρχικές Ρυθμίσεις για τον πίνακα ParkingLot 
            modelBuilder.Entity<ParkingLot>(entity =>
            {


                // Εδώ γίνεται το Seed Data
                entity.HasData(
                    new ParkingLot { Id = 1, Name = "Parking A", Location = "Chalandri, Grammou 12", TotalSpots = 10, ReservedSpots = 0, PricePerHour = 2.50m },
                    new ParkingLot { Id = 2, Name = "Parking B", Location = "Chalandri, Bakogianni 10", TotalSpots = 20, ReservedSpots = 0, PricePerHour = 2.00m },
                    new ParkingLot { Id = 3, Name = "Parking C", Location = "Chalandri, Attikis 8", TotalSpots = 15, ReservedSpots = 0, PricePerHour = 1.50m }
            ); // ΔΕΝ ΕΧΕΙ OwnerId, οπότε για να μη σκάει θέλει ερωτηματικό στο πεδίο OwnerId, του ParkingLot

            });*/


            

        }    

    

    }

}

