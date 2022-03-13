using DruivendoosAPI.DTOs;
using DruivendoosAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DruivendoosAPI.Services
{
    public interface IReviewServices
    {
        Task<Review> AddReview(ReviewDTOs.ReviewDTO review, int customerId);
        Task<Review> GetReviewById(int id);
        Task<IEnumerable<Review>> ReviewsFromWine(int id);
    }
}