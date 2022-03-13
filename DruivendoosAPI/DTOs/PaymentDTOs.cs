using System;

namespace DruivendoosAPI.DTOs
{
    public class PaymentDTOs
    {
        public class RegularPayment
        {
            public decimal Amount { get; set; }
            public string Description { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public string Street { get; set; }
            public string HouseNumber { get; set; }
            public string City { get; set; }
            public string PostalCode { get; set; }
            public Models.Length SubscriptionType { get; set; }
            public Models.Type BoxType { get; set; }
            public Boolean HasInvoice { get; set; }
            public string CompanyName { get; set; }
            public string StreetCompany { get; set; }
            public string HouseNumberCompany { get; set; }
            public string CityCompany { get; set; }
            public string PostalCodeCompany { get; set; }
            public string VATNumber { get; set; }


            public RegularPayment()
            {

            }

            public RegularPayment(decimal amount, string description, string firstname, string lastname, string email, string phonenumber, string street, string housenumber, string city, string postalcode, Models.Length length, Models.Type type)
            {
                Amount = amount;
                Description = description;
                FirstName = firstname;
                LastName = lastname;
                Email = email;
                PhoneNumber = phonenumber;
                Street = street;
                HouseNumber = housenumber;
                City = city;
                PostalCode = postalcode;
                SubscriptionType = length;
                BoxType = type;


            }
        }
        public class GiftPayment
        {
            public decimal Amount { get; set; }
            public string Description { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string EmailSender { get; set; }
            public string EmailReceiver { get; set; }
            public string PhoneNumber { get; set; }
            public string Street { get; set; }
            public string HouseNumber { get; set; }
            public string City { get; set; }
            public string PostalCode { get; set; }
            public Models.Length SubscriptionType { get; set; }
            public Models.Type BoxType { get; set; }
            public string Message { get; set; }
            public Boolean HasInvoice { get; set; }
            public string CompanyName { get; set; }
            public string StreetCompany { get; set; }
            public string HouseNumberCompany { get; set; }
            public string CityCompany { get; set; }
            public string PostalCodeCompany { get; set; }
            public string VATNumber { get; set; }


            public GiftPayment()
            {

            }

            public GiftPayment(decimal amount, string description, string firstname, string lastname, string emailSender, string emailReceiver, string phonenumber, string street, string housenumber, string city, string postalcode, Models.Length length, Models.Type type, string message)
            {
                Amount = amount;
                Description = description;
                FirstName = firstname;
                LastName = lastname;
                EmailSender = emailSender;
                EmailReceiver = emailReceiver;
                PhoneNumber = phonenumber;
                Street = street;
                HouseNumber = housenumber;
                City = city;
                PostalCode = postalcode;
                SubscriptionType = length;
                BoxType = type;
                Message = message;

            }
        }

    }
}
