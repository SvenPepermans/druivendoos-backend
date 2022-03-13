using DruivendoosAPI.Models;
using System;
using System.Collections.Generic;

namespace DruivendoosAPI.DTOs
{
    public class BoxOfTheMonthDTO
    {
        public int BoxOfTheMonthId { get; set; }
        public ICollection<WineDTOs.WineInBoxOfTheMonthAndroidDTO> Wines { get; set; }
        public DateTime CreatedAt { get; set; }

        public BoxOfTheMonthDTO(BoxOfTheMonth box)
        {
            BoxOfTheMonthId = box.BoxOfTheMonthId;
            CreatedAt = box.CreatedAt;
            Wines = new List<WineDTOs.WineInBoxOfTheMonthAndroidDTO>();
            foreach (var wine in box.Wines)
            {
                Wines.Add(new WineDTOs.WineInBoxOfTheMonthAndroidDTO(wine));
            }
        }
    }
}
