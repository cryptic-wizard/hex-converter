using CrypticWizard.HexConverter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace HexConverterTest.Drivers
{
    public class TestFixture
    {
        public Stopwatch sw = new();
        public long LinqMillis = 0;
        public long HexConverterMillis = 0;

        public byte[] ByteArray;
        public bool ExceptionThrown = false;
        public string Hex;
        public string[] HexArray;
        public List<string> HexList;
    }
}
