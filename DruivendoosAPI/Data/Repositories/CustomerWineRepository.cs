using DruivendoosAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DruivendoosAPI.Data.Repositories
{
    public class CustomerWineRepository : ICustomerWineRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<CustomerWine> _klantWijnen;

        public CustomerWineRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
            this._klantWijnen = dbContext.CustomersWines;
        }

        public void Add(CustomerWine klantWijn)
        {
            _klantWijnen.Add(klantWijn);
        }

        public void Delete(CustomerWine klantWijn)
        {
            _klantWijnen.Remove(klantWijn);
        }

        public IEnumerable<CustomerWine> GetAll()
        {
            //dacht hier ook include te doen mr bij istuti hebben we dat niet gedaan
            return _klantWijnen.ToList();
        }

        public CustomerWine GetBy(int klantId, int wijnId)
        {
            return _klantWijnen.Where(kw => kw.CustomerId == klantId && kw.WineId == wijnId).SingleOrDefault();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
