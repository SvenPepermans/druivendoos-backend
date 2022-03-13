using DruivendoosAPI.DTOs;
using DruivendoosAPI.Models;
using DruivendoosAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DruivendoosAPI.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/[Controller]")]
    [ApiController]
    [Authorize("read:admin")]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierServices supplierServices;

        public SupplierController(ISupplierServices supplierServices)
        {
            this.supplierServices = supplierServices;
        }

        /// <summary>
        /// Get the supplier with given id
        /// </summary>
        /// <param name="id">The id of the supplier that we want to see</param>
        /// <returns>The supplier</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<SupplierDTOs.RetrieveSupplier>> GetSupplier(int id)
        {
            return await supplierServices.SupplierDetail(id);

        }

        [HttpGet("GetAll")]
        public Task<IEnumerable<SupplierDTOs.RetrieveSupplier>> GetAll()
        {
            return supplierServices.AllSuppliers();
        }

        /// <summary>
        /// Adds new Supplier
        /// </summary>
        /// <param name="supplier">The newly created supplier</param>
        [HttpPost("Add/{Supplier}")]
        public ActionResult<Supplier> AddSupplier(SupplierDTOs.NewSupplier supplier)
        {
            Supplier newSupplier = supplierServices.AddSupplier(supplier);
            return CreatedAtAction(nameof(GetSupplier),
                new { id = newSupplier.Id }, newSupplier);
        }

        ///<summary>
        ///Updates a supplier with given id
        /// </summary>
        /// <param name="supplierId">Id of the supplier we want to update</param>
        /// <param name="supplier">The updated supplier</param>
        /// <returns>The</returns> 
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> EditSupplier(int supplierId, Supplier supplier)
        {
            if (supplierId != supplier.Id)
            {
                return BadRequest();
            }
            await supplierServices.EditSupplier(supplier);
            return NoContent();
        }

        ///<summary>
        ///Deletes the supplier with given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The deleted supplier if exists</returns>
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<Supplier>> DeleteSupplier(int id)
        {
            Supplier supplier = await supplierServices.GetSupplierForRemove(id);
            if (supplier == null)
            {
                return NotFound();
            }
            await supplierServices.DeleteSupplier(supplier);
            return supplier;
        }
    }
}