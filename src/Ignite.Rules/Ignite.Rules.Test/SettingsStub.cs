namespace Ignite.Rules.Test
{
    public class SettingsStub : ISettings
    {
        public SettingsStub()
        {
            RulesPath = "https://raw.githubusercontent.com/intergenignite/ignite/master/resource/test/ignite_rules.json";
            SessionMapPath = "https://raw.githubusercontent.com/intergenignite/ignite/master/resource/test/ignite_session_map.json";
        }

        public SettingsStub WithRulesPath(string path)
        {
            RulesPath = path;
            return this;
        }

        public SettingsStub WithSessionMapPath(string path)
        {
            SessionMapPath = path;
            return this;
        }

        public string RulesPath { get; private set; }
        public string SessionMapPath { get; private set; }
    }
}