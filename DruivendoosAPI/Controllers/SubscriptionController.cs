using DruivendoosAPI.Models;
using DruivendoosAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DruivendoosAPI.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/[Controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionServices subscriptionServices;

        public SubscriptionController(ISubscriptionServices subscriptionServices)
        {
            this.subscriptionServices = subscriptionServices;
        }

        ///<summary>
        ///Get a subscription with given id
        ///<param name="id">The id from the subscription which we want to get</param>
        ///<return>The subscription</return>
        ///</summary>
        [HttpGet("{id}")]
        public Task<Subscription> GetById(int id)
        {
            return subscriptionServices.GetById(id);
        }


        ///<summary>
        ///Add a subscription
        ///<param name="subscription">The Subscription which needs to be added</param
        /// </summary>
        [HttpPost("Add/{Subscription}")]
        public ActionResult<Subscription> AddSubscription(Subscription subscription)
        {
            subscriptionServices.AddSubscription(subscription);
            return CreatedAtAction(nameof(GetById),
                 new { id = subscription.Id }, subscription);
        }
    }
}
