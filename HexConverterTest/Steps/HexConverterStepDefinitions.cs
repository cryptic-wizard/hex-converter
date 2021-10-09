using TechTalk.SpecFlow;
using NUnit.Framework;
using System;

namespace HexConverter
{
    [Binding]
    public sealed class HexConverterStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private byte[] ByteArray;
        private bool ExceptionThrown = false;
        private string Hex;
        private string[] HexArray;

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
                Hex = HexConverter.ToHex(ByteArray);
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
                ByteArray = HexConverter.ToByteArray(Hex);
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
                ByteArray = HexConverter.ToByteArray(HexArray);
            }
            catch (Exception)
            {
                ExceptionThrown = true;
            }
        }

        #endregion
        #region ThenSteps

        [Then(@"the result string is (.*)")]
        public void ThenTheResultStringIsX(string result)
        {
            Assert.IsNotNull(Hex);
            Assert.AreEqual(result, Hex, "ERROR - " + result + " != " + Hex);
        }

        [Then(@"the result array is (.*)")]
        public void ThenTheResultArrayIs(string byteString)
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

        #endregion
    }
}
