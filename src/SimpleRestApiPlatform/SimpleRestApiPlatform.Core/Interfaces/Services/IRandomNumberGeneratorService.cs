using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleRestApiPlatform.Core.Interfaces.Services
{
    public interface IRandomNumberGeneratorService
    {
        int GenerateRandomSigned32BitInteger(int minimumValue = int.MinValue, int maximumValue = int.MaxValue);
        IEnumerable<int> GenerateRandomSigned32BitIntegers(int minimumValue = int.MinValue, int maximumValue = int.MaxValue, int count = 0);
    }
}
