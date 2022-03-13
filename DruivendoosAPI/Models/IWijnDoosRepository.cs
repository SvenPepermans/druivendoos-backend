using System.Collections.Generic;

namespace DruivendoosAPI.Models
{
    public interface IWijnDoosRepository
    {
        IEnumerable<WineBox> GetAll();
        WineBox GetBy(int doosId, int wijnId);
        void Add(WineBox wijnDoos);
        void Delete(WineBox wijnDoos);
        void SaveChanges();
    }
}
