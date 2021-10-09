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
            string hex = HexConverter.GetHex(byteArray);
            byte[] hexBytes1 = HexConverter.GetBytes(hex);
            byte[] hexBytes2 = HexConverter.GetBytes(hexArray);

            int debug = 0;
        }
    }
}
