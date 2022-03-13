using System.Collections.Generic;

namespace DruivendoosAPI.Models
{
    public class Wine
    {
        public int WineId { get; set; }
        public string GrapeVariety { get; set; }
        public string Story { get; set; }
        public string GrapeColor { get; set; }
        public string Year { get; set; }
        public string GrapeCountry { get; set; }
        public double Rating { get; set; }
        public string GrapeDomain { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public Supplier Supplier { get; set; }
        public string WineName { get; set; }
        public Picture Image { get; set; }
        public int ImageId { get; set; }
        public ICollection<WineBox> WineBoxes { get; set; }
        public ICollection<CustomerWine> CustomerWines { get; set; }

        public Wine()
        {
            this.Reviews = new List<Review>();
            CustomerWines = new List<CustomerWine>();
            WineBoxes = new List<WineBox>();
        }


        public void AddReview(Review review)
        {
            Reviews.Add(review);
        }
    }
}
