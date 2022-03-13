using DruivendoosAPI.Models;
using System;
using System.Threading.Tasks;

namespace DruivendoosAPI.Data
{
    public class DataInitializer
    {
        private readonly ApplicationDbContext _context;
        //private readonly UserManager<IdentityUser> _userManager;

        public DataInitializer(ApplicationDbContext context/*, UserManager<IdentityUser> userManager*/)
        {
            _context = context;
            //_userManager = userManager;

        }

        public async Task InitializeData()
        {


            Console.WriteLine("Deleting db...");
            await _context.Database.EnsureDeletedAsync();
            Console.WriteLine("Creating db....");
            if (await _context.Database.EnsureCreatedAsync())
            {


                //Klant
                var sven = new Customer { FirstName = "Sven", LastName = "Pepermans", Email = "sven@gmail.com", Street = "Smeerebbestraat", HouseNumber = "35", PostalCode = "9506", City = "Geraardsbergen", TelephoneNumber = "0479128068" };
                await _context.Customers.AddAsync(sven);


                var glenn = new Customer { FirstName = "Glenn", LastName = "Beeckman", Email = "glenn.beeckman31@gmail.com", Street = "Brusselbaan", HouseNumber = "141", PostalCode = "9320", City = "Erembodegem", TelephoneNumber = "0486656028" };
                await _context.Customers.AddAsync(glenn);

                var jos = new Customer { FirstName = "Jos", LastName = "Smith", Email = "Jos.smith@gmail.com", Street = "Kerkstraat", HouseNumber = "50", PostalCode = "9320", City = "Erembodegem", TelephoneNumber = "053211789" };
                await _context.Customers.AddAsync(jos);




                await _context.Customers.AddAsync(glenn);
                //TODO Data toevoegen
                //Leveranciers
                var lev = new Supplier() { Email = "lev@mail.com", Name = "leverancier" };
                await _context.Suppliers.AddAsync(lev);
                //Wijnen
                /*   var cheap1 = new Wine() { Story = "een beschr", GrapeVariety = "spaans", Year = "2018", GrapeColor = "rood", GrapeCountry = "Benidorm",GrapeDomain="Benidorm Castle", Image="image", Thumbnail = "thumb", Supplier= lev };
                   var cheap2 = new Wine() { Story = "een beschr", GrapeVariety = "spaans", Year = "2018", GrapeColor = "wit", GrapeCountry = "Benidorm", GrapeDomain = "Benidorm Castle", Image = "image", Thumbnail = "thumb", Supplier = lev};
                   var cheap3 = new Wine() { Story = "een beschr", GrapeVariety = "spaans", Year = "2018", GrapeColor = "rosé", GrapeCountry = "Benidorm", GrapeDomain = "Benidorm Castle", Image = "image", Thumbnail = "thumb", Supplier = lev };
                  await _context.Wines.AddRangeAsync(cheap1, cheap2, cheap3);
                   await _context.SaveChangesAsync();
                   List<Wine> cheapWijnen = new List<Wine>() { cheap1, cheap2, cheap3 };

                   //Order


                   //Dozen

                   var doos = new Box(sven) { Type = Models.Type.Cheap};
                   var cheap1WB = new WineBox(doos, cheap1);
                   var cheap2WB = new WineBox(doos, cheap2);
                   var cheap3WB = new WineBox(doos, cheap3);              
                   doos.Wines.Add(cheap1WB);
                   doos.Wines.Add(cheap2WB);
                   doos.Wines.Add(cheap3WB);
                   await _context.WinesBoxes.AddRangeAsync(cheap1WB, cheap2WB, cheap3WB);
                   await _context.SaveChangesAsync();
                  */

                // var subscription = new Subscription() { CustomerId = 1, Type = Models.Type.Cheap, Length = Length.Month };
                // await _context.Subscriptions.AddAsync(subscription);

                /* var subscriptionGlenn = new Subscription() { CustomerId = 3, Type = Models.Type.Rich, Length = Length.Month };
                 await _context.Subscriptions.AddAsync(subscriptionGlenn);

                 var subscriptionJos = new Subscription() { CustomerId = 2, Type = Models.Type.Cheap, Length = Length.Month };
                 await _context.Subscriptions.AddAsync(subscriptionJos);*/

                /*
                                sven.Wines.Add(new CustomerWine(sven, cheap1));
                                sven.Wines.Add(new CustomerWine(sven, cheap2));
                                sven.Wines.Add(new CustomerWine(sven, cheap3));
                */
                await _context.SaveChangesAsync();
            }

        }

    }
}
