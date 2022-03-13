using DruivendoosAPI.Data;
using DruivendoosAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DruivendoosAPI.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly ApplicationDbContext context;

        public OrderServices(ApplicationDbContext context)
        {
            this.context = context;
        }

        //Adds a new order
        public void AddOrder(Order order)
        {
            context.Orders.Add(order);
            context.SaveChanges();
        }

        //Gets order with given MolliePaymentId
        public Task<Order> GetOrderByMolliePaymentId(string id)
        {
            return context.Orders.Include(o => o.Subscription).Include(o => o.Invoice).SingleOrDefaultAsync(o => o.MolliePaymentId == id);
        }

        //Gets the latest order from a customer with given Id
        public Task<Order> GetLatestOrderFromCustomer(int customerId)
        {
            return context.Orders.LastOrDefaultAsync(o => o.CustomerId == customerId);
        }
    }
}
