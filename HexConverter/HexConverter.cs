using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Text.RegularExpressions;

namespace CrypticWizard.HexConverter
{
    public static class HexConverter
    {
        private static bool Initialized = false;
        private static readonly Dictionary<byte, string> HexChars = new()
        {
            { 0 , "0" },
            { 1 , "1" },
            { 2 , "2" },
            { 3 , "3" },
            { 4 , "4" },
            { 5 , "5" },
            { 6 , "6" },
            { 7 , "7" },
            { 8 , "8" },
            { 9 , "9" },
            { 10, "A" },
            { 11, "B" },
            { 12, "C" },
            { 13, "D" },
            { 14, "E" },
            { 15, "F" },
        };

        public static readonly Dictionary<byte, string> ByteToHex = new();
        public static readonly Dictionary<string, byte> HexToByte = new();

        static HexConverter()
        {
            if (!Initialized)
            {
                Initialize();
            }
        }

        /// <summary>
        /// Initialize the static dictionaries in advance of using the HexConverter class
        /// </summary>
        private static void Initialize()
        {
            if (Initialized)
            {
                return;
            }
            string hex;

            for (int i = 0; i <= byte.MaxValue; i++)
            {
                hex = HexChars[(byte)(i >> 4)] + HexChars[(byte)(i & 0x0F)];
                ByteToHex.Add((byte)i, hex);
                HexToByte.Add(hex, (byte)i);
            }

            Initialized = true;
        }

        /// <summary>
        /// Gets a hexadecimal string
        /// </summary>
        /// <param name="bytes"> raw bytes </param>
        /// <returns> Ex. "2A54FF00" </returns>
        public static string GetHexString(byte[] bytes)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException();
            }

            string[] hexArray = new string[bytes.Length];

            for (int i = 0; i < bytes.Length; i++)
            {
                hexArray[i] = ByteToHex[bytes[i]];
            }

            return string.Concat(hexArray);
        }

        /// <summary>
        /// Gets a hexadecimal string array
        /// </summary>
        /// <param name="bytes"> raw bytes </param>
        /// <returns> Ex. { "2A", "54", "FF", "00" } </returns>
        public static string[] GetHexArray(byte[] bytes)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException();
            }

            string[] hexArray = new string[bytes.Length];

            for (int i = 0; i < bytes.Length; i++)
            {
                hexArray[i] = ByteToHex[bytes[i]];
            }

            return hexArray;
        }

        /// <summary>
        /// Gets a hexadecimal string list
        /// </summary>
        /// <param name="bytes"> raw bytes </param>
        /// <returns> Ex. { "2A", "54", "FF", "00" } </returns>
        public static List<string> GetHexList(byte[] bytes)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException();
            }

            List<string> hexArray = new();

            for (int i = 0; i < bytes.Length; i++)
            {
                hexArray.Add(ByteToHex[bytes[i]]);
            }

            return hexArray;
        }

        /// <summary>
        /// Gets raw bytes from hexadecimal string array
        /// </summary>
        /// <param name="hex"> hexadecimal string array </param>
        /// <returns></returns>
        public static byte[] GetBytes(string[] hex)
        {
            if (hex == null)
            {
                throw new ArgumentNullException();
            }

            byte[] byteArray = new byte[hex.Length];

            for (int i = 0; i < hex.Length; i++)
            {
                byteArray[i] = HexToByte[hex[i]];
            }

            return byteArray;
        }

        /// <summary>
        /// Gets raw bytes from hexadecimal string list
        /// </summary>
        /// <param name="hex"> hexadecimal string list </param>
        /// <returns></returns>
        public static byte[] GetBytes(List<string> hex)
        {
            if (hex == null)
            {
                throw new ArgumentNullException();
            }

            byte[] byteArray = new byte[hex.Count];

            for (int i = 0; i < hex.Count; i++)
            {
                byteArray[i] = HexToByte[hex[i]];
            }

            return byteArray;
        }

        /// <summary>
        /// Gets raw bytes from hexadecimal string
        /// </summary>
        /// <param name="hex"> hexadecimal string </param>
        /// <returns></returns>
        public static byte[] GetBytes(string hex)
        {
            byte[] byteArray;

            if (hex == null)
            {
                throw new ArgumentNullException();
            }

            // Odd length
            if (hex.Length % 2 != 0)
            {
                byteArray = new byte[(hex.Length / 2) + 1];
                byteArray[0] = HexToByte["0" + hex.Substring(0, 1)];

                for (int i = 1; i < hex.Length; i += 2)
                {
                    byteArray[(i / 2) + 1] = HexToByte[hex.Substring(i, 2)];
                }
            }
            else // Even length
            {
                byteArray = new byte[hex.Length / 2];

                for (int i = 0; i < hex.Length; i += 2)
                {
                    byteArray[i / 2] = HexToByte[hex.Substring(i, 2)];
                }
            }

            return byteArray;
        }
    }
}
