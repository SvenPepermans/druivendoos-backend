using System;
using System.Collections.Generic;

namespace DruivendoosAPI.Models
{
    public class Box
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string PostalCode { get; set; }
        public Type Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<WineBox> Wines { get; set; }
        public Boolean IsSent { get; set; }

        public Box()
        {
            Wines = new List<WineBox>();
            IsSent = false;
        }

        public Box(Customer customer) : this()
        {
            CustomerId = customer.CustomerId;
            Street = customer.Street;
            HouseNumber = customer.HouseNumber;
            PostalCode = customer.PostalCode;
            City = customer.City;
            CreatedAt = DateTime.Now;
            IsSent = false;



        }
    }
}
