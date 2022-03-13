using DruivendoosAPI.Data;
using DruivendoosAPI.DTOs;
using DruivendoosAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DruivendoosAPI.Services
{
    public class WineServices : IWineServices
    {
        private readonly ApplicationDbContext context;
        private readonly IConfiguration configuration;

        public WineServices(ApplicationDbContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        //Adds a new wine
        public Wine AddWine(WineDTOs.NewWine wine)
        {
            Supplier supplierToAdd = context.Suppliers.Where(sup => sup.Email == wine.SupplierEmail).SingleOrDefault();
            if (supplierToAdd == null)
            {
                supplierToAdd = new Supplier(wine.SupplierName, wine.SupplierEmail);
                context.Suppliers.Add(supplierToAdd);
                context.SaveChanges();
            }
            var accesKey = configuration.GetConnectionString("AccessKey");
            CloudStorageAccount.TryParse(accesKey, out CloudStorageAccount storageAccount);

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("images");

            var blob = container.GetBlockBlobReference(wine.Image.Name);
            var blobUrl = blob.Uri.AbsoluteUri;


            Wine wineToCreate = new Wine()
            {
                Story = wine.Story,
                GrapeColor = wine.GrapeColor,
                GrapeVariety = wine.GrapeVariety,
                GrapeCountry = wine.GrapeCountry,
                GrapeDomain = wine.GrapeDomain,
                WineName = wine.WineName,
                Image = new Picture()
                {
                    Url = blobUrl,
                    Name = wine.Image.Name
                },
                Year = wine.Year,
                Supplier = supplierToAdd
            };
            context.Wines.Add(wineToCreate);
            context.SaveChanges();

            return wineToCreate;
        }

        //Gets the details of a wine with given id
        public async Task<WineDTOs.WineDetail> WineDetail(int id)
        {
            var wine = await context.Wines.Include(w => w.Reviews).Include(w => w.Supplier).SingleOrDefaultAsync(w => w.WineId == id);
            if (wine == null)
            {
                return null;
            }
            return new WineDTOs.WineDetail(wine);

        }

        //Get a wine with given id
        public Task<Wine> GetWineById(int id)
        {
            return context.Wines.Include(w => w.Reviews).Include(w => w.Supplier).SingleOrDefaultAsync(w => w.WineId == id);
        }

        //Gets all the wines
        public async Task<IEnumerable<WineDTOs.WineDetail>> AllWines()
        {
            var returnWines = new List<WineDTOs.WineDetail>();
            var wines = await context.Wines.Include(w => w.Reviews).Include(w => w.Image).Include(w => w.Supplier).ToListAsync();
            foreach (var wine in wines)
            {
                returnWines.Add(new WineDTOs.WineDetail(wine));
            }
            return returnWines;

        }

        //Gets all the wines from customer with given email
        public async Task<IEnumerable<WineDTOs.WineDetailWithCustomerId>> WinesFromCostumer(string email)
        {
            Customer customer = await context.Customers.Include(c => c.Wines).ThenInclude(w => w.Wine).ThenInclude(w => w.Reviews).SingleOrDefaultAsync(c => c.Email.Equals(email));
            var wines = customer.Wines;
            var customerId = customer.CustomerId;
            var returnWines = new List<WineDTOs.WineDetailWithCustomerId>();
            
            foreach (var winebox in wines)
            {
                var reviewList = new List<ReviewDTOs.ReviewFromWine>();
                foreach (var review in winebox.Wine.Reviews)
                {
                    var reviewer = await context.Customers.SingleOrDefaultAsync(c => c.CustomerId == review.CustomerId);
                    var reviewerName = reviewer.FirstName + " " + reviewer.LastName;
                    var newReview = new ReviewDTOs.ReviewFromWine(review) { CustomerName = reviewerName };
                    reviewList.Add(newReview);
                }
                returnWines.Add(new WineDTOs.WineDetailWithCustomerId(winebox.Wine, reviewList) { CustomerId = customerId });
            }
            return returnWines;
        }


        //Deletes a wine
        public Task DeleteWine(Wine wine)
        {
            context.Wines.Remove(wine);
            return context.SaveChangesAsync();
        }
    }
}
