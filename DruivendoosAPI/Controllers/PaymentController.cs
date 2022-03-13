using DruivendoosAPI.DTOs;
using DruivendoosAPI.Models;
using DruivendoosAPI.Services;
using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Mollie.Api.Client;
using Mollie.Api.Models;
using Mollie.Api.Models.Payment;
using Mollie.Api.Models.Payment.Request;
using Mollie.Api.Models.Payment.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DruivendoosAPI.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("api/[controller]")]
    [ApiController]
    
    public class PaymentController : ControllerBase
    {
        private readonly ICustomerServices customerServices;
        private readonly IOrderServices orderServices;
        private readonly ISubscriptionServices subscriptionServices;
        private readonly IPaymentServices paymentServices;
        private readonly IInvoiceServices invoiceServices;
        private readonly IBoxServices boxServices;

        public PaymentController(ICustomerServices customerServices, IOrderServices orderServices, ISubscriptionServices subscriptionServices, IPaymentServices paymentServices, IInvoiceServices invoiceServices, IBoxServices boxServices)
        {
            this.customerServices = customerServices;
            this.orderServices = orderServices;
            this.subscriptionServices = subscriptionServices;
            this.paymentServices = paymentServices;
            this.invoiceServices = invoiceServices;
            this.boxServices = boxServices;
        }

       
        [HttpPost("Pay")]
        public async Task<string> PayAsync(PaymentDTOs.RegularPayment paymentDTO)
        {
            PaymentClient paymentClient = new PaymentClient("test_kRTwa374N5AjEVmJQAfueqaCRx9rQf");
            PaymentRequest paymentRequest = new PaymentRequest()
            {
                Amount = new Amount(Currency.EUR, paymentDTO.Amount),
                Description = paymentDTO.Description,
                RedirectUrl = "https://druivendoos-5fc7c.web.app/paymentRedirect",
                WebhookUrl = "https://druivendoosbackend.azurewebsites.net/api/Payment/PaymentWebhook",
                Methods = new List<string>()
                {
                    PaymentMethod.Bancontact,
                    PaymentMethod.CreditCard,
                    PaymentMethod.Kbc,
                    PaymentMethod.Belfius
                }
            };

            PaymentResponse result = await paymentClient.CreatePaymentAsync(paymentRequest);


            //Checks if there is already a customer with this email, if not it will create a new one
            Customer customer = await customerServices.GetByEmail(paymentDTO.Email);
            if (customer == null)
            {
                customerServices.AddCustomerInAccountController(new Customer()
                {
                    FirstName = paymentDTO.FirstName,
                    LastName = paymentDTO.LastName,
                    Email = paymentDTO.Email,
                    Street = paymentDTO.Street,
                    HouseNumber = paymentDTO.HouseNumber,
                    City = paymentDTO.City,
                    PostalCode = paymentDTO.PostalCode,
                    TelephoneNumber = paymentDTO.PhoneNumber
                });

            }
            else
            {
                var cust = new CustomerDTOs.CustomerDetail()
                {

                    FirstName = paymentDTO.FirstName,
                    LastName = paymentDTO.LastName,
                    Email = paymentDTO.Email,
                    Street = paymentDTO.Street,
                    HouseNumber = paymentDTO.HouseNumber,
                    City = paymentDTO.City,
                    PostalCode = paymentDTO.PostalCode,
                    TelephoneNumber = paymentDTO.PhoneNumber
                };
                await customerServices.EditCustomer(cust, customer);

            }
            //Setting the new customer to a local variable so the id can be used to create the order
            Customer newCustomer = await customerServices.GetByEmail(paymentDTO.Email);
            //Create the order with the customerId and paymentDTO details.
            var customerId = customer == null ? newCustomer.CustomerId : customer.CustomerId;
            Order order = new Order()
            {
                Amount = paymentDTO.Amount,
                Description = result.Description,
                MollieOrderId = result.OrderId,
                MolliePaymentId = result.Id,
                CustomerId = customerId,
                Subscription = new Subscription()
                {
                    Type = paymentDTO.BoxType,
                    CustomerId = customerId,
                    Length = paymentDTO.SubscriptionType
                },
                Invoice = paymentDTO.HasInvoice ? new Invoice()
                {
                    CompanyName = paymentDTO.CompanyName,
                    CustomerId = customerId,
                    Street = paymentDTO.StreetCompany,
                    HouseNumber = paymentDTO.HouseNumberCompany,
                    City = paymentDTO.CityCompany,
                    PostalCode = paymentDTO.PostalCodeCompany,
                    VATNumber = paymentDTO.VATNumber,
                    Amount = paymentDTO.Amount
                } : null
            };


            orderServices.AddOrder(order);
            Payment payment = new Payment() { CreatedAt = result.CreatedAt, Status = result.Status, Order = order };
            paymentServices.AddPayment(payment);
            return result.Links.Checkout.Href;
        }


       
        [HttpPost("PayGift")]
        public async Task<string> PayGiftAsync(PaymentDTOs.GiftPayment paymentDTO)
        {
            PaymentClient paymentClient = new PaymentClient("test_kRTwa374N5AjEVmJQAfueqaCRx9rQf");
            PaymentRequest paymentRequest = new PaymentRequest()
            {
                Amount = new Amount(Currency.EUR, paymentDTO.Amount),
                Description = paymentDTO.Description,
                RedirectUrl = "https://druivendoos-5fc7c.web.app/paymentRedirect",
                WebhookUrl = "https://druivendoosbackend.azurewebsites.net/api/Payment/PaymentWebhook",
                Methods = new List<string>()
                {
                    PaymentMethod.Bancontact,
                    PaymentMethod.CreditCard,
                    PaymentMethod.Kbc,
                    PaymentMethod.Belfius
                }
            };
            PaymentResponse result = await paymentClient.CreatePaymentAsync(paymentRequest);

            //Checks if there is already a customer with this email, if not it will create a new one
            Customer customer = await customerServices.GetByEmail(paymentDTO.EmailReceiver);
            if (customer == null)
            {
                customerServices.AddCustomerInAccountController(new Customer()
                {
                    FirstName = paymentDTO.FirstName,
                    LastName = paymentDTO.LastName,
                    Email = paymentDTO.EmailReceiver,
                    Street = paymentDTO.Street,
                    HouseNumber = paymentDTO.HouseNumber,
                    City = paymentDTO.City,
                    PostalCode = paymentDTO.PostalCode,
                });
            }
            else
            {
                var cust = new CustomerDTOs.CustomerDetail()
                {

                    FirstName = paymentDTO.FirstName,
                    LastName = paymentDTO.LastName,
                    Email = paymentDTO.EmailReceiver,
                    Street = paymentDTO.Street,
                    HouseNumber = paymentDTO.HouseNumber,
                    City = paymentDTO.City,
                    PostalCode = paymentDTO.PostalCode,
                    TelephoneNumber = paymentDTO.PhoneNumber
                };
                await customerServices.EditCustomer(cust, customer);

            }
            //Setting the new customer to a local variable so the id can be used to create the order
            Customer newCustomer = await customerServices.GetByEmail(paymentDTO.EmailReceiver);
            //Create the order with the customerId and paymentDTO details.
            var customerId = customer == null ? newCustomer.CustomerId : customer.CustomerId;
            Order order = new Order()
            {
                Amount = paymentDTO.Amount,
                Description = result.Description,
                MollieOrderId = result.OrderId,
                MolliePaymentId = result.Id,
                CustomerId = customer.CustomerId,
                Subscription = new Subscription()
                {
                    Type = paymentDTO.BoxType,
                    CustomerId = customer.CustomerId,
                    Length = paymentDTO.SubscriptionType
                },
                Invoice = paymentDTO.HasInvoice ? new Invoice()
                {
                    CompanyName = paymentDTO.CompanyName,
                    CustomerId = customerId,
                    Street = paymentDTO.StreetCompany,
                    HouseNumber = paymentDTO.HouseNumberCompany,
                    City = paymentDTO.CityCompany,
                    PostalCode = paymentDTO.PostalCodeCompany,
                    VATNumber = paymentDTO.VATNumber,
                    Amount = paymentDTO.Amount,
                    Date = DateTime.Today
                } : null
            };
            orderServices.AddOrder(order);
            Payment payment = new Payment() { CreatedAt = result.CreatedAt, Status = result.Status, Order = order };
            paymentServices.AddPayment(payment);

            return result.Links.Checkout.Href;
        }

        
        [Consumes("application/x-www-form-urlencoded")]
        [HttpPost("PaymentWebhook")]
        public async Task<IActionResult> PaymentWebHook()
        {

            string paramId = HttpContext.Request.Form["Id"];

            PaymentClient paymentClient = new PaymentClient("test_kRTwa374N5AjEVmJQAfueqaCRx9rQf");

            PaymentResponse result = await paymentClient.GetPaymentAsync(paramId);
            Order order = await orderServices.GetOrderByMolliePaymentId(paramId);
            await CreatePayment(order, result);
            return StatusCode(200);











        }
        //a new payment gets created every time the status changes, if the staus == paid then the subscription we made earlier will be added to the subscription
        private async Task CreatePayment(Order order, PaymentResponse result)
        {

            Payment payment = new Payment() { CreatedAt = result.CreatedAt, Status = result.Status, Order = order };
            paymentServices.AddPayment(payment);
            if (result.Status == "failed" || result.Status == "canceled" || result.Status == "expired")
            {
                var message = new MimeMessage();
                var customer = await customerServices.GetById(order.CustomerId);
                message.From.Add(new MailboxAddress("Druivendoos", "druivendoos@glennbeeckman.be"));
                //hier zeggen naar waar een email moet verstuurd worden
                string name = customer.FirstName + " " + customer.LastName;
                message.To.Add(new MailboxAddress(name, customer.Email));
                message.Subject = "Druivendoos ";

                var builder = new BodyBuilder();
                builder.HtmlBody = string.Format(
                    $@"<p>Beste {name}</p>                            
                       <p>Er is iets misgegaan tijdens het verwerken van uw bestelling, u kan ons contacteren via de website.</p>"
                    );

                message.Body = builder.ToMessageBody();

                using var client = new MailKit.Net.Smtp.SmtpClient();
                //hier moet de correcte gegevens ingevuld worden
                //mailserver van host + emailadres van host
                await client.ConnectAsync("smtp-auth.mailprotect.be", 587, SecureSocketOptions.StartTls);
                //dit is authenticatie de persoon die admin is krijgt alleen een email
                await client.AuthenticateAsync("druivendoos@glennbeeckman.be", "druiven12345");
                await client.SendAsync(message);
                await client.DisconnectAsync(true);

                subscriptionServices.Remove(order.Subscription);
                invoiceServices.Remove(order.Invoice);
            }
            else
            if (result.Status == "paid")
            {
                var message = new MimeMessage();
                var customer = await customerServices.GetById(order.CustomerId);
                var boxofthemonthDTO = await boxServices.GetCurrentBoxOfTheMonth();
                var boxOfTheMonth = await boxServices.GetBoxOfTheMonth(boxofthemonthDTO.BoxOfTheMonthId);
                var box = await boxServices.AddBox(customer, boxOfTheMonth);
                order.Subscription.Boxes.Add(box);
                message.From.Add(new MailboxAddress("Druivendoos", "druivendoos@glennbeeckman.be"));
                //hier zeggen naar waar een email moet verstuurd worden
                string name = customer.FirstName + " " + customer.LastName;
                message.To.Add(new MailboxAddress(name, customer.Email));
                message.Subject = "Betaling Druivendoos voltooid";

                var builder = new BodyBuilder();
                builder.HtmlBody = string.Format(
                    $@"<p>Beste {name}</p>                            
                            <p>Uw betaling voor uw druivendoos is voltooid</p>                         
                            
                            
                        "
                    );
                message.Body = builder.ToMessageBody();

                using var client = new MailKit.Net.Smtp.SmtpClient();
                //hier moet de correcte gegevens ingevuld worden
                //mailserver van host + emailadres van host
                await client.ConnectAsync("smtp-auth.mailprotect.be", 587, SecureSocketOptions.StartTls);
                //dit is authenticatie de persoon die admin is krijgt alleen een email
                await client.AuthenticateAsync("druivendoos@glennbeeckman.be", "druiven12345");
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }

        }


    }
}