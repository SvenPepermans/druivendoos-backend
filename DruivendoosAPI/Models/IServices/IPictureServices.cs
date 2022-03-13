using DruivendoosAPI.Models;
using System.Threading.Tasks;

namespace DruivendoosAPI.Services
{
    public interface IPictureServices
    {
        Task AddPicture(Picture picture);
    }
}