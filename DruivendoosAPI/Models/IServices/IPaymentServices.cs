using DruivendoosAPI.Models;
using System.Threading.Tasks;

namespace DruivendoosAPI.Services
{
    public interface IPaymentServices
    {
        void AddPayment(Payment payment);
        Task<Payment> GetLatestPaymentFromOrder(Order order);
    }
}