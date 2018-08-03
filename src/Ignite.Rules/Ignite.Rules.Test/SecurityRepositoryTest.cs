using System.IO;
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
//            var webLoader = new WebLoader();
//            var settings = new SettingsStub();
//            var repos = new SecurityRepository(webLoader, settings);
            CanLoadAccessLevelFromPath("../../../../../resource/envision2018-prod/rules.json", "../../../../../resource/envision2018-prod/session_map.json");
            CanLoadAccessLevelFromPath("../../../../../resource/envision2018-test/rules.json", "../../../../../resource/envision2018-test/session_map.json");
            CanLoadAccessLevelFromPath("../../../../../resource/ignite2018/ignite_rules.json", "../../../../../resource/ignite2018/ignite_session_map.json");
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

        private void CanLoadAccessLevelFromPath(string rulesPath, string sessionMapPath)
        {
            var fileLoader = new FileLoader();
            var settings = new SettingsStub()
                .WithRulesPath(Path.Combine(Directory.GetCurrentDirectory(), rulesPath))
                .WithSessionMapPath(Path.Combine(Directory.GetCurrentDirectory(), sessionMapPath));
            var repos = new SecurityRepository(fileLoader, settings);

            RulesDto level = repos.LoadAccessLevel().Result;

            Assert.IsNotNull(level);
            level.AssertNoPropertiesAreNull(new []{ "Identifier" });            
        }
    }
}