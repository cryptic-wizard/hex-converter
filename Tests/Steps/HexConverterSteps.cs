using TechTalk.SpecFlow;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using HexConverterTest.Drivers;

namespace CrypticWizard.HexConverter
{
    [Binding]
    public sealed class HexConverterSteps
    {
        private readonly TestFixture Fixture;

        public HexConverterSteps(TestFixture fixture)
        {
            Fixture = fixture;
        }

        #region GivenSteps

        [Given(@"I have a null byte array")]
        public void GivenIHaveANullByteArray()
        {
            Fixture.ByteArray = null;
        }

        [Given(@"I have a null string array")]
        public void GivenIHaveANullStringArray()
        {
            Fixture.HexArray = null;
        }

        [Given(@"I have a null string")]
        public void GivenIHaveANullString()
        {
            Fixture.Hex = null;
        }

        [Given(@"I have byte array (.*)")]
        public void GivenIHaveByteArrayX(string byteString)
        {
            string[] split = byteString.Split(',');
            Fixture.ByteArray = new byte[split.Length];

            for (int i = 0; i < split.Length; i++)
            {
                Fixture.ByteArray[i] = Convert.ToByte(split[i]);
            }
        }

        [Given(@"I have string array (.*)")]
        public void GivenIHaveStringArrayX(string stringArray)
        {
            Fixture.HexArray = stringArray.Split(',');
        }

        [Given(@"I have string list (.*)")]
        public void GivenIHaveStringListX(string stringArray)
        {
            Fixture.HexList = stringArray.Split(',').ToList();
        }

        [Given(@"I have hex string (.*)")]
        public void GivenIHaveHexStringX(string hex)
        {
            Fixture.Hex = hex;
        }

        #endregion
        #region WhenSteps

        [When(@"I convert the byte array to hex")]
        public void WhenIConvertTheByteArrayToHex()
        {
            try
            {
                Fixture.Hex = HexConverter.GetHexString(Fixture.ByteArray);
            }
            catch (Exception)
            {
                Fixture.ExceptionThrown = true;
            }
        }

        [When(@"I convert the byte array to a hex array")]
        public void WhenIConvertTheByteArrayToAHexArray()
        {
            try
            {
                Fixture.HexArray = HexConverter.GetHexArray(Fixture.ByteArray);
            }
            catch (Exception)
            {
                Fixture.ExceptionThrown = true;
            }
        }

        [When(@"I convert the byte array to a hex list")]
        public void WhenIConvertTheByteArrayToAHexList()
        {
            try
            {
                Fixture.HexList = HexConverter.GetHexList(Fixture.ByteArray);
            }
            catch (Exception)
            {
                Fixture.ExceptionThrown = true;
            }
        }

        [When(@"I convert the string to a byte array")]
        public void WhenIConvertTheStringToAByteArray()
        {
            try
            {
                Fixture.ByteArray = HexConverter.GetBytes(Fixture.Hex);
            }
            catch(Exception)
            {
                Fixture.ExceptionThrown = true;
            }
        }

        [When(@"I convert the string array to a byte array")]
        public void WhenIConvertTheStringArrayToAByteArray()
        {
            try
            {
                Fixture.ByteArray = HexConverter.GetBytes(Fixture.HexArray);
            }
            catch (Exception)
            {
                Fixture.ExceptionThrown = true;
            }
        }

        [When(@"I convert the string list to a byte array")]
        public void WhenIConvertTheStringListToAByteArray()
        {
            try
            {
                Fixture.ByteArray = HexConverter.GetBytes(Fixture.HexList);
            }
            catch (Exception)
            {
                Fixture.ExceptionThrown = true;
            }
        }

        [When(@"I race Linq Concat with (\d+) bytes")]
        public void WhenIRaceLinqConcatWithXBytes(int qty)
        {
            Fixture.ByteArray = new byte[qty];
            new Random().NextBytes(Fixture.ByteArray);

            Fixture.sw.Restart();
            string.Concat(Fixture.ByteArray.Select(b => b.ToString("x2")));
            Fixture.sw.Stop();
            Fixture.LinqMillis = Fixture.sw.ElapsedTicks;
            Console.WriteLine(Fixture.LinqMillis.ToString() + " = Linq");

            Fixture.sw.Restart();
            HexConverter.GetHexString(Fixture.ByteArray);
            Fixture.sw.Stop();
            Fixture.HexConverterMillis = Fixture.sw.ElapsedTicks;
            Console.WriteLine(Fixture.HexConverterMillis.ToString() + " = HexConverter");
        }

        [When(@"I race Linq Select with (\d+) bytes")]
        public void WhenIRaceLinqSelectWithXBytes(int qty)
        {
            Fixture.ByteArray = new byte[qty];
            new Random().NextBytes(Fixture.ByteArray);
            Fixture.Hex = HexConverter.GetHexString(Fixture.ByteArray);

            Fixture.sw.Restart();
            Enumerable.Range(0, Fixture.Hex.Length)
               .Where(x => x % 2 == 0)
               .Select(x => Convert.ToByte(Fixture.Hex.Substring(x, 2), 16))
               .ToArray();
            Fixture.sw.Stop();
            Fixture.LinqMillis = Fixture.sw.ElapsedTicks;
            Console.WriteLine(Fixture.LinqMillis.ToString() + " = Linq");

            Fixture.sw.Restart();
            HexConverter.GetBytes(Fixture.Hex);
            Fixture.sw.Stop();
            Fixture.HexConverterMillis = Fixture.sw.ElapsedTicks;
            Console.WriteLine(Fixture.HexConverterMillis.ToString() + " = HexConverter");
        }

        #endregion
        #region ThenSteps

        [Then(@"the result string is (.*)")]
        public void ThenTheResultStringIsX(string result)
        {
            Assert.IsNotNull(Fixture.Hex);
            Assert.AreEqual(result, Fixture.Hex, "ERROR - " + result + " != " + Fixture.Hex);
        }

        [Then(@"the result string array is (.*)")]
        public void ThenTheResultStringArrayIsX(string result)
        {
            Assert.IsNotNull(Fixture.HexArray);
            string[] split = result.Split(',');
            Assert.AreEqual(split.Length, Fixture.HexArray.Length, "Length of byte array did not match");

            for (int i = 0; i < split.Length; i++)
            {
                Assert.AreEqual(split[i], Fixture.HexArray[i], "ERROR - " + split[i] + " != " + Fixture.HexArray[i]);
            }
        }

        [Then(@"the result string list is (.*)")]
        public void ThenTheResultStringListIsX(string result)
        {
            Assert.IsNotNull(Fixture.HexList);
            string[] split = result.Split(',');
            Assert.AreEqual(split.Length, Fixture.HexList.Count, "Length of byte array did not match");

            for (int i = 0; i < split.Length; i++)
            {
                Assert.AreEqual(split[i], Fixture.HexList[i], "ERROR - " + split[i] + " != " + Fixture.HexList[i]);
            }
        }

        [Then(@"the result byte array is (.*)")]
        public void ThenTheResultByteArrayIs(string byteString)
        {
            Assert.IsNotNull(Fixture.ByteArray);

            string[] split = byteString.Split(',');
            byte[] byteArray = new byte[split.Length];

            for (int i = 0; i < split.Length; i++)
            {
                byteArray[i] = Convert.ToByte(split[i]);
            }

            Assert.AreEqual(byteArray.Length, Fixture.ByteArray.Length, "Length of byte array did not match");

            for(int i = 0; i < byteArray.Length; i++)
            {
                Assert.AreEqual(byteArray[i], Fixture.ByteArray[i], Fixture.ByteArray[i].ToString() + " did not match " + byteArray[i].ToString());
            }
        }

        [Then(@"an exception was thrown")]
        public void ThenAnExceptionWasThrown()
        {
            Assert.IsTrue(Fixture.ExceptionThrown);
        }

        [Then(@"HexConverter is faster")]
        public void ThenHexConverterIsFaster()
        {
            Assert.Less(Fixture.HexConverterMillis, Fixture.LinqMillis);
        }

        #endregion
    }
}
