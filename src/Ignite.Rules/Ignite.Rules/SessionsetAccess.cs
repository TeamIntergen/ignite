namespace Ignite.Rules
{
    public class SessionsetAccess
    {
        public SessionsetAccess(string identifier)
        {
            Identifier = identifier;
        }

        public string Identifier { get; private set; }
    }
}