using System.IO;

namespace RangeUnitTest.Classes
{
    public class FileOperations
    {
        public static string[] OregonCities() => File.ReadAllLines("OregonCityNames.txt");

    }
}
