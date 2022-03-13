using DruivendoosAPI.DTOs;
using DruivendoosAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DruivendoosAPI.Services
{
    public interface IWineServices
    {
        Wine AddWine(WineDTOs.NewWine wine);
        Task<IEnumerable<WineDTOs.WineDetail>> AllWines();
        Task<WineDTOs.WineDetail> WineDetail(int id);
        Task<IEnumerable<WineDTOs.WineDetailWithCustomerId>> WinesFromCostumer(string email);
        Task<Wine> GetWineById(int id);
        Task DeleteWine(Wine wine);


    }
}