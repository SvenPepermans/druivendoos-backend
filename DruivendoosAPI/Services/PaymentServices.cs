using DruivendoosAPI.Data;
using DruivendoosAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DruivendoosAPI.Services
{
    public class PaymentServices : IPaymentServices
    {
        private readonly ApplicationDbContext context;

        public PaymentServices(ApplicationDbContext context)
        {
            this.context = context;
        }

        //Adds a new payment
        public void AddPayment(Payment payment)
        {
            context.Payments.Add(payment);
            context.SaveChanges();
        }

        //Gets the latest payment from a given order
        public Task<Payment> GetLatestPaymentFromOrder(Order order)
        {
            return context.Payments.Where(p => p.Order == order).OrderByDescending(o => o.CreatedAt).FirstAsync();
        }
    }
}
