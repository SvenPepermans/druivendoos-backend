using DruivendoosAPI.Models;

namespace DruivendoosAPI.DTOs
{
    public class ReviewDTOs
    {
        public class ReviewDTO
        {
            public int Score { get; set; }
            public string Description { get; set; }
            public int WineId { get; set; }
            public int CustomerId { get; set; }

            public ReviewDTO() { }
        }

        public class ReviewFromWine
        {
            public int Score { get; set; }
            public string Description { get; set; }
            public string CustomerName { get; set; }
            public int ReviewId { get; set; }

            public ReviewFromWine(Review review)
            {
                Score = review.Score;
                Description = review.Description;
                ReviewId = review.ReviewId;
            }
        }
    }
}
