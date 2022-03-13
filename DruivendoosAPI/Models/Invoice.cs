using System;

namespace DruivendoosAPI.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CompanyName { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string VATNumber { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public Invoice() { }
    }
}
