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
    public class ReviewController : ControllerBase
    {
        private readonly IReviewServices reviewServices;
        private readonly ICustomerServices customerServices;

        public ReviewController(IReviewServices reviewServices, ICustomerServices customerServices)
        {
            this.reviewServices = reviewServices;
            this.customerServices = customerServices;
        }

        /// <summary>
        /// Get all reviews from given wine
        /// </summary>
        /// <param name="wineId">The wine from which we want te reviews</param>
        /// <returns>IEnumarable<Reviews></returns>
        /// 
        [HttpGet("ReviewsFromWine")]
        [Authorize("read:klant")]
        public Task<IEnumerable<Review>> GetReviewsFromWine(int wineId)
        {
            return reviewServices.ReviewsFromWine(wineId);
        }

        ///<summary>
        ///Add a review
        /// </summary>
        /// <param name="review">The review which the current customer wants to add</param>
        [HttpPost("Add/{review}")]
        [Authorize("read:klant")]
        public async Task<ActionResult<Review>> AddReview(ReviewDTOs.ReviewDTO review)
        {
            var customer = await customerServices.GetById(review.CustomerId);
            var reviewToCreate = await reviewServices.AddReview(review, customer.CustomerId);
            return CreatedAtAction(nameof(GetReview),
                new { id = reviewToCreate.ReviewId }, reviewToCreate);
        }


        ///<summary>
        ///Get review with given id
        ///</summary>
        ///<param name="id">Id from review we want to see</param>
        ///<returns>the review</returns>
        [HttpGet("Review/Get/{id}")]
        [Authorize("read:klant")]
        public Task<Review> GetReview(int id)
        {
            return reviewServices.GetReviewById(id);
        }
    }
}
