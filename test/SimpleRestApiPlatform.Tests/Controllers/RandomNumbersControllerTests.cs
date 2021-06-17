using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using SimpleRestApiPlatform.Controllers;
using SimpleRestApiPlatform.Core.Interfaces.Services;
using System;
using Xunit;

namespace SimpleRestApiPlatform.Tests.Controllers
{
    public class RandomNumbersControllerTests
    {
        readonly Mock<IRandomNumberGeneratorService> _mockIRandomNumberGeneratorService;

        public RandomNumbersControllerTests()
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
        public void When_GetRandomInteger_is_called_with_2_strings_that_are_within_range_integers()
        {
            //Arrange
            var r = new Random();
            var randomNumber = r.Next();
            var minValue = "-1";
            var maxValue = "1";

            _mockIRandomNumberGeneratorService
                .Setup(x => x.GenerateRandomSigned32BitInteger(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(randomNumber);

            var controller = new RandomNumbersController(_mockIRandomNumberGeneratorService.Object);

            //Act
            var result = controller.GetRandomInteger(minValue, maxValue);

            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.True(((OkObjectResult)result).StatusCode == 200);
            Assert.True(int.TryParse(((OkObjectResult)result).Value.ToString(), out var parsedValue));
            Assert.True(parsedValue == randomNumber);
        }

        [Fact]
        public void When_GetRandomInteger_is_called_with_2_strings_that_are_within_range_integers_but_min_is_greater_than_max()
        {
            //Arrange
            var r = new Random();
            var randomNumber = r.Next();
            var minValue = "1";
            var maxValue = "-1";

            var expectedProblemDetails = new ProblemDetails
            {
                Detail = "Minimum value greater than maximum value.",
                Status = 422,
                Title = "Maximum value must be greater than minimum value.",
                Type = "System.ArgumentOutOfRangeException"
            };

            _mockIRandomNumberGeneratorService
                .Setup(x => x.GenerateRandomSigned32BitInteger(It.IsAny<int>(), It.IsAny<int>()))
                .Throws(new ArgumentOutOfRangeException(It.IsAny<string>()));

            var controller = new RandomNumbersController(_mockIRandomNumberGeneratorService.Object);

            //Act
            var result = controller.GetRandomInteger(minValue, maxValue);

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.True(((ObjectResult)result).StatusCode == 422);

            var actualProblemDetails = (ProblemDetails)((ObjectResult)result).Value;

            Assert.Equal(JsonConvert.SerializeObject(expectedProblemDetails),
                JsonConvert.SerializeObject(actualProblemDetails));
        }

        [Fact]
        public void When_GetRandomInteger_is_called_with_minimumValue_a_within_range_string_and_maximum_value_is_null_or_empty()
        {
            //Arrange
            var r = new Random();
            var randomNumber = r.Next();
            var minValue = "-1";
            var maxValue = string.Empty;

            var expectedProblemDetails = new ProblemDetails
            {
                Detail = "One or more parameters is not an integer.",
                Status = 400,
                Title = "There was an issue with one (or more) of the parameters.",
                Type = "System.FormatException"
            };

            _mockIRandomNumberGeneratorService
                .Setup(x => x.GenerateRandomSigned32BitInteger(It.IsAny<int>(), It.IsAny<int>()))
                .Throws(new FormatException(It.IsAny<string>()));

            var controller = new RandomNumbersController(_mockIRandomNumberGeneratorService.Object);

            //Act
            var result = controller.GetRandomInteger(minValue, maxValue);

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.True(((ObjectResult)result).StatusCode == 400);

            var actualProblemDetails = (ProblemDetails)((ObjectResult)result).Value;

            Assert.Equal(JsonConvert.SerializeObject(expectedProblemDetails),
                JsonConvert.SerializeObject(actualProblemDetails));
        }

        [Fact]
        public void When_GetRandomInteger_is_called_with_minimumValue_a_within_range_string_and_maximum_value_is_not_an_integer()
        {
            //Arrange
            var r = new Random();
            var randomNumber = r.Next();
            var minValue = "-1";
            var maxValue = "This is not a number!";

            var expectedProblemDetails = new ProblemDetails
            {
                Detail = "One or more parameters is not an integer.",
                Status = 400,
                Title = "There was an issue with one (or more) of the parameters.",
                Type = "System.FormatException"
            };

            _mockIRandomNumberGeneratorService
                .Setup(x => x.GenerateRandomSigned32BitInteger(It.IsAny<int>(), It.IsAny<int>()))
                .Throws(new FormatException(It.IsAny<string>()));

            var controller = new RandomNumbersController(_mockIRandomNumberGeneratorService.Object);

            //Act
            var result = controller.GetRandomInteger(minValue, maxValue);

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.True(((ObjectResult)result).StatusCode == 400);

            var actualProblemDetails = (ProblemDetails)((ObjectResult)result).Value;

            Assert.Equal(JsonConvert.SerializeObject(expectedProblemDetails),
                JsonConvert.SerializeObject(actualProblemDetails));
        }

        [Fact]
        public void When_GetRandomInteger_is_called_with_minimumValue_a_within_range_string_and_maximum_value_above_intMaxValue()
        {
            //Arrange
            var r = new Random();
            var randomNumber = r.Next();
            var minValue = "-1";
            var maxValue = "2147483648"; //1 above int.MaxValue

            var expectedProblemDetails = new ProblemDetails
            {
                Detail = "Either 'minimumValue' is less than -2147483648, or 'maximumValue is greater than 2147483647.",
                Status = 400,
                Title = "There was an issue with one (or more) of the parameters.",
                Type = "System.OverflowException"
            };

            _mockIRandomNumberGeneratorService
                .Setup(x => x.GenerateRandomSigned32BitInteger(It.IsAny<int>(), It.IsAny<int>()))
                .Throws(new OverflowException(It.IsAny<string>()));

            var controller = new RandomNumbersController(_mockIRandomNumberGeneratorService.Object);

            //Act
            var result = controller.GetRandomInteger(minValue, maxValue);

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.True(((ObjectResult)result).StatusCode == 400);

            var actualProblemDetails = (ProblemDetails)((ObjectResult)result).Value;

            Assert.Equal(JsonConvert.SerializeObject(expectedProblemDetails),
                JsonConvert.SerializeObject(actualProblemDetails));
        }

        [Fact]
        public void When_GetRandomInteger_is_called_with_maximumValue_a_within_range_string_and_minimum_value_is_null_or_empty()
        {
            //Arrange
            var r = new Random();
            var randomNumber = r.Next();
            var minValue = string.Empty;
            var maxValue = "1";

            var expectedProblemDetails = new ProblemDetails
            {
                Detail = "One or more parameters is not an integer.",
                Status = 400,
                Title = "There was an issue with one (or more) of the parameters.",
                Type = "System.FormatException"
            };

            _mockIRandomNumberGeneratorService
                .Setup(x => x.GenerateRandomSigned32BitInteger(It.IsAny<int>(), It.IsAny<int>()))
                .Throws(new FormatException(It.IsAny<string>()));

            var controller = new RandomNumbersController(_mockIRandomNumberGeneratorService.Object);

            //Act
            var result = controller.GetRandomInteger(minValue, maxValue);

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.True(((ObjectResult)result).StatusCode == 400);

            var actualProblemDetails = (ProblemDetails)((ObjectResult)result).Value;

            Assert.Equal(JsonConvert.SerializeObject(expectedProblemDetails),
                JsonConvert.SerializeObject(actualProblemDetails));
        }

        [Fact]
        public void When_GetRandomInteger_is_called_with_maximumValue_a_within_range_string_and_minimum_value_is_not_an_integer()
        {
            //Arrange
            var r = new Random();
            var randomNumber = r.Next();
            var minValue = "This is not a number!";
            var maxValue = "1";

            var expectedProblemDetails = new ProblemDetails
            {
                Detail = "One or more parameters is not an integer.",
                Status = 400,
                Title = "There was an issue with one (or more) of the parameters.",
                Type = "System.FormatException"
            };

            _mockIRandomNumberGeneratorService
                .Setup(x => x.GenerateRandomSigned32BitInteger(It.IsAny<int>(), It.IsAny<int>()))
                .Throws(new FormatException(It.IsAny<string>()));

            var controller = new RandomNumbersController(_mockIRandomNumberGeneratorService.Object);

            //Act
            var result = controller.GetRandomInteger(minValue, maxValue);

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.True(((ObjectResult)result).StatusCode == 400);

            var actualProblemDetails = (ProblemDetails)((ObjectResult)result).Value;

            Assert.Equal(JsonConvert.SerializeObject(expectedProblemDetails),
                JsonConvert.SerializeObject(actualProblemDetails));
        }

        [Fact]
        public void When_GetRandomInteger_is_called_with_maximumValue_a_within_range_string_and_minimum_value_below_intMinValue()
        {
            //Arrange
            var r = new Random();
            var randomNumber = r.Next();
            var minValue = "-2147483649"; //1 below int.MinValue
            var maxValue = "1"; //1 above int.MaxValue

            var expectedProblemDetails = new ProblemDetails
            {
                Detail = "Either 'minimumValue' is less than -2147483648, or 'maximumValue is greater than 2147483647.",
                Status = 400,
                Title = "There was an issue with one (or more) of the parameters.",
                Type = "System.OverflowException"
            };

            _mockIRandomNumberGeneratorService
                .Setup(x => x.GenerateRandomSigned32BitInteger(It.IsAny<int>(), It.IsAny<int>()))
                .Throws(new OverflowException(It.IsAny<string>()));

            var controller = new RandomNumbersController(_mockIRandomNumberGeneratorService.Object);

            //Act
            var result = controller.GetRandomInteger(minValue, maxValue);

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.True(((ObjectResult)result).StatusCode == 400);

            var actualProblemDetails = (ProblemDetails)((ObjectResult)result).Value;

            Assert.Equal(JsonConvert.SerializeObject(expectedProblemDetails),
                JsonConvert.SerializeObject(actualProblemDetails));
        }

        [Fact]
        public void When_GetRandomInteger_is_called_with_2_strings_that_are_within_range_integers_but_a_general_internal_exception_is_thrown()
        {
            //Arrange
            var r = new Random();
            var randomNumber = r.Next();
            var minValue = "-1";
            var maxValue = "1";

            var expectedProblemDetails = new ProblemDetails
            {
                Detail = "Some sort of general exception.",
                Status = 400,
                Title = "There was an unknown issue.",
                Type = "System.Exception"
            };

            _mockIRandomNumberGeneratorService
                .Setup(x => x.GenerateRandomSigned32BitInteger(It.IsAny<int>(), It.IsAny<int>()))
                .Throws(new Exception("Some sort of general exception."));

            var controller = new RandomNumbersController(_mockIRandomNumberGeneratorService.Object);

            //Act
            var result = controller.GetRandomInteger(minValue, maxValue);

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.True(((ObjectResult)result).StatusCode == 400);

            var actualProblemDetails = (ProblemDetails)((ObjectResult)result).Value;

            Assert.Equal(JsonConvert.SerializeObject(expectedProblemDetails),
                JsonConvert.SerializeObject(actualProblemDetails));
        }

        [Fact]
        public void When_GetRandomInteger_is_called_with_2_integers_that_are_within_range_integers()
        {
            //Arrange
            var r = new Random();
            var randomNumber = r.Next();
            var minValue = -1;
            var maxValue = 1;

            _mockIRandomNumberGeneratorService
                .Setup(x => x.GenerateRandomSigned32BitInteger(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(randomNumber);

            var controller = new RandomNumbersController(_mockIRandomNumberGeneratorService.Object);

            //Act
            var result = controller.GetRandomInteger(minValue, maxValue);

            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.True(((OkObjectResult)result).StatusCode == 200);
            Assert.True(int.TryParse(((OkObjectResult)result).Value.ToString(), out var parsedValue));
            Assert.True(parsedValue == randomNumber);
        }

        [Fact]
        public void When_GetRandomInteger_is_called_with_2_integers_that_are_within_range_integers_but_min_is_greater_than_max()
        {
            //Arrange
            var r = new Random();
            var randomNumber = r.Next();
            var minValue = 1;
            var maxValue = -1;

            var expectedProblemDetails = new ProblemDetails
            {
                Detail = "Minimum value greater than maximum value.",
                Status = 422,
                Title = "Maximum value must be greater than minimum value.",
                Type = "System.ArgumentOutOfRangeException"
            };

            _mockIRandomNumberGeneratorService
                .Setup(x => x.GenerateRandomSigned32BitInteger(It.IsAny<int>(), It.IsAny<int>()))
                .Throws(new ArgumentOutOfRangeException(It.IsAny<string>()));

            var controller = new RandomNumbersController(_mockIRandomNumberGeneratorService.Object);

            //Act
            var result = controller.GetRandomInteger(minValue, maxValue);

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.True(((ObjectResult)result).StatusCode == 422);

            var actualProblemDetails = (ProblemDetails)((ObjectResult)result).Value;

            Assert.Equal(JsonConvert.SerializeObject(expectedProblemDetails),
                JsonConvert.SerializeObject(actualProblemDetails));
        }

        [Fact]
        public void When_GetRandomInteger_is_called_with_2_integers_that_are_within_range_integers_but_a_general_internal_exception_is_thrown()
        {
            //Arrange
            var r = new Random();
            var randomNumber = r.Next();
            var minValue = -1;
            var maxValue = 1;

            var expectedProblemDetails = new ProblemDetails
            {
                Detail = "Some sort of general exception.",
                Status = 400,
                Title = "There was an unknown issue.",
                Type = "System.Exception"
            };

            _mockIRandomNumberGeneratorService
                .Setup(x => x.GenerateRandomSigned32BitInteger(It.IsAny<int>(), It.IsAny<int>()))
                .Throws(new Exception("Some sort of general exception."));

            var controller = new RandomNumbersController(_mockIRandomNumberGeneratorService.Object);

            //Act
            var result = controller.GetRandomInteger(minValue, maxValue);

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.True(((ObjectResult)result).StatusCode == 400);

            var actualProblemDetails = (ProblemDetails)((ObjectResult)result).Value;

            Assert.Equal(JsonConvert.SerializeObject(expectedProblemDetails),
                JsonConvert.SerializeObject(actualProblemDetails));
        }
    }
}
