using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConfigurationLib;
using System.Linq;

namespace UnitTest
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void given_configurationFile_when_Loaded_then_validCountExpected()
        {
            var configurationFilepath = @"C:\Users\320050767\Source\Repos\G7CaseStudy13\StaticAnalyzer\ConfigurationLib\Configuration.xml";
            var toolsConfiguration = new ToolsConfiguration(configurationFilepath);
            toolsConfiguration.LoadConfiguration();
            var toolList = toolsConfiguration.Tools;
            var expectedValue = toolList.Count;
            var actualValue = 1;
            Assert.AreEqual(expectedValue,actualValue);
        }
        [TestMethod]
        public void given_configurationToolList_when_objectCreated_validCountExpecetd()
        {
            var configurationFilepath = @"C:\Users\320050767\Source\Repos\G7CaseStudy13\StaticAnalyzer\ConfigurationLib\Configuration.xml";
            var toolsConfiguration = new ToolsConfiguration(configurationFilepath);
            toolsConfiguration.LoadConfiguration();
            var staticAnalyzer = new StaticAnalysisApplication(toolsConfiguration.Tools);
            var staticAnalyzerList = staticAnalyzer.ToolObjectList;
            var expectedValue = staticAnalyzerList.Count();
            var actualValue = 2;
            Assert.AreEqual(expectedValue, actualValue);
        }
        [TestMethod]
        public void given_something_when_somethingHappens_then_expectedSomething()
        {

        }
    }
}
