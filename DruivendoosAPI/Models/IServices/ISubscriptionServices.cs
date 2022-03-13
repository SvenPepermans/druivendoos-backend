using DruivendoosAPI.Models;
using System.Threading.Tasks;

namespace DruivendoosAPI.Services
{
    public interface ISubscriptionServices
    {
        void AddSubscription(Subscription subscription);
        Task<Subscription> GetActiveSubscription(int customerId);
        Task<Subscription> GetById(int id);
        void Remove(Subscription subscription);
    }
}