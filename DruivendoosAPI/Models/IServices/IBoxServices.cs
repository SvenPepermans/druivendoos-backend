using DruivendoosAPI.DTOs;
using DruivendoosAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DruivendoosAPI.Services
{
    public interface IBoxServices
    {
        Task<BoxOfTheMonth> AddBoxOfTheMonth(BoxDTOs.NewBox box);
        Task<BoxDTOs.BoxDetail> BoxDetail(int id);
        Task<IEnumerable<BoxDTOs.BoxFromCustomerDetail>> GetBoxesFromCustomer(int customerId);
        Task<IEnumerable<BoxDTOs.BoxFromType>> GetBoxesFromType(Type type);
        Task<IEnumerable<BoxDTOs.BoxSentStatus>> GetBoxesWithSentStatus();
        Task ChangeSentStatus(int id);
        Task<BoxOfTheMonth> GetBoxOfTheMonth(int id);
        Task<BoxOfTheMonthDTO> GetCurrentBoxOfTheMonth();
        Task<Box> AddBox(Customer customer, BoxOfTheMonth box);
    }
}