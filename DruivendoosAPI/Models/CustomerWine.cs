namespace DruivendoosAPI.Models
{
    public class CustomerWine
    {
        public int CustomerId { get; set; }
        public int WineId { get; set; }
        public Customer Customer { get; set; }
        public Wine Wine { get; set; }

        protected CustomerWine()
        { }

        public CustomerWine(Customer customer, Wine wine) : this()
        {
            Customer = customer;
            Wine = wine;
            CustomerId = customer.CustomerId;
            WineId = wine.WineId;
        }
    }
}
