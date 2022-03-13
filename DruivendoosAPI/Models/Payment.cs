using System;

namespace DruivendoosAPI.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public Order Order { get; set; }


        public Payment() { }
    }
}
