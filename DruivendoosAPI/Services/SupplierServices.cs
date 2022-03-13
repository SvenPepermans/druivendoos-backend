using DruivendoosAPI.Data;
using DruivendoosAPI.DTOs;
using DruivendoosAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DruivendoosAPI.Services
{
    public class SupplierServices : ISupplierServices
    {
        private readonly ApplicationDbContext context;

        public SupplierServices(ApplicationDbContext context)
        {
            this.context = context;
        }

        //Gets a supplier with its details
        public async Task<SupplierDTOs.RetrieveSupplier> SupplierDetail(int id)
        {
            return new SupplierDTOs.RetrieveSupplier(await context.Suppliers.Include(s => s.Wines).SingleOrDefaultAsync(s => s.Id.Equals(id)));
        }

        //Adds a supplier
        public Supplier AddSupplier(SupplierDTOs.NewSupplier supplier)
        {
            Supplier supplierToCreate = new Supplier()
            {
                Name = supplier.Name,
                Email = supplier.Email,
                Wines = new List<Wine>(),
            };
            if (supplier.Wines != null)
            {
                foreach (var wine in supplier.Wines)
                {
                    supplierToCreate.AddWine(wine);
                }
            }
            context.Suppliers.Add(supplierToCreate);
            context.SaveChanges();
            return supplierToCreate;
        }

        //Edits a supplier with new values
        public Task EditSupplier(Supplier supplier)
        {
            context.Suppliers.Update(supplier);
            return context.SaveChangesAsync();
        }

        //Removes a supplier
        public Task DeleteSupplier(Supplier supplier)
        {
            context.Suppliers.Remove(supplier);
            return context.SaveChangesAsync();
        }

        //Gets a supplier by id
        public Task<Supplier> GetSupplierForRemove(int id)
        {
            return context.Suppliers.SingleOrDefaultAsync(s => s.Id.Equals(id));
        }

        //Gets all the suppliers
        public async Task<IEnumerable<SupplierDTOs.RetrieveSupplier>> AllSuppliers()
        {
            var returnSuppliers = new List<SupplierDTOs.RetrieveSupplier>();
            var suppliers = await context.Suppliers.Include(s => s.Wines).ToListAsync();
            foreach (var sup in suppliers)
            {
                returnSuppliers.Add(new SupplierDTOs.RetrieveSupplier(sup));
            }
            return returnSuppliers;

        }
    }
}
