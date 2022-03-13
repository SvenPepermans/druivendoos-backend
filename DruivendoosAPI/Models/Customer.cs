using System.Collections.Generic;

namespace DruivendoosAPI.Models
{
    public class Customer
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
        public ICollection<CustomerWine> Wines { get; set; }
        public ICollection<Box> Boxes { get; set; }

        public Customer()
        {
            Wines = new List<CustomerWine>();
            Boxes = new List<Box>();
        }


    }
}
