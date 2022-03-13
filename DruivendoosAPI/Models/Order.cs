namespace DruivendoosAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string MolliePaymentId { get; set; }
        public int CustomerId { get; set; }
        public string MollieOrderId { get; set; }
        public Subscription Subscription { get; set; }
        public Invoice Invoice { get; set; }

        public Order()
        {

        }
    }
}
