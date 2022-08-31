using BoDi;
using TechTalk.SpecFlow;
using HexConverterTest.Drivers;

namespace CrypticWizard.HexConverter
{
    [Binding]
    public class TestFixtureSteps
    {
        private readonly IObjectContainer objectContainer;

        public TestFixtureSteps(IObjectContainer objectContainer)
        {
            this.objectContainer = objectContainer;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            objectContainer.RegisterInstanceAs(new TestFixture());
        }

        [AfterScenario]
        public void AfterScenario()
        {

        }
    }
}