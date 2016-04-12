namespace Ignite.Rules.Test
{
    public class SettingsStub : ISettings
    {
        public SettingsStub()
        {
            RulesPath = "https://raw.githubusercontent.com/intergenignite/ignite/master/resource/test/ignite_rules.json";
            SessionMapPath = "https://raw.githubusercontent.com/intergenignite/ignite/master/resource/test/ignite_session_map.json";
        }

        public string RulesPath { get; }
        public string SessionMapPath { get; }
    }
}