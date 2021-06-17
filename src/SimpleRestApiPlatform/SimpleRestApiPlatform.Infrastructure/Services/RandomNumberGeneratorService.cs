using SimpleRestApiPlatform.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleRestApiPlatform.Infrastructure.Services
{
    public class RandomNumberGeneratorService : IRandomNumberGeneratorService
    {
        static Random _random = new();

        public IEnumerable<int> GenerateRandomSigned32BitIntegers(int minimumValue = int.MinValue, int maximumValue = int.MaxValue, int count = 0)
        {
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("Count of generated numbers cannot be less than zero.");
            }

            try
            {
                return Enumerable.Range(0, count)
                    .Select(rand => GenerateRandomSigned32BitInteger(minimumValue, maximumValue))
                    .ToList();
            }
            catch
            {
                throw;
            }
        }

        public int GenerateRandomSigned32BitInteger(int minimumValue = int.MinValue, int maximumValue = int.MaxValue)
        {
            try
            {
                return _random.Next(minimumValue, maximumValue);
            }
            catch
            {
                throw;
            }
        }
    }
}
