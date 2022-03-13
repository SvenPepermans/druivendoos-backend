using System;

namespace DruivendoosAPI.Models
{
    public class Review
    {
        private int _score;
        public int ReviewId { get; set; }
        public int Score
        {
            get { return _score; }
            set
            {
                if (value < 0 || value > 5)
                {
                    throw new ArgumentException("Score moet tussen 0 en 5 liggen.");
                }
                else
                {
                    _score = value;
                }
            }
        }
        public string Description { get; set; }
        public int CustomerId { get; set; }
        public int WineId { get; set; }

        public Review()
        {
        }

        public Review(int score, string description, int customerId, int wineId)
        {
            this.Score = score;
            this.Description = description;
            this.CustomerId = customerId;
            this.WineId = wineId;
        }
    }
}
