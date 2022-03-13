using DruivendoosAPI.Data;
using DruivendoosAPI.Models;
using System.Threading.Tasks;

namespace DruivendoosAPI.Services
{
    public class PictureServices : IPictureServices
    {
        private readonly ApplicationDbContext context;

        public PictureServices(ApplicationDbContext context)
        {
            this.context = context;
        }

        //Adds a new picture
        public Task AddPicture(Picture picture)
        {
            context.Pictures.Add(picture);
            return context.SaveChangesAsync();
        }
    }
}
