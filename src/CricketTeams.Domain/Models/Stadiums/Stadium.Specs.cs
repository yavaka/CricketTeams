using CricketTeams.Domain.Exceptions;
using FakeItEasy;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CricketTeams.Domain.Models.Stadiums
{
   public class StadiumSpecs
    {
        private Stadium _stadium;

        public StadiumSpecs()
        {
            this._stadium = A.Dummy<Stadium>();
        }

        [Fact]
        public void InvalidNameShouldThrowException() 
        {
            //Act
            Action act = ()
                => this._stadium.UpdateName("invalid name should throw exception");

            //Assert
            act.Should().Throw<InvalidStadiumException>();
        }

        [Fact]
        public void InvalidCapacityShouldThrowException()
        {
            //Act
            Action act = ()
                => this._stadium.UpdateCapacity(100000000);

            //Assert
            act.Should().Throw<InvalidStadiumException>();
        }

        [Fact]
        public void InvalidWebsiteUrlShouldThrowException()
        {
            //Act
            Action act = ()
                => this._stadium.UpdateWebsiteUrl("invalid.url");

            //Assert
            act.Should().Throw<InvalidStadiumException>();
        }

        [Fact]
        public void InvalidOwnerNameShouldThrowException()
        {
            //Act
            Action act = ()
                => this._stadium.UpdateOwner("s");

            //Assert
            act.Should().Throw<InvalidStadiumException>();
        }
    }
}
