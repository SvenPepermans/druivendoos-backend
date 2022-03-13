using DruivendoosAPI.Models;
using System.Collections.Generic;

namespace DruivendoosAPI.DTOs
{
    public class SupplierDTOs
    {
        public class NewSupplier
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public ICollection<Wine> Wines { get; set; }

            public NewSupplier() { }
            public NewSupplier(Supplier supplier)
            {
                Name = supplier.Name;
                Email = supplier.Email;
                Wines = supplier.Wines;
            }
        }
        public class RetrieveSupplier
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public ICollection<WineDTOs.WineShortDetail> Wines { get; set; }
            public RetrieveSupplier() { }

            public RetrieveSupplier(Supplier supplier)
            {
                Name = supplier.Name;
                Email = supplier.Email;
                Wines = new List<WineDTOs.WineShortDetail>();
                foreach (Wine wine in supplier.Wines)
                {
                    var wineDTO = new WineDTOs.WineShortDetail(wine);
                    Wines.Add(wineDTO);
                }

            }
        }
    }
}
