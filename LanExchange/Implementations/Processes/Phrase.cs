namespace LanExchange.Implementations.Processes
{
    public class Phrase
    {
        public Phrase(string context, string id)
        {
            Context = context;
            Id = id;
        }

        public string Context { get; private set; }

        public string Id { get; private set; }
    }
}