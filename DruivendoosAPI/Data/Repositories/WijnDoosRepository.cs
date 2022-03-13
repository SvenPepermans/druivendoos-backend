using DruivendoosAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DruivendoosAPI.Data.Repositories
{
    public class WijnDoosRepository : IWijnDoosRepository

    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<WineBox> _wijnenDozen;

        public WijnDoosRepository(ApplicationDbContext context)
        {
            _context = context;
            _wijnenDozen = context.WinesBoxes;
        }
        public void Add(WineBox wijnDoos)
        {
            _wijnenDozen.Add(wijnDoos);
        }

        public void Delete(WineBox wijnDoos)
        {
            _wijnenDozen.Remove(wijnDoos);
        }

        public IEnumerable<WineBox> GetAll()
        {
            return _wijnenDozen.ToList();
        }

        public WineBox GetBy(int doosId, int wijnId)
        {
            return _wijnenDozen.SingleOrDefault(wd => wd.BoxId == doosId && wd.WineId == wijnId);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
