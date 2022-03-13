using System;
using System.Collections.Generic;

namespace DruivendoosAPI.Models
{
    public class BoxOfTheMonth
    {
        public int BoxOfTheMonthId { get; set; }
        public ICollection<Wine> Wines { get; set; }
        public DateTime CreatedAt { get; set; }

        public BoxOfTheMonth()
        {
            CreatedAt = DateTime.Now;
        }
        public BoxOfTheMonth(List<Wine> wines) : this()
        {
            Wines = wines;
        }
    }
}
