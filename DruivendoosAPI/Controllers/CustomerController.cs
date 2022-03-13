using DruivendoosAPI.DTOs;
using DruivendoosAPI.Models;
using DruivendoosAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DruivendoosAPI.Controllers
{
    //[ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/[controller]")]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerServices customerServices;
        private readonly IInvoiceServices invoiceServices;
        private readonly IWineServices wineServices;

        public CustomerController(ICustomerServices customerServices, IInvoiceServices invoiceServices, IWineServices wineServices)
        {
            this.customerServices = customerServices;
            this.invoiceServices = invoiceServices;
            this.wineServices = wineServices;
        }

        /// <summary>
        /// Get the details of the customer with given email
        /// </summary>
        /// <param name="email">The email from the customer we want to get</param>
        /// <returns>the customer</returns>
        /// 
        [Authorize("read:klant")]
        [HttpGet("ByEmail/{email}")]         

        public async Task<ActionResult<CustomerDTOs.CustomerDetail>> GetCustomerByEmail(string email)
        {
            var user = User;
            var customer = await customerServices.GetByEmail(email);
            if (customer == null)
            {
                return NotFound();
            }
            return new CustomerDTOs.CustomerDetail(customer);

        }

        ///<summary>
        ///Get customer with given id
        ///</summary>
        ///<param name="id">Id from customer we want to see</param>
        ///<returns>the customer</returns>    
        [HttpGet("{id}")]
        [Authorize("read:klant")]
        public Task<Customer> GetCustomer(int id)
        {
            return customerServices.GetById(id);
        }

        ///<summary>
        ///Get customers with valid Subscription
        ///</summary>
        ///<returns>the customers</returns>
        [Authorize("read:admin")]
        [HttpGet("CustomersWithValidSubscription")]
        public Task<IEnumerable<CustomerDTOs.CustomerWithValidSubscription>> GetCustomersWithValidSubscription()
        {
            return customerServices.GetCustomersWithValidSubscription();
        }



        ///<summary>
        ///Gets all the Customers
        /// </summary>
        /// <returns>Array of customers</returns>
        [Authorize("read:admin")]
        [HttpGet("customers")]
        public Task<IEnumerable<CustomerDTOs.GetCustomer>> GetCustomers()
        {
            return customerServices.GetAllCustomers();
        }

        /// <summary>
        /// Adds new Customer
        /// </summary>
        /// <param name="customer">The newly created customer</param>
        [HttpPost("Add/{customer}")]
        public ActionResult<Customer> AddCustomer(CustomerDTOs.NewCustomer customer)
        {
            Customer newCustomer = customerServices.AddCustomer(customer);
            return CreatedAtAction(nameof(GetCustomer),
                new { id = newCustomer.CustomerId }, newCustomer);
        }

        ///<summary>
        ///Updates a customer with given id
        /// </summary>      
        /// <param name="customerDetail">The updated customer</param>
        /// <returns>The</returns> 
        [Authorize("read:klant")]
        [HttpPut("EditCustomer/{customer}")]       
        public async Task<IActionResult> EditCustomer(CustomerDTOs.CustomerDetail customerDetail)
        {
            var cust = await customerServices.GetById(customerDetail.CustomerId);
            if (cust == null)
            {
                return NotFound();
            }

            try
            {
                await customerServices.EditCustomer(customerDetail, cust);
                return StatusCode(200);
            }
            catch
            {
                return BadRequest();
            }




        }

        ///<summary>
        ///Deletes the customer with given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The deleted customer if exists</returns>
        [Authorize("read:klant")]
        [HttpDelete("DeleteCustomer/{id}")]
        public async Task<ActionResult<Customer>> DeleteCustomer(int id)
        {
            Customer customer = await customerServices.GetById(id);
            if (customer == null)
            {
                return NotFound();
            }
            await customerServices.DeleteCustomer(customer);
            return customer;
        }

        /// <summary>
        /// Get the details of the authenticated customer
        /// </summary>
        /// <returns>the customer</returns>
        /// 
        [Authorize("read:admin")]
        [HttpGet("Get/AllInvoices")]
        
        public Task<IEnumerable<InvoiceDTO>> GetAllInvoices()
        {
            return invoiceServices.GetAllInvoices();
        }




    }
}