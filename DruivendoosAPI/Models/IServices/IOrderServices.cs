using DruivendoosAPI.Models;
using System.Threading.Tasks;

namespace DruivendoosAPI.Services
{
    public interface IOrderServices
    {
        void AddOrder(Order order);
        Task<Order> GetOrderByMolliePaymentId(string id);
        Task<Order> GetLatestOrderFromCustomer(int customerId);
    }
}