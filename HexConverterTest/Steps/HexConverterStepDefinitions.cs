using TechTalk.SpecFlow;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;

namespace CrypticWizard.HexConverter
{
    [Binding]
    public sealed class HexConverterStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private static Stopwatch sw = new Stopwatch();
        private long LinqMillis = 0;
        private long HexConverterMillis = 0;

        private byte[] ByteArray;
        private bool ExceptionThrown = false;
        private string Hex;
        private string[] HexArray;
        private List<string> HexList;

        public HexConverterStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            ByteArray = null;
            Hex = null;
            HexArray = null;
            ExceptionThrown = false;
            LinqMillis = 0;
            HexConverterMillis = 0;

            sw.Stop();
            sw.Reset();
        }

        #region GivenSteps

        [Given(@"I have a null byte array")]
        public void GivenIHaveANullByteArray()
        {
            ByteArray = null;
        }

        [Given(@"I have a null string array")]
        public void GivenIHaveANullStringArray()
        {
            HexArray = null;
        }

        [Given(@"I have a null string")]
        public void GivenIHaveANullString()
        {
            Hex = null;
        }

        [Given(@"I have byte array (.*)")]
        public void GivenIHaveByteArrayX(string byteString)
        {
            string[] split = byteString.Split(',');
            ByteArray = new byte[split.Length];

            for (int i = 0; i < split.Length; i++)
            {
                ByteArray[i] = Convert.ToByte(split[i]);
            }
        }

        [Given(@"I have string array (.*)")]
        public void GivenIHaveStringArrayX(string stringArray)
        {
            HexArray = stringArray.Split(',');
        }

        [Given(@"I have string list (.*)")]
        public void GivenIHaveStringListX(string stringArray)
        {
            HexList = stringArray.Split(',').ToList();
        }

        [Given(@"I have hex string (.*)")]
        public void GivenIHaveHexStringX(string hex)
        {
            Hex = hex;
        }

        #endregion
        #region WhenSteps

        [When(@"I convert the byte array to hex")]
        public void WhenIConvertTheByteArrayToHex()
        {
            try
            {
                Hex = HexConverter.GetHex(ByteArray);
            }
            catch (Exception)
            {
                ExceptionThrown = true;
            }
        }

        [When(@"I convert the byte array to a hex array")]
        public void WhenIConvertTheByteArrayToAHexArray()
        {
            try
            {
                HexArray = HexConverter.GetHexArray(ByteArray);
            }
            catch (Exception)
            {
                ExceptionThrown = true;
            }
        }

        [When(@"I convert the byte array to a hex list")]
        public void WhenIConvertTheByteArrayToAHexList()
        {
            try
            {
                HexList = HexConverter.GetHexList(ByteArray);
            }
            catch (Exception)
            {
                ExceptionThrown = true;
            }
        }

        [When(@"I convert the string to a byte array")]
        public void WhenIConvertTheStringToAByteArray()
        {
            try
            {
                ByteArray = HexConverter.GetBytes(Hex);
            }
            catch(Exception)
            {
                ExceptionThrown = true;
            }
        }


        [When(@"I convert the string array to a byte array")]
        public void WhenIConvertTheStringArrayToAByteArray()
        {
            try
            {
                ByteArray = HexConverter.GetBytes(HexArray);
            }
            catch (Exception)
            {
                ExceptionThrown = true;
            }
        }

        [When(@"I convert the string list to a byte array")]
        public void WhenIConvertTheStringListToAByteArray()
        {
            try
            {
                ByteArray = HexConverter.GetBytes(HexList);
            }
            catch (Exception)
            {
                ExceptionThrown = true;
            }
        }

        [When(@"I race Linq Concat with (\d+) bytes")]
        public void WhenIRaceLinqConcatWithXBytes(int qty)
        {
            ByteArray = new byte[qty];
            new Random().NextBytes(ByteArray);

            sw.Restart();
            string.Concat(ByteArray.Select(b => b.ToString("x2")));
            sw.Stop();
            LinqMillis = sw.ElapsedMilliseconds;
            Console.WriteLine(LinqMillis.ToString() + " = Linq");

            sw.Restart();
            HexConverter.GetHex(ByteArray);
            sw.Stop();
            HexConverterMillis = sw.ElapsedMilliseconds;
            Console.WriteLine(HexConverterMillis.ToString() + " = HexConverter");
        }

        [When(@"I race Linq Select with (\d+) bytes")]
        public void WhenIRaceLinqSelectWithXBytes(int qty)
        {
            ByteArray = new byte[qty];
            new Random().NextBytes(ByteArray);
            Hex = HexConverter.GetHex(ByteArray);

            sw.Restart();
            Enumerable.Range(0, Hex.Length)
               .Where(x => x % 2 == 0)
               .Select(x => Convert.ToByte(Hex.Substring(x, 2), 16))
               .ToArray();
            sw.Stop();
            LinqMillis = sw.ElapsedMilliseconds;
            Console.WriteLine(LinqMillis.ToString() + " = Linq");

            sw.Restart();
            HexConverter.GetBytes(Hex);
            sw.Stop();
            HexConverterMillis = sw.ElapsedMilliseconds;
            Console.WriteLine(HexConverterMillis.ToString() + " = HexConverter");
        }

        #endregion
        #region ThenSteps

        [Then(@"the result string is (.*)")]
        public void ThenTheResultStringIsX(string result)
        {
            Assert.IsNotNull(Hex);
            Assert.AreEqual(result, Hex, "ERROR - " + result + " != " + Hex);
        }

        [Then(@"the result string array is (.*)")]
        public void ThenTheResultStringArrayIsX(string result)
        {
            Assert.IsNotNull(HexArray);
            string[] split = result.Split(',');
            Assert.AreEqual(split.Length, HexArray.Length, "Length of byte array did not match");

            for (int i = 0; i < split.Length; i++)
            {
                Assert.AreEqual(split[i], HexArray[i], "ERROR - " + split[i] + " != " + HexArray[i]);
            }
        }

        [Then(@"the result string list is (.*)")]
        public void ThenTheResultStringListIsX(string result)
        {
            Assert.IsNotNull(HexList);
            string[] split = result.Split(',');
            Assert.AreEqual(split.Length, HexList.Count, "Length of byte array did not match");

            for (int i = 0; i < split.Length; i++)
            {
                Assert.AreEqual(split[i], HexList[i], "ERROR - " + split[i] + " != " + HexList[i]);
            }
        }

        [Then(@"the result byte array is (.*)")]
        public void ThenTheResultByteArrayIs(string byteString)
        {
            Assert.IsNotNull(ByteArray);

            string[] split = byteString.Split(',');
            byte[] byteArray = new byte[split.Length];

            for (int i = 0; i < split.Length; i++)
            {
                byteArray[i] = Convert.ToByte(split[i]);
            }

            Assert.AreEqual(byteArray.Length, ByteArray.Length, "Length of byte array did not match");

            for(int i = 0; i < byteArray.Length; i++)
            {
                Assert.AreEqual(byteArray[i], ByteArray[i], ByteArray[i].ToString() + " did not match " + byteArray[i].ToString());
            }
        }

        [Then(@"an exception was thrown")]
        public void ThenAnExceptionWasThrown()
        {
            Assert.IsTrue(ExceptionThrown);
        }

        [Then(@"HexConverter is faster")]
        public void ThenHexConverterIsFaster()
        {
            Assert.Less(HexConverterMillis, LinqMillis);
        }

        #endregion
    }
}
