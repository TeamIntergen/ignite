using Ignite.Rules.Dto;
using NUnit.Framework;

namespace Ignite.Rules.Test
{
    [TestFixture]
    public class SecurityRepositoryTest
    {
        [Test]
        public void CanLoadAccessLevel()
        {
            var webLoader = new WebLoader();
            var settings = new SettingsStub();
            var repos = new SecurityRepository(webLoader, settings);

            RulesDto level = repos.LoadAccessLevel().Result;

            Assert.IsNotNull(level);
            level.AssertNoPropertiesAreNull();
        }

        [Test]
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