using DruivendoosAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Druivendoos.Tests
{
    public class WijnTest
    {
        private readonly Wine _wijn;
        
        public WijnTest() {
            _wijn = new Wine();
}

        [Fact]

        public void AddRecensie_GeldigeRecensie_VoegtRecensieToe()
        {
            Review recensie1 = new Review(3, "beschrijving", 1, 1);
            _wijn.AddRecensie(recensie1);
            Assert.Equal(1, _wijn.Recensies.Count);
            Assert.True(_wijn.Recensies.Contains(recensie1));
        }
       

        [Fact]

        public void CalculateRating_JuisteRating_GeeftCorrecteRatingWeer()
        {

            Wine wijn = new Wine();
            Review recensie1 = new Review(5, "beschrijving", 1, 1);
            wijn.AddRecensie(recensie1);
            Review recensie2 = new Review(4, "beschrijving", 1, 1);
            wijn.AddRecensie(recensie2);
            Review recensie3 = new Review(3, "beschrijving", 1, 1);
            wijn.AddRecensie(recensie3);

            Assert.Equal(4, wijn.CalculateRating());

        }

        [Fact]

        public void CalculateRating_FouteRating_Fails()
        {
            Wine wijn = new Wine();
            Review recensie1 = new Review(5, "beschrijving", 1, 1);
            wijn.AddRecensie(recensie1);
            Review recensie2 = new Review(4, "beschrijving", 1, 1);
            wijn.AddRecensie(recensie2);
            Review recensie3 = new Review(3, "beschrijving", 1, 1);
            wijn.AddRecensie(recensie3);

            
            Assert.NotEqual(6, wijn.CalculateRating());
        }
    }
}
