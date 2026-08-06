ParkDom — ASP.NET Core MVC Parking Reservation System

Το ParkDom είναι μια μικρή web εφαρμογή για κρατήσεις ιδιωτικών parking, υλοποιημένη με ASP.NET Core MVC, Identity, EF Core και SQLite.
Οι πολίτες μπορούν να δημιουργήσουν λογαριασμό, να δουν διαθέσιμα parking και να κάνουν κράτηση.
Οι ιδιοκτήτες parking (Owners) έχουν ξεχωριστό ρόλο και μπορούν να διαχειρίζονται τις θέσεις του δικού τους parking.

Τι υλοποίησα
Authentication & Authorization με ASP.NET Core Identity (User / Owner roles)

Δημιουργία, προβολή και διαγραφή κρατήσεων

Validation για ημερομηνίες, διαθέσιμες θέσεις και overlapping reservations

Σχέσεις βάσης με foreign keys (User–Reservation, Parking–Reservation, Owner–Parking)

Razor Views με Bootstrap

LINQ queries & async/await

Role‑based access control

κάθε χρήστης βλέπει μόνο τις δικές του κρατήσεις

κάθε owner διαχειρίζεται μόνο το δικό του parking

Πώς συνδέεται ο χρήστης
Η εφαρμογή χρησιμοποιεί ASP.NET Core Identity για εγγραφή και σύνδεση.
Ο πολίτης δημιουργεί λογαριασμό μέσω Register και συνδέεται μέσω Login.
Οι ιδιοκτήτες parking συνδέονται με δικό τους Owner λογαριασμό και έχουν πρόσβαση σε σελίδες διαχείρισης του parking τους.

Τεχνολογίες
C#, .NET 8, ASP.NET Core MVC, ASP.NET Core Identity, Entity Framework Core, SQLite, Razor Views, Bootstrap

Εκτέλεση
Ανοίξτε το Parking.sln στο Visual Studio 2022

Κάντε restore τα NuGet packages

Από το Package Manager Console εκτελέστε:

Code
Update-Database
Τρέξτε την εφαρμογή
Η SQLite βάση δημιουργείται τοπικά.

Demo Owner (τοπική χρήση)
Email: owner1parking@gmail.com
Password: Password123!

Σχετικά
Το project δημιουργήθηκε στο πλαίσιο της εκμάθησης μου στο ASP.NET Core και αποτελεί μέρος του προσωπικού μου portfolio.
Μέσα από την ανάπτυξή του ασχολήθηκα με MVC, Identity, ρόλους χρηστών, EF Core, validation, authorization και βασικές σχέσεις βάσης δεδομένων.