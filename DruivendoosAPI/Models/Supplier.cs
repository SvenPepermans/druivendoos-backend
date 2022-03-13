using System.Collections.Generic;
using System.Linq;

namespace DruivendoosAPI.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<Wine> Wines { get; set; }

        public Supplier()
        {
            Wines = new List<Wine>();
        }
        public Supplier(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public void AddWine(Wine wine)
        {
            Wines.Append(wine);
        }

    }
}
