using SimpleRestApiPlatform.Infrastructure.Services;
using System;
using System.Linq;
using Xunit;

namespace SimpleRestApiPlatform.Infrastructure.Tests.Services
{
    public class RandomNumberGeneratorServiceTests
    {
        [Fact]
        public void When_a_request_is_made_to_generate_a_32_bit_integer_with_no_parameters_given()
        {
            //Arrange
            var randomNumberGeneratorService = new RandomNumberGeneratorService();

            //Act
            var result = randomNumberGeneratorService.GenerateRandomSigned32BitInteger();

            //Assert
            Assert.True(result >= int.MinValue);
            Assert.True(result <= int.MaxValue);
        }

        [Fact]
        public void When_a_request_is_made_to_generate_a_32_bit_integer_with_minimumValue_parameter_only()
        {
            //Arrange
            var randomNumberGeneratorService = new RandomNumberGeneratorService();

            //Act
            var result = randomNumberGeneratorService.GenerateRandomSigned32BitInteger(minimumValue: int.MinValue);

            //Assert
            Assert.True(result >= int.MinValue);
            Assert.True(result <= int.MaxValue);
        }

        [Fact]
        public void When_a_request_is_made_to_generate_a_32_bit_integer_with_maximumValue_parameter_only()
        {
            //Arrange
            var randomNumberGeneratorService = new RandomNumberGeneratorService();

            //Act
            var result = randomNumberGeneratorService.GenerateRandomSigned32BitInteger(maximumValue: int.MaxValue);

            //Assert
            Assert.True(result >= int.MinValue);
            Assert.True(result <= int.MaxValue);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(-1, 1)]
        [InlineData(-100, 100)]
        [InlineData(0, 100)]
        [InlineData(-100, 0)]
        [InlineData(-100, -50)]
        [InlineData(50, 100)]
        public void When_a_request_is_made_to_generate_a_32_bit_integer_with_minimumValue_and_maximumValue_parameters(int testMinimumValue, int testMaximumValue)
        {
            //Arrange
            var randomNumberGeneratorService = new RandomNumberGeneratorService();

            //Act
            var result = randomNumberGeneratorService.GenerateRandomSigned32BitInteger(testMinimumValue, testMaximumValue);

            //Assert
            Assert.True(result >= testMinimumValue);
            Assert.True(result <= testMaximumValue);
        }

        [Theory]
        [InlineData(1, -1)]
        [InlineData(100, -100)]
        [InlineData(0, -100)]
        [InlineData(100, 0)]
        [InlineData(100, 50)]
        [InlineData(-50, -100)]
        public void When_a_request_is_made_to_generate_a_32_bit_integer_with_minimumValue_greater_than_maximumValue_parameter(int testMinimumValue, int testMaximumValue)
        {
            //Arrange
            var randomNumberGeneratorService = new RandomNumberGeneratorService();

            //Act
            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                randomNumberGeneratorService.GenerateRandomSigned32BitInteger(testMinimumValue, testMaximumValue));
        }

        [Theory]
        [InlineData(0, 0, 3)]
        [InlineData(-1, 1, 18)]
        [InlineData(-100, 100, 12)]
        [InlineData(0, 100, 55)]
        [InlineData(-100, 0, 1)]
        [InlineData(-100, -50, 8)]
        [InlineData(50, 100, 12)]
        public void When_a_request_is_made_to_generate_several_32_bit_integers(int testMinimumValue, int testMaximumValue, int countRequested)
        {
            //Arrange
            var randomNumberGeneratorService = new RandomNumberGeneratorService();

            //Act
            var result = randomNumberGeneratorService.GenerateRandomSigned32BitIntegers(testMinimumValue, testMaximumValue, countRequested);

            //Assert
            Assert.All(result, x => Assert.True(x >= testMinimumValue));
            Assert.All(result, x => Assert.True(x <= testMaximumValue));
            Assert.Equal(countRequested, result.Count());
        }

        [Fact]
        public void When_a_request_is_made_to_generate_several_32_bit_integers_but_no_parameters_are_sent()
        {
            //Arrange
            var randomNumberGeneratorService = new RandomNumberGeneratorService();

            //Act
            var result = randomNumberGeneratorService.GenerateRandomSigned32BitIntegers();

            //Assert
            Assert.Empty(result);
        }

        [Fact]
        public void When_a_request_is_made_to_generate_several_32_bit_integers_but_a_zero_count_is_requested()
        {
            //Arrange
            var randomNumberGeneratorService = new RandomNumberGeneratorService();

            //Act
            var result = randomNumberGeneratorService.GenerateRandomSigned32BitIntegers(1, 10, 0);

            //Assert
            Assert.Empty(result);
        }

        [Theory]
        [InlineData(0, 0, -1)]
        [InlineData(-1, 1, -2)]
        [InlineData(-100, 100, -5)]
        [InlineData(0, 100, -10)]
        [InlineData(-100, 0, -20)]
        [InlineData(-100, -50, -40)]
        [InlineData(50, 100, -80)]
        public void When_a_request_is_made_to_generate_several_32_bit_integers_but_a_negative_count_is_requested(int testMinimumValue, int testMaximumValue, int countRequested)
        {
            //Arrange
            var randomNumberGeneratorService = new RandomNumberGeneratorService();

            //Act
            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                randomNumberGeneratorService.GenerateRandomSigned32BitIntegers(testMinimumValue, testMaximumValue, countRequested));
        }
    }
}
