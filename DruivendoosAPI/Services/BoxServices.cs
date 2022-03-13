using DruivendoosAPI.Data;
using DruivendoosAPI.DTOs;
using DruivendoosAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DruivendoosAPI.Services
{
    public class BoxServices : IBoxServices
    {
        private readonly ApplicationDbContext context;
        private readonly IWineServices _wineServices;
    

        public BoxServices(ApplicationDbContext context, IWineServices wineServices)
        {
            this.context = context;
            this._wineServices = wineServices;
            
        }

        //Gets a box and its details.
        public async Task<BoxDTOs.BoxDetail> BoxDetail(int id)
        {
            return new BoxDTOs.BoxDetail(await context.Boxes.Include(b => b.Wines).ThenInclude(w => w.Wine).SingleOrDefaultAsync(b => b.Id.Equals(id)));

        }

        //Gets a given customer all his or her boxes
        public async Task<IEnumerable<BoxDTOs.BoxFromCustomerDetail>> GetBoxesFromCustomer(int customerId)
        {
            var returnBoxes = new List<BoxDTOs.BoxFromCustomerDetail>();
            var boxes = await context.Boxes.Include(b => b.Wines).ThenInclude(wb => wb.Wine).Where(b => b.CustomerId.Equals(customerId)).ToListAsync();
            foreach (Box box in boxes)
            {
                returnBoxes.Add(new BoxDTOs.BoxFromCustomerDetail(box));
            }
            return returnBoxes;

        }

        //Gets BoxOfTheMonth with given id
        public Task<BoxOfTheMonth> GetBoxOfTheMonth(int id)
        {
            return context.BoxesOfTheMonth.Include(b => b.Wines).SingleOrDefaultAsync(b => b.BoxOfTheMonthId == id);
        }

        //Gets the BoxOfTheMonth from the current month
        public async Task<BoxOfTheMonthDTO> GetCurrentBoxOfTheMonth()
        {
            var box = await context.BoxesOfTheMonth.Include(b => b.Wines).ThenInclude(w => w.Reviews).Include(b => b.Wines).ThenInclude(w => w.Image).OrderByDescending(b => b.CreatedAt).FirstAsync();
            var returnBox = new BoxOfTheMonthDTO(box);
            return returnBox;
        }

        //Adds a new box
        public async Task<Box> AddBox(Customer customer, BoxOfTheMonth box)
        {
            Box boxToCreate = new Box(customer)
            {
                Type = 0
            };
            foreach (Wine wine in box.Wines)
            {
                //Creating wineboxes and adding them to the box                
                var winebox = new WineBox(boxToCreate, wine);
                boxToCreate.Wines.Add(winebox);
                await context.WinesBoxes.AddAsync(winebox);

                //Creating Customerwines
                var customerWineDB = await context.CustomersWines.SingleOrDefaultAsync(cw => cw.CustomerId == customer.CustomerId && cw.WineId == wine.WineId);
                if (customerWineDB == null)
                {
                    var customerWine = new CustomerWine(customer, wine);
                    await context.CustomersWines.AddAsync(customerWine);
                }              
            }
            await context.Boxes.AddAsync(boxToCreate);
            await context.SaveChangesAsync();
            return boxToCreate;
        }

        //Adds new BoxOfTheMonth
        public async Task<BoxOfTheMonth> AddBoxOfTheMonth(BoxDTOs.NewBox box)
        {
            var wines = new List<Wine>();
            foreach (int id in box.WineIds)
            {
                wines.Add(await _wineServices.GetWineById(id));
            }
            var boxofthemonth = new BoxOfTheMonth(wines);
            await context.BoxesOfTheMonth.AddAsync(boxofthemonth);
            await context.SaveChangesAsync();
            return boxofthemonth;
        }

        //Gets all boxes from a certain type 
        public async Task<IEnumerable<BoxDTOs.BoxFromType>> GetBoxesFromType(Models.Type type)
        {
            var returnBoxes = new List<BoxDTOs.BoxFromType>();
            var boxes = await context.Boxes.Include(b => b.Wines).ThenInclude(wb => wb.Wine).Where(b => b.Type.Equals(type)).ToListAsync();
            foreach (Box box in boxes)
            {
                var customer = await context.Customers.SingleOrDefaultAsync(c => c.CustomerId.Equals(box.CustomerId));
                returnBoxes.Add(new BoxDTOs.BoxFromType(box) { FirstName = customer.FirstName, LastName = customer.LastName });
            }
            return returnBoxes;
        }

        //Gets all the boxes with their sent status
        public async Task<IEnumerable<BoxDTOs.BoxSentStatus>> GetBoxesWithSentStatus()
        {
            var returnBoxes = new List<BoxDTOs.BoxSentStatus>();
            var boxes = await context.Boxes.Include(b => b.Wines).ThenInclude(w => w.Wine).ThenInclude(w => w.Supplier).ToListAsync();
            foreach (Box box in boxes)
            {
                var customer = await context.Customers.SingleOrDefaultAsync(c => c.CustomerId.Equals(box.CustomerId));
                returnBoxes.Add(new BoxDTOs.BoxSentStatus(box) { FirstName = customer.FirstName, LastName = customer.LastName });
            }
            return returnBoxes;
        }

        //Changes sent status of a box with given id
        public async Task ChangeSentStatus(int id)
        {
            var box = await context.Boxes.SingleOrDefaultAsync(b => b.Id == id);
            box.IsSent = !box.IsSent;
            context.Boxes.Update(box);
            await context.SaveChangesAsync();
        }

    }
}