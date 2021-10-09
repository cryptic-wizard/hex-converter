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
        private string Hex;
        private bool Exception = false;

        public HexConverterStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            ByteArray = null;
            Hex = null;
            Exception = false;
        }

        [Given(@"I have a null byte array")]
        public void GivenIHaveByteArrayX()
        {
            ByteArray = null;
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

        [When(@"I convert the byte array to hex")]
        public void WhenIConvertTheByteArrayToHex()
        {
            Hex = HexConverter.ToHex(ByteArray);
        }

        [When(@"I attempt to convert the byte array to hex")]
        public void WhenIAttemptToConvertTheByteArrayToHex()
        {
            try
            {
                Hex = HexConverter.ToHex(ByteArray);
            }
            catch(Exception)
            {
                Exception = true;
            }
        }

        [Then(@"the result string is (.*)")]
        public void ThenTheResultStringIsX(string result)
        {
            Assert.IsNotNull(Hex);
            Assert.AreEqual(result, Hex, "ERROR - " + result + " != " + Hex);
        }

        [Then(@"an exception was thrown")]
        public void ThenAnExceptionWasThrown()
        {
            Assert.IsTrue(Exception);
        }
    }
}
