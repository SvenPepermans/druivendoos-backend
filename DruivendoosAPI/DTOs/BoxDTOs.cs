using DruivendoosAPI.Models;
using System;
using System.Collections.Generic;

namespace DruivendoosAPI.DTOs
{
    public class BoxDTOs
    {

        public class NewBox
        {

            public ICollection<int> WineIds { get; set; }
            public Models.Type Type { get; set; }

            public NewBox()
            {
                WineIds = new List<int>();


            }
        }

        public class BoxFromCustomerDetail
        {
            public ICollection<WineDTOs.WineShortDetail> Wines { get; set; }
            public Models.Type Type { get; set; }
            public DateTime CreatedAt { get; set; }
            public string City { get; set; }
            public string Street { get; set; }
            public string HouseNumber { get; set; }
            public string PostalCode { get; set; }

            public BoxFromCustomerDetail(Box box)
            {
                Type = box.Type;
                CreatedAt = box.CreatedAt;
                Wines = new List<WineDTOs.WineShortDetail>();
                City = box.City;
                Street = box.Street;
                HouseNumber = box.HouseNumber;
                PostalCode = box.PostalCode;
                foreach (WineBox wine in box.Wines)
                {
                    var wineDTO = new WineDTOs.WineShortDetail(wine.Wine);
                    Wines.Add(wineDTO);
                }
            }
        }

        public class BoxFromType
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string City { get; set; }
            public string Street { get; set; }
            public string HouseNumber { get; set; }
            public string PostalCode { get; set; }
            public DateTime CreatedAt { get; set; }
            public ICollection<WineDTOs.WineShortDetail> Wines { get; set; }

            public BoxFromType(Box box)
            {
                CreatedAt = box.CreatedAt;
                Wines = new List<WineDTOs.WineShortDetail>();
                City = box.City;
                Street = box.Street;
                HouseNumber = box.HouseNumber;
                PostalCode = box.PostalCode;
                foreach (WineBox wine in box.Wines)
                {
                    var wineDTO = new WineDTOs.WineShortDetail(wine.Wine);
                    Wines.Add(wineDTO);
                }
            }
        }
        public class BoxDetail
        {
            public int Id { get; set; }
            public string City { get; set; }
            public string Street { get; set; }
            public string HouseNumber { get; set; }
            public string PostalCode { get; set; }
            public DateTime CreatedAt { get; set; }
            public ICollection<WineDTOs.WineShortDetail> Wines { get; set; }
            public Models.Type Type { get; set; }

            public BoxDetail(Box box)
            {

                Id = box.Id;
                CreatedAt = box.CreatedAt;
                Wines = new List<WineDTOs.WineShortDetail>();
                City = box.City;
                Street = box.Street;
                HouseNumber = box.HouseNumber;
                PostalCode = box.PostalCode;
                Type = box.Type;
                foreach (WineBox wine in box.Wines)
                {
                    var wineDTO = new WineDTOs.WineShortDetail(wine.Wine);
                    Wines.Add(wineDTO);
                }
            }
        }
        public class BoxSentStatus
        {
            public int Id { get; set; }
            public string City { get; set; }
            public string Street { get; set; }
            public string HouseNumber { get; set; }
            public string PostalCode { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public Boolean IsSent { get; set; }
            public ICollection<WineDTOs.WineDetail> Wines { get; set; }

            public BoxSentStatus()
            {
                Wines = new List<WineDTOs.WineDetail>();
            }
            public BoxSentStatus(Box box) : this()
            {
                Id = box.Id;
                IsSent = box.IsSent;
                City = box.City;
                Street = box.Street;
                HouseNumber = box.HouseNumber;
                PostalCode = box.PostalCode;
                foreach (WineBox wine in box.Wines)
                {
                    var wineDTO = new WineDTOs.WineDetail(wine.Wine);
                    Wines.Add(wineDTO);
                }
            }


        }

    }
}
