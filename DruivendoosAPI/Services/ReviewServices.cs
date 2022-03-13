using DruivendoosAPI.Data;
using DruivendoosAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DruivendoosAPI.DTOs.ReviewDTOs;

namespace DruivendoosAPI.Services
{
    public class ReviewServices : IReviewServices
    {
        private readonly ApplicationDbContext context;

        public ReviewServices(ApplicationDbContext context)
        {
            this.context = context;
        }

        //Adds a new review
        public async Task<Review> AddReview(ReviewDTO review, int customerId)
        {
            Review reviewToCreate = new Review()
            {
                CustomerId = customerId,
                WineId = review.WineId,
                Score = review.Score,
                Description = review.Description,

            };
            var wine = await context.Wines.SingleOrDefaultAsync(w => w.WineId == review.WineId);
            await context.Reviews.AddAsync(reviewToCreate);
            var rating = CalculateRating(wine.Reviews);
            wine.Rating = rating;
            await context.SaveChangesAsync();
            return reviewToCreate;
        }

        //Calculates the rating of a wine after adding a new review to its Reviews list
        private double CalculateRating(ICollection<Review> reviews)
        {
            double rating = 0;
            foreach (Review review in reviews)
            {
                rating += review.Score;
            }
            rating /= reviews.Count();

            return rating;
        }

        //Gets all the review from a wine with given id
        public async Task<IEnumerable<Review>> ReviewsFromWine(int id)
        {
            return await context.Reviews.Where(r => r.WineId.Equals(id)).ToListAsync();
        }

        //Gets a review by its id
        public Task<Review> GetReviewById(int id)
        {
            return context.Reviews.SingleOrDefaultAsync(r => r.ReviewId.Equals(id));
        }
    }
}
