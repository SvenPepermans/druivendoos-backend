using DruivendoosAPI.Models;

namespace DruivendoosAPI.DTOs
{
    public class SubscriptionDTO
    {
        public int CustomerId { get; set; }
        public Length Length { get; set; }
        public Models.Type Type { get; set; }

        public SubscriptionDTO()
        {

        }
    }
}
