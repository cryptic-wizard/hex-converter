using System;
using System.Collections.Generic;
using CrypticWizard.HexConverter;

namespace CrypticWizard.HexConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] byteArray = { 42, 84, 255, 0 };

            string hex = "2A54FF00";
            string[] hexArray = { "2A", "54", "FF", "00" };
            List<string> hexList = new List<string> { "2A", "54", "FF", "00" };

            byte[] hexBytes1 = HexConverter.GetBytes(hex);
            byte[] hexBytes2 = HexConverter.GetBytes(hexArray);
            byte[] hexBytes3 = HexConverter.GetBytes(hexList);

            hex = HexConverter.GetHex(hexBytes1);
            hexArray = HexConverter.GetHexArray(hexBytes2);
            hexList = HexConverter.GetHexList(hexBytes3);
        }
    }
}
