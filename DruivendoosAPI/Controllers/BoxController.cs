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
    public class BoxController : ControllerBase
    {
        private readonly IBoxServices boxServices;

        public BoxController(IBoxServices boxServices)
        {
            this.boxServices = boxServices;
        }

        /// <summary>
        /// Get the box with given id
        /// </summary>
        /// <param name="id">The id of the box that we want to see</param>
        /// <returns>The box</returns>
        [HttpGet("Boxes/{id}")]
        [Authorize("read:admin")]
        public async Task<ActionResult<BoxDTOs.BoxDetail>> GetBox(int id)
        {
            var box = await boxServices.BoxDetail(id);
            if (box is null)
            {
                return NotFound();
            }
            return box;
        }

        /// <summary>
        /// Get the boxOfTheMonth with given id
        /// </summary>
        /// <param name="id">The id of the boxOfTheMonth that we want to see</param>
        /// <returns>The box</returns>
        [HttpGet("BoxesOfTheMonth/{id}")]
        [Authorize("read:klant")]
        public async Task<ActionResult<BoxOfTheMonth>> GetBoxOfTheMonth(int id)
        {
            var box = await boxServices.GetBoxOfTheMonth(id);
            if (box is null)
            {
                return NotFound();
            }
            return box;
        }

        /// <summary>
        /// Get the current boxOfTheMonth
        /// </summary>
        /// <returns>The box</returns>
        [HttpGet("BoxesOfTheMonth/CurrentBoxOfTheMonth")]
        public async Task<ActionResult<BoxOfTheMonthDTO>> GetCurrentBoxOfTheMonth()
        {
            var box = await boxServices.GetCurrentBoxOfTheMonth();
            if (box is null)
            {
                return NotFound();
            }
            return box;
        }

        /// <summary>
        /// Get the boxes from the customer with given id
        /// </summary>
        /// <param name="customerId">The id of the customer from whom we want to see all the boxes</param>
        /// <returns>The boxes from given customer with: Wines, Type, CreatedAt date, Address</returns>
        [HttpGet("BoxesFromCustomer/{customerId}")]
        [Authorize("read:klant")]
        public Task<IEnumerable<BoxDTOs.BoxFromCustomerDetail>> GetBoxesFromCustomer(int customerId)
        {
            return boxServices.GetBoxesFromCustomer(customerId);
        }

        /// <summary>
        /// Get the boxes from the given type
        /// </summary>
        /// <param name="type">The type of boxes we want to see</param>
        /// <returns>The boxes from given type with: Customer first- and lastname, address, Wines, CreatedAt date </returns>
        [HttpGet("BoxesFromType/{type}")]
        [Authorize("read:admin")]
        public Task<IEnumerable<BoxDTOs.BoxFromType>> GetBoxesFromType(Models.Type type)
        {

            return boxServices.GetBoxesFromType(type);

        }

        /// <summary>
        /// Get the boxes with delivery status
        /// </summary>
        /// <returns>All The boxes from given type with: Customer first- and lastname, address and IsSent status</returns>
        [Authorize("read:admin")]
        [HttpGet("BoxesWithSentStatus")]
        public Task<IEnumerable<BoxDTOs.BoxSentStatus>> GetBoxesWithSentStatus()
        {

            return boxServices.GetBoxesWithSentStatus();

        }

        /// <summary>
        /// Adds new boxes
        /// </summary>
        /// <param name="box">The box which has to be created</param>
        [HttpPost("Add/{box}")]
        [Authorize("read:admin")]
        public async Task<ActionResult<BoxDTOs.BoxDetail>> AddBox(BoxDTOs.NewBox box)
        {
            try
            {
                BoxOfTheMonth boxOfTheMonth = await boxServices.AddBoxOfTheMonth(box);


                return CreatedAtAction(nameof(GetBoxOfTheMonth),
                     new { id = boxOfTheMonth.BoxOfTheMonthId }, boxOfTheMonth);
            }
            catch
            {
                return new BadRequestResult();
            }
        }

        ///<summary>
        ///Changes IsSent from false to true
        /// </summary>
        /// <param name="id">The id of the box that has to be changed</param>
        [HttpPut("ChangeSentStatus/{id}")]
        [Authorize("read:admin")]
        public async Task<IActionResult> EditSentStatus(int id)
        {
            try
            {
                await boxServices.ChangeSentStatus(id);
                return NoContent();
            }
            catch
            {
                return BadRequest();
            }

        }
    }
}