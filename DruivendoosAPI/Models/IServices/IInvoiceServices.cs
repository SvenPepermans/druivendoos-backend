using DruivendoosAPI.DTOs;
using DruivendoosAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DruivendoosAPI.Services
{
    public interface IInvoiceServices
    {
        void AddInvoice(Invoice invoice);
        Task<Invoice> GetInvoiceById(int id);
        void Remove(Invoice invoice);
        Task<IEnumerable<InvoiceDTO>> GetAllInvoices();
    }
}