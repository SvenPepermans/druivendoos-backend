using DruivendoosAPI.DTOs;
using DruivendoosAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DruivendoosAPI.Services
{
    public interface ISupplierServices
    {
        Supplier AddSupplier(SupplierDTOs.NewSupplier supplier);
        Task<IEnumerable<SupplierDTOs.RetrieveSupplier>> AllSuppliers();
        Task DeleteSupplier(Supplier supplier);
        Task EditSupplier(Supplier supplier);
        Task<Supplier> GetSupplierForRemove(int id);
        Task<SupplierDTOs.RetrieveSupplier> SupplierDetail(int id);
    }
}