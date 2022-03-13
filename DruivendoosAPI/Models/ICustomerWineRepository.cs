using DruivendoosAPI.Models;
using System.Collections.Generic;

namespace DruivendoosAPI.Data.Repositories
{
    public interface ICustomerWineRepository
    {
        IEnumerable<CustomerWine> GetAll();
        CustomerWine GetBy(int klantId, int wijnId);
        void Add(CustomerWine klantWijn);
        void Delete(CustomerWine klantWijn);
        void SaveChanges();
    }
}
