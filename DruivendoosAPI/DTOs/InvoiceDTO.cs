using DruivendoosAPI.Models;

namespace DruivendoosAPI.DTOs
{
    public class InvoiceDTO
    {
        public int InvoiceId { get; set; }
        public int CustomerId { get; set; }
        public string CompanyName { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string VATNumber { get; set; }
        public decimal Amount { get; set; }

        public int OrderId { get; set; }

        public InvoiceDTO(Invoice invoice)
        {
            InvoiceId = invoice.Id;
            CustomerId = invoice.CustomerId;
            CompanyName = invoice.CompanyName;
            Street = invoice.Street;
            HouseNumber = invoice.HouseNumber;
            PostalCode = invoice.PostalCode;
            City = invoice.City;
            VATNumber = invoice.VATNumber;
            OrderId = invoice.OrderId;
            Amount = invoice.Amount;
        }
    }
}
