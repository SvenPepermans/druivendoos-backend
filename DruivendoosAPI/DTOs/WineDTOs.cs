using DruivendoosAPI.Models;
using System.Collections.Generic;

namespace DruivendoosAPI.DTOs
{
    public class WineDTOs
    {

        public class NewWine
        {
            public string GrapeVariety { get; set; }
            public string Story { get; set; }
            public string GrapeColor { get; set; }
            public string Year { get; set; }
            public string GrapeCountry { get; set; }
            public string GrapeDomain { get; set; }
            public string SupplierName { get; set; }
            public string SupplierEmail { get; set; }
            public string WineName { get; set; }
            public Picture Image { get; set; }

            public NewWine() { }

            public NewWine(Wine wine)
            {
                GrapeVariety = wine.GrapeVariety;
                Story = wine.Story;
                GrapeColor = wine.GrapeColor;
                Year = wine.Year;
                GrapeCountry = wine.GrapeCountry;
                GrapeDomain = wine.GrapeDomain;
                //Supplier = wine.Supplier;
                WineName = wine.WineName;
                Image = wine.Image;
            }
        }

        public class WineDetail
        {
            public int WineId { get; set; }
            public string GrapeVariety { get; set; }
            public string Story { get; set; }
            public string GrapeColor { get; set; }
            public string Year { get; set; }
            public string GrapeCountry { get; set; }
            public string GrapeDomain { get; set; }
            public string SupplierName { get; set; }
            public string WineName { get; set; }
            public Picture Image { get; set; }
            public double Rating { get; set; }
            public WineDetail(Wine wine)
            {
                WineId = wine.WineId;
                GrapeVariety = wine.GrapeVariety;
                Story = wine.Story;
                GrapeColor = wine.GrapeColor;
                Year = wine.Year;
                GrapeCountry = wine.GrapeCountry;
                GrapeDomain = wine.GrapeDomain;
                SupplierName = wine.Supplier.Name;
                WineName = wine.WineName;
                Image = wine.Image;
                Rating = wine.Rating;
            }
        }

        public class WineShortDetail
        {
            public string GrapeVariety { get; set; }
            public string GrapeCountry { get; set; }
            public string GrapeDomain { get; set; }
            public string Year { get; set; }
            public string GrapeColor { get; set; }
            public string WineName { get; set; }
            public Picture Image { get; set; }

            public WineShortDetail(Wine wine)
            {
                GrapeVariety = wine.GrapeVariety;
                GrapeColor = wine.GrapeColor;
                Year = wine.Year;
                GrapeCountry = wine.GrapeCountry;
                GrapeDomain = wine.GrapeDomain;
                WineName = wine.WineName;
                Image = wine.Image;
            }

        }

        public class WineDetailWithCustomerId
        {
            public int WineId { get; set; }
            public string GrapeVariety { get; set; }
            public string Story { get; set; }
            public string GrapeColor { get; set; }
            public string Year { get; set; }
            public string GrapeCountry { get; set; }
            public string GrapeDomain { get; set; }
            public string WineName { get; set; }
            public string ImageUrl { get; set; }
            public double Rating { get; set; }
            public int CustomerId { get; set; }
            public ICollection<ReviewDTOs.ReviewFromWine> Reviews { get; set; }
            public WineDetailWithCustomerId(Wine wine, List<ReviewDTOs.ReviewFromWine> reviews)
            {
                WineId = wine.WineId;
                GrapeVariety = wine.GrapeVariety;
                Story = wine.Story;
                GrapeColor = wine.GrapeColor;
                Year = wine.Year;
                GrapeCountry = wine.GrapeCountry;
                GrapeDomain = wine.GrapeDomain;
                WineName = wine.WineName;
                ImageUrl = wine.Image.Url;
                Rating = wine.Rating;
                Reviews = reviews;
            }
        }

        public class WineInBoxOfTheMonthAndroidDTO
        {
            public int WineId { get; set; }
            public string GrapeVariety { get; set; }
            public string Story { get; set; }
            public string GrapeColor { get; set; }
            public string Year { get; set; }
            public string GrapeCountry { get; set; }
            public string GrapeDomain { get; set; }
            public string ImageUrl { get; set; }
            public double Rating { get; set; }
            public string WineName { get; set; }


            public WineInBoxOfTheMonthAndroidDTO(Wine wine)
            {
                WineId = wine.WineId;
                GrapeVariety = wine.GrapeVariety;
                Story = wine.Story;
                GrapeColor = wine.GrapeColor;
                Year = wine.Year;
                GrapeCountry = wine.GrapeCountry;
                GrapeDomain = wine.GrapeDomain;
                ImageUrl = wine.Image.Url;
                Rating = wine.Rating;
                WineName = wine.WineName;

            }
        }

    }
}
