using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ignite.Rules.Test
{
    [TestClass]
    public class SecurityRepositoryTest
    {
        [TestMethod]
        public void CanLoadAccessLevel()
        {
            var webLoader = new WebLoader();
            var settings = new SettingsStub();
            var repos = new SecurityRepository(webLoader, settings);

            var level = repos.LoadAccessLevel().Result;

            Assert.IsNotNull(level);
            level.AssertNoPropertiesAreNull();
        }

        [TestMethod]
        public void CanLoadSessionMap()
        {
            var webLoader = new WebLoader();
            var settings = new SettingsStub();
            var repos = new SecurityRepository(webLoader, settings);

            var level = repos.LoadSessionMap().Result;

            Assert.IsNotNull(level);
            level.AssertNoPropertiesAreNull();
        }
    }
}