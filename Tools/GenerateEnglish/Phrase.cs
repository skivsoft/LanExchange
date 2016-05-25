namespace GenerateEnglish
{
    internal sealed class Phrase
    {
        public Phrase(string context, string id)
        {
            Context = context;
            Id = id;
        }

        public string Context { get; }

        public string Id { get; }
    }
}