using DruivendoosAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Druivendoos.Tests.Models
{
    public class RecensieTest
    {

        [Fact]
        public void NieuweRecensie_GeldigeRecensie_MaaktNieuweRecensieAan()
        {
            Review recensie = new Review(3, "een beschrijving", 1, 1);
            Assert.Equal("een beschrijving", recensie.Description);
            Assert.Equal(3, recensie.Score);
            Assert.Equal(1, recensie.CustomerId);
            Assert.Equal(1, recensie.WineId);
        }

        [Fact]
        public void NieuweRecensie_OngeldigeScore_ThrowException()
        {
            Assert.Throws<ArgumentException>(() => new Review(-1, "een beschrijving", 1, 1));
            
        }
    }
}
