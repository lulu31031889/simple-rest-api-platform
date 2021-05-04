using SimpleRestApiPlatform.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using SimpleRestApiPlatform.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace SimpleRestApiPlatform.Tests.Controllers
{
    public class RandomNumbersTests
    {
        readonly Mock<IRandomNumberGeneratorService> _mockIRandomNumberGeneratorService;

        public RandomNumbersTests()
        {
            _mockIRandomNumberGeneratorService = new Mock<IRandomNumberGeneratorService>();
        }

        [Fact]
        public void When_parameterless_GetRandomInteger_is_called()
        {
            //Arrange
            var r = new Random();
            var randomNumber = r.Next();

            _mockIRandomNumberGeneratorService
                .Setup(x => x.GenerateRandomSigned32BitInteger(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(randomNumber);

            var controller = new RandomNumbersController(_mockIRandomNumberGeneratorService.Object);

            //Act
            var result = controller.GetRandomInteger();

            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.True(((OkObjectResult)result).StatusCode == 200);
            Assert.True(int.TryParse(((OkObjectResult)result).Value.ToString(), out var parsedValue));
            Assert.True(parsedValue == randomNumber);
        }

        [Fact]
        public void When_GetRandomInteger_is_called_with_2_strings_that_are_within_range_integers_but_min_is_greater_than_max()
        { }

        [Fact]
        public void When_GetRandomInteger_is_called_with_2_strings_that_are_within_range_integers()
        { }

        [Fact]
        public void When_GetRandomInteger_is_called_with_minimumValue_a_within_range_string_and_maximum_value_is_null_or_empty()
        { }

        [Fact]
        public void When_GetRandomInteger_is_called_with_minimumValue_a_within_range_string_and_maximum_value_is_not_an_integer()
        { }

        [Fact]
        public void When_GetRandomInteger_is_called_with_minimumValue_a_within_range_string_and_maximum_value_above_intMaxValue()
        { }



        [Fact]
        public void When_GetRandomInteger_is_called_with_maximumValue_a_within_range_string_and_minimum_value_is_null_or_empty()
        { }

        [Fact]
        public void When_GetRandomInteger_is_called_with_maximumValue_a_within_range_string_and_minimum_value_is_not_an_integer()
        { }

        [Fact]
        public void When_GetRandomInteger_is_called_with_maximumValue_a_within_range_string_and_minimum_value_below_intMinValue()
        { }
    }
}
