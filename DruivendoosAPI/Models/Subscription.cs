using System;
using System.Collections.Generic;

namespace DruivendoosAPI.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public ICollection<Box> Boxes { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Length Length { get; set; }
        public Type Type { get; set; }
        public Boolean IsActive { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public Subscription()
        {
            IsActive = true;
            Boxes = new List<Box>();
            StartDate = DateTime.Today;
            if (Length == Length.Month)
            {
                EndDate = StartDate.AddMonths(1);
            }
            else if (Length == Length.Year)
            {
                EndDate = StartDate.AddYears(1);
            }
        }

        public Subscription(int customerId) : base()
        {
            CustomerId = customerId;

        }
    }
}
