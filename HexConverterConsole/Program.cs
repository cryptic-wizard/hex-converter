using System;
using HexConverter;

namespace HexConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] byteArray = { 42, 84, 255, 0 };
            string[] hexArray = { "2A", "54", "FF", "00" };
            string hex = HexConverter.ToHex(byteArray);
            byte[] hexBytes1 = HexConverter.ToByteArray(hex);
            byte[] hexBytes2 = HexConverter.ToByteArray(hexArray);

            int debug = 0;
        }
    }
}
