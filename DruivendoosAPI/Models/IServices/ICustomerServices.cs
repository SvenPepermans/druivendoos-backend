using DruivendoosAPI.DTOs;
using DruivendoosAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DruivendoosAPI.Services
{
    public interface ICustomerServices
    {
        Task<Customer> GetByEmail(string email);
        Task<Customer> GetById(int id);
        Task<IEnumerable<CustomerDTOs.GetCustomer>> GetAllCustomers();
        Task EditCustomer(CustomerDTOs.CustomerDetail customerUpdate, Customer customer);
        Task DeleteCustomer(Customer customer);
        void AddCustomerInAccountController(Customer customer);
        Customer AddCustomer(CustomerDTOs.NewCustomer customer);
        Task<IEnumerable<CustomerDTOs.CustomerWithValidSubscription>> GetCustomersWithValidSubscription();
    }
}