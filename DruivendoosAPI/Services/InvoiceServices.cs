using DruivendoosAPI.Data;
using DruivendoosAPI.DTOs;
using DruivendoosAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DruivendoosAPI.Services
{
    public class InvoiceServices : IInvoiceServices
    {
        private readonly ApplicationDbContext context;

        public InvoiceServices(ApplicationDbContext context)
        {
            this.context = context;
        }

        //Adds an invoice
        public void AddInvoice(Invoice invoice)
        {
            context.Invoices.Add(invoice);
            context.SaveChanges();
        }

        //Gets invoice with given id
        public Task<Invoice> GetInvoiceById(int id)
        {
            return context.Invoices.SingleOrDefaultAsync(i => i.Id == id);
        }

        //Deletes an invoice
        public void Remove(Invoice invoice)
        {
            context.Invoices.Remove(invoice);
            context.SaveChanges();
        }

        //Gets all the invoices
        public async Task<IEnumerable<InvoiceDTO>> GetAllInvoices()
        {
            var invoices = await context.Invoices.OrderByDescending(i => i.Date).ToListAsync();
            var invoicesToReturn = new List<InvoiceDTO>();
            foreach (var invoice in invoices)
            {
                var invoicedto = new InvoiceDTO(invoice);
                invoicesToReturn.Add(invoicedto);
            }
            return invoicesToReturn;
        }
    }
}
