using DruivendoosAPI.Data;
using DruivendoosAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DruivendoosAPI.Services
{
    public class SubscriptionServices : ISubscriptionServices
    {
        private readonly ApplicationDbContext context;

        public SubscriptionServices(ApplicationDbContext context)
        {
            this.context = context;
        }

        //Gets the active subscription from customer with given id
        public Task<Subscription> GetActiveSubscription(int customerId)
        {
            return context.Subscriptions.SingleOrDefaultAsync(s => s.CustomerId == customerId && s.IsActive == true);
        }

        //Adds a new subscription
        public void AddSubscription(Subscription subscription)
        {
            context.Subscriptions.Add(subscription);
            context.SaveChanges();

        }

        //Gets a subscription by id
        public Task<Subscription> GetById(int id)
        {
            return context.Subscriptions.SingleOrDefaultAsync(s => s.Id == id);
        }

        //Deletes a subscription
        public void Remove(Subscription subscription)
        {
            context.Subscriptions.Remove(subscription);
            context.SaveChanges();
        }
    }
}
