using DruivendoosAPI.Models;

namespace DruivendoosAPI.DTOs
{
    public class CustomerDTOs
    {
        public class GetCustomer
        {
            public string FirstName { get; set; }

            public string LastName { get; set; }

            public string Email { get; set; }

            public GetCustomer() { }

            public GetCustomer(Customer customer) : this()
            {
                FirstName = customer.FirstName;
                LastName = customer.LastName;
                Email = customer.Email;

            }
        }
        public class NewCustomer
        {
            public string FirstName { get; set; }

            public string LastName { get; set; }

            public string Email { get; set; }
            public string TelephoneNumber { get; set; }
            public string City { get; set; }
            public string Street { get; set; }
            public string HouseNumber { get; set; }
            public string PostalCode { get; set; }

            public NewCustomer() { }
        }
        public class CustomerDetail
        {
            public int CustomerId { get; set; }
            public string FirstName { get; set; }

            public string LastName { get; set; }

            public string Email { get; set; }
            public string TelephoneNumber { get; set; }
            public string City { get; set; }
            public string Street { get; set; }
            public string HouseNumber { get; set; }
            public string PostalCode { get; set; }

            public CustomerDetail() { }
            public CustomerDetail(Customer customer)
            {
                CustomerId = customer.CustomerId;
                FirstName = customer.FirstName;
                LastName = customer.LastName;
                Email = customer.Email;
                TelephoneNumber = customer.TelephoneNumber;
                City = customer.City;
                Street = customer.Street;
                HouseNumber = customer.HouseNumber;
                PostalCode = customer.PostalCode;
            }
        }

        public class CustomerWithValidSubscription
        {
            public string Email { get; set; }
            public Models.Type Type { get; set; }

            public CustomerWithValidSubscription(Customer customer)
            {
                Email = customer.Email;
            }
        }
    }
}
