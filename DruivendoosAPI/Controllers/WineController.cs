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
    public class WineController : Controller
    {

        private readonly IWineServices wineService;

        public WineController(IWineServices wineService)
        {
            this.wineService = wineService;
        }

        /// <summary>
        /// Get the wine with given id
        /// </summary>
        /// <param name="id">The id of the wine that we want to see</param>
        /// <returns>The wine</returns>
        [HttpGet("Get/{id}")]
        public async Task<ActionResult<WineDTOs.WineDetail>> GetWine(int id)
        {
            var wine = await wineService.WineDetail(id);
            if (wine == null)
            {
                return NotFound();
            }
            return wine;


        }

        /// <summary>
        /// Gets all the wines
        /// </summary>

        /// <returns>All the wines</returns>
        [HttpGet("GetAll")]
        [Authorize("read:admin")]
        public Task<IEnumerable<WineDTOs.WineDetail>> GetWines()
        {
            return wineService.AllWines();
        }

        /// <summary>
        /// Get all wines from logged in customer
        /// </summary>
        /// <returns>IEnumarable<Wine></returns>
        /// 
        [HttpGet("FromCurrentCustomer")]
        [Authorize("read:klant")]
        public Task<IEnumerable<WineDTOs.WineDetailWithCustomerId>> GetWinesFromCustomer(string email)
        {
            return wineService.WinesFromCostumer(email);
        }


        ///<summary>
        ///Add a wine
        /// </summary>
        /// <param name="wine">The newly created wine</param>
        [HttpPost("Add/{wine}")]
        [Authorize("read:admin")]
        public ActionResult<WineDTOs.WineDetail> AddWine(WineDTOs.NewWine wine)
        {
            var wineToCreate = wineService.AddWine(wine);
            var wineDTO = new WineDTOs.WineDetail(wineToCreate);
            return CreatedAtAction(nameof(GetWine),
                new { id = wineToCreate.WineId }, wineDTO);

        }

        ///<summary>
        ///Deletes the wine with given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The deleted wine if exists</returns>
        [HttpDelete("Delete/{id}")]
        [Authorize("read:admin")]
        public async Task<ActionResult<Wine>> DeleteWine(int id)
        {
            Wine wine = await wineService.GetWineById(id);
            if (wine == null)
            {
                return NotFound();
            }
            await wineService.DeleteWine(wine);
            return wine;
        }
    }
}