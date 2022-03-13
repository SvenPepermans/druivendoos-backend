using DruivendoosAPI.Data;
using DruivendoosAPI.DTOs;
using DruivendoosAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DruivendoosAPI.Services
{
    public class CustomerServices : ICustomerServices
    {
        private readonly ApplicationDbContext context;

        public CustomerServices(ApplicationDbContext context)
        {
            this.context = context;
        }

        //Deletes a customer
        public Task DeleteCustomer(Customer customer)
        {
            context.Customers.Remove(customer);
            return context.SaveChangesAsync();
        }

        //Gets a customer with given email
        public Task<Customer> GetByEmail(string email)
        {
            return context.Customers.SingleOrDefaultAsync(c => c.Email.Equals(email));
        }

        //Gets all the customers from db
        public async Task<IEnumerable<CustomerDTOs.GetCustomer>> GetAllCustomers()
        {
            var customerdtos = new List<CustomerDTOs.GetCustomer>();
            var customers = await context.Customers.ToListAsync();
            foreach (var customer in customers)
            {
                customerdtos.Add(new CustomerDTOs.GetCustomer(customer));
            }
            return customerdtos;
        }

        //Edits a customers details
        public Task EditCustomer(CustomerDTOs.CustomerDetail customerUpdate, Customer customer)
        {

            customer.FirstName = customerUpdate.FirstName;
            customer.LastName = customerUpdate.LastName;
            customer.Email = customerUpdate.Email;
            customer.TelephoneNumber = customerUpdate.TelephoneNumber;
            customer.Street = customerUpdate.Street;
            customer.HouseNumber = customerUpdate.HouseNumber;
            customer.PostalCode = customerUpdate.PostalCode;
            customer.City = customerUpdate.City;

            context.Customers.Update(customer);
            return context.SaveChangesAsync();

        }

        //Gets customer with given id
        public Task<Customer> GetById(int id)
        {
            return context.Customers.Include(c => c.Boxes).SingleOrDefaultAsync(c => c.CustomerId.Equals(id));
        }

        //Adds a customer if it is a new customer during a payment
        public void AddCustomerInAccountController(Customer customer)
        {
            context.Customers.Add(customer);
            context.SaveChanges();
        }

        //Adds a new customer
        public Customer AddCustomer(CustomerDTOs.NewCustomer customer)
        {
            Customer customerToCreate = new Customer()
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                TelephoneNumber = customer.TelephoneNumber,
                Street = customer.Street,
                HouseNumber = customer.HouseNumber,
                City = customer.City,
                PostalCode = customer.PostalCode

            };
            context.Customers.Add(customerToCreate);
            context.SaveChanges();
            return customerToCreate;
        }

        //Gets all customers with a valid subscription
        public async Task<IEnumerable<CustomerDTOs.CustomerWithValidSubscription>> GetCustomersWithValidSubscription()
        {
            var customers = new List<CustomerDTOs.CustomerWithValidSubscription>();
            var validSubcriptions = await context.Subscriptions.Where(s => s.EndDate >= DateTime.Today && s.StartDate <= DateTime.Today && s.IsActive == true).ToListAsync();
            foreach (var sub in validSubcriptions)
            {
               
                customers.Add(new CustomerDTOs.CustomerWithValidSubscription(await GetById(sub.CustomerId)) { Type = sub.Type });
               
            }
            return customers;
        }


    }
}
